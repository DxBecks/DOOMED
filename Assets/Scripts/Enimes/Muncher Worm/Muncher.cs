using UnityEngine;
using System.Collections;

public class Muncher : MonoBehaviour {

	/*DATA MEMBERS*/

	//Instantiate private class level objects.
	private AudioSource[] sources;			//An array of all the AudioSources attached to Muncher's child GameObject's.
	private AudioSource   breathSound;		//The Muncher breath sound AudioSource.
	private AudioSource	  screamSound;		//The Muncher scream sound AudioSource.
	private Light 		  eye;				//The light source on the Muncher's eye.
	private BoxCollider2D trigger;			//The Muncher's trigger.

	//Declare private class level objects.
	[SerializeField][Range(25.0f, 100.0f)]	private float chargeForce;	 	//The charge speed of the muncher worm.
	[SerializeField][Range(1.0f, 5.0f)]		private float lightIntensity;	//The intensity of the eye light.
	[SerializeField][Range(0.0f, 20.0f)] 	private float fadeDuration;		//The duration of the audio fade.

	/**
	 * Method Name: Awake
	 * Description: Method is the first to execute, executes only once.
	 */ 
	void Awake() {
		sources 	= GetComponentsInChildren<AudioSource>();	//A reference to the AudioSources.
		eye     	= GetComponentInChildren<Light>();			//A reference to the Light component.
		trigger 	= GetComponent<BoxCollider2D> ();			//A reference to the BoxCollider2D component.
		//Sound effect assignment.
		breathSound 		  = sources [0];					//Assign the first AudioSource in the sources array to breathSound.
		screamSound 		  = sources [1];					//Assign the second AudioSource in the sources array to screamSound.
	}
	/**
	 * Method Name: OnTriggerEnter2D
	 * Description: Method handles behavior when a collider enters a trigger.
	 * @param collider
	 */ 
	void OnTriggerEnter2D(Collider2D collider) {
		//If the collider belongs to the player...
		if (collider.gameObject.tag == "Player") {
			//...Awaken the worm.
			StartCoroutine(Awaken());
			//...Disable the trigger.
			trigger.enabled = false;
		}
	}
	/**
	 * Method Name: OnTriggerExit2D
	 * Description: Method handles behavior when a collider exits a trigger.
	 * @param collider
	 */ 
	void OnTriggerExit2D(Collider2D collider) {
		//If the collider is tagged as 'Destroy'...
		if (collider.gameObject.tag == "Destroy") {
			//...Start the Dying coroutine.
			StartCoroutine("Dying");
		}
	}
	/**
	 * Method Name: OnCollisionEnter2D
	 * Description: Method handles behavior when a collision occurs between two colliders.
	 * @param collision
	 */ 
	void OnCollisionEnter2D(Collision2D collision) {
		//If one of the colliding objects is the player...
		if(collision.gameObject.tag == "Player") {
			//...Damage the player.
			collision.gameObject.SendMessage("Damage", 1);
		}
	}
	/**
	 * Coroutine Name: Awaken
	 * Description   : Coroutine handles Muncher behavior when the Kiwi enters it's trigger.
	 */
	private IEnumerator Awaken() {
		//Set the intensity for the Muncher's eye light.
		eye.intensity = lightIntensity;
		//Play the Muncher scream sound.
		screamSound.Play();
		//Wait for the scream to stop before continuing.
		yield return new WaitForSeconds(screamSound.clip.length / 2.5f);
		//Increase the pitch of the Muncher breathing sound.
		breathSound.pitch = 3.0f;
		//Start the Charge coroutine.
		StartCoroutine("Charge");
	}
	/**
	 * Coroutine Name: Charge
	 * Description   : Coroutine translates/moves the Muncher forward
	 */ 
	private IEnumerator Charge() {
		//While true... (enables code to execute every frame).
		while(true) {
			//...Translate the worm forward
			this.transform.Translate(-transform.right * transform.localScale.x * chargeForce * Time.deltaTime, Space.Self);
			//...Yield until the next frame before next loop execution.
			yield return 0;
		}
	}
	/**
	 * Coroutine Name: Dying
	 * Description   : Coroutine handles Muncher behavior before it is destroyed.
	 */ 
	private IEnumerator Dying() {
		//Stop the Charge coroutine.
		StopCoroutine ("Charge");
		//Start the AudioFade coroutine.
		AudioController.Instance.AudioFadeOut(sources, fadeDuration, true);
		//Destroy the Muncher.
		Destroy(gameObject, fadeDuration + 1.0f);
		//...Yield until the next frame.
		yield return 0;
	}
}