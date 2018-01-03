using UnityEngine;
using System.Collections;

public class KiwiHealth : MonoBehaviour {

	//Declare public class level variables.
	public int  health;			//The current health
	public int  maximumHealth;	//The maximum amount of health of the Kiwi.
	public bool dying;			//True if the Kiwi bird is dying.
	public bool dead;			//True after the Kiwi bird is dead.

	/**
	 * Method Name: Awake
	 * Description: Method is the first to execute, executes only once.
	 */ 
	void Awake() {
		health = maximumHealth; //Initialize the current health the to the maximum amount of health possible.
		dying  = false;			//Initialize dying to false.
		dead   = false; 		//Initialize dead to false.
	}
	/**
	 * Method Name: Update
	 * Description: Method executes once per frame.
	 */ 
	void Update () {
		//Check the health of the Kiwi bird.
		CheckHealth();
	}
	/**
	 * Method Name: CheckHealth
	 * Description: Method checks the health level of the Kiwi bird and sets its state to dying or dead if health reaches zero.
	 */ 
	private void CheckHealth() {
		//If the Kiwi bird's health is zero and the Kiwi is not already dying...
		if (health == 0 && !dying) {
			//...The Kiwi bird is dying.
			dying = true;
			//...Send a message to execute the dying behavior.
			this.SendMessage("Dying");
		}
		//...Else if the Kiwi bird is dead...
		else if (dead) {
			//...Reset the game.
			GameController.Instance.ResetGame();
		}
	}
	/**
	 * Method Name: Damage
	 * Description: Method enables the Kiwi to take damage from another source.
	 * @param amountDamage
	 */ 
	private void Damage(int amountDamage) {
		//Set the health equal to health minus amountDamage, unless amount Damage is greater than health, then set health to zero.
		health = health - amountDamage > 0 ? health - amountDamage : 0;
	}
	/**
	 * Method Name: Heal
	 * Description: Method enables the Kiwi to heal.
	 * @param amountHealth
	 */ 
	private void Heal(int amountHealth) {
		//Set the health equal to health plus amountHealth, unless amountHealth is greater than health, then set health to maximum health.
		health = health + amountHealth <= maximumHealth ? health + amountHealth : maximumHealth;
	}
}