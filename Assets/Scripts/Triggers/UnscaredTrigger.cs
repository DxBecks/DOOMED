using UnityEngine;
using System.Collections;

public class UnscaredTrigger : MonoBehaviour {
	/**
	 * Method Name: OnTriggerEnter2D
	 * Description: Method handles behavior when a collider enters the trigger.
	 * @param collider
	 */ 
	void OnTriggerEnter2D(Collider2D collider) {
		//If the collider belongs to the Player...
		if(collider.gameObject.tag == "Player") {
			//...Send a message to the Player to initiate the Scared state.
			collider.gameObject.SendMessage("Unscared");
			//Destroy the trigger.
			Destroy(this.gameObject);
		}
	}
}