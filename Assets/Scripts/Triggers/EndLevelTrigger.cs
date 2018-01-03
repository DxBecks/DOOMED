using UnityEngine;
using System.Collections;

public class EndLevelTrigger : MonoBehaviour {
	/*DATA MEMBERS*/

	//Declare private class level variables.
	[SerializeField] private string level;		//The name of the level to load.
	[SerializeField] private float  fadeSpeed;	//The speed of the fade on level load.

	/**
	 * Method Name: OnTriggerEnter2D
	 * Description: Method handles behavior when a collider enters the trigger.
	 * @param collider
	 */ 
	void OnTriggerEnter2D(Collider2D collider) {
		//If the collider belongs to the Player...
		if(collider.gameObject.tag == "Player") {
			//Load the next level...
			GameController.Instance.LoadLevel(level, fadeSpeed);
		}
	}
}