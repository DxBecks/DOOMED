using UnityEngine;
using System.Collections;

public class Lantern : MonoBehaviour {

	/*DATA MEMBERS*/

	//Declare private class level variables.
	[SerializeField][Range(0, 20)] 		 	private int   minimumLevel    = 2;		//The minimum level of fuel in the lantern.
	[SerializeField][Range(1, 20)] 		 	private int   maximumLevel    = 10;		//The maximum level of fuel in the lantern.
	[SerializeField][Range(1, 10)] 		 	private int   depletionAmount = 1;		//The amount the fuel depletes every cycle.
	[SerializeField][Range(1.0f, 50.0f)]	private float repeatTime 	  = 15.0f;	//The amount of time in one cycle.

	//Declare public class level variables.
	[Range(1.0f, 20.0f)]	public int   fuel;	//The amount of fuel in the lantern.

	//Instantiate private class level objects.
	private Animator animator;	//The animator of the lantern.
	private Light	 lantern;	//The light of the lantern.

	/**
	 * Method Name: Awake
	 * Description: Method is the first to execute, executes only once.
	 */ 
	void Awake() {
		animator = GetComponentInChildren<Animator>();	//A reference to the animator component.
		lantern  = GetComponentInChildren<Light>();		//A reference to the light component.
		fuel 	 = maximumLevel;
	}
	/**
	 * Method Name: Start
	 * Description: Method executes once after the Awake method.
	 */ 
	void Start() {
		//Repeatedly call the Decrease Fuel method.
		InvokeRepeating("DecreaseFuel", 0.01f, repeatTime);
	}
	/**
	 * Method Name: Update
	 * Description: Method executes once per frame.
	 */ 
	void Update() {
		//Update the amount of fuel in the animator.
		UpdateLanternRange();
	}
	/**
	 * Method Name: DecreaseFuel
	 * Description: Method decreases the amount of fuel in the lantern by a set amount.
	 */ 
	private void DecreaseFuel() {
		//Decrease the amount of fuel.
		fuel = fuel - depletionAmount > minimumLevel ? fuel - depletionAmount : minimumLevel;
	}
	/**
	 * Method Name: UpdateLanternRange
	 * Description: Method updates the light rande of the lantern.
	 */ 
	private void UpdateLanternRange() {
		//Update the range of the light.
		lantern.range = fuel;
		//Update the amount of fuel in the animator.
		animator.SetFloat("Fuel", fuel);
	}
	/**
	 * Method Name: DisableLantern
	 * Description: Method destroys the lantern upton kiwi death.
	 */ 
	public void DisableLantern() {
		//Destroy the lantern.
		Destroy(this.gameObject);
	}
}