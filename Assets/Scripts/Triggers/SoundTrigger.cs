using UnityEngine;
using System.Collections;

public class SoundTrigger : MonoBehaviour {
	/*DATA MEMBERS*/
	
	//Instantiate private class level objects.
	private AudioSource source;	//The audio source for the break sound effect.
	
	/**
	 * Method Name: Start
	 * Description: Method executes once after the Awake method.
	 */ 
	void Start () {
		source = GetComponent<AudioSource>();	//A reference to the AudioSource component.
	}
	
	/**
	 * Method Name: OnTriggerEnter2D
	 * Description: Method handles behavior when a collider enters the trigger.
	 * @param collider
	 */ 
	void OnTriggerEnter2D(Collider2D collider) {
		//If the Muncher worm collides with the barricade...
		if (collider.gameObject.tag == "Player") {
			//...Play the break sound effect.
			source.Play();
		}
	}
}