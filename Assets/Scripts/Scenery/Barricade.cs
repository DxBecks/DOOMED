using UnityEngine;
using System.Collections;

public class Barricade : MonoBehaviour {

	/*DATA MEMBERS*/

	//Instantiate private class level objects.
	private AudioSource source;	//The audio source for the break sound effect.
	private MeshRenderer mesh;	//The mesh renderer component.

	//Declare private class level variables.
	private bool hit;	//Boolean value is true if the barrior has been hit by the Muncher worm, false when not.

	/**
	 * Method Name: Start
	 * Description: Method executes once after the Awake method.
	 */ 
	void Start () {
		source = GetComponentInChildren<AudioSource>();	//A reference to the AudioSource component.
		mesh   = GetComponent<MeshRenderer> ();			//A reference to the MeshRenderer component.
		hit    = false;									//Initialize hit to false.
	}
	/**
	 * Method Name: OnCollisionEnter2D
	 * Description: Method handles behavior for collisions between two colliders.
	 * @param collision
	 */ 
	void OnTriggerEnter2D(Collider2D collider) {
		//If the Muncher worm collides with the barricade...
		if (collider.gameObject.tag == "Muncher (Worm)" && !hit) {
			//...Start the Break coroutine.
			StartCoroutine("Break");
		}
	}
	/**
	 * Coroutine Name: Break
	 * Description   : Method handles the behavior for the Muncher worm breaking the barrior.
	 */ 
	private IEnumerator Break() {
		//The barrior has been hit.
		hit = true;
		//Disable the mesh on the barrior.
		mesh.enabled = false;
		//Play the break sound effect.
		source.PlayOneShot(source.clip);
		//Destroy the barricade.
		Destroy(this.gameObject, source.clip.length);
		//Yield until next frame before continuing execution.
		yield return 0;
	}
}