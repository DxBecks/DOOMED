using UnityEngine;
using System.Collections;

public class Credits : MonoBehaviour {

	/*DATA MEMBERS*/

	//Declare private class level variables.
	[SerializeField] private float scrollSpeed;	//The scroll speed of the credits per second.
	/**
	 * Method Name: Start
	 * Description: Start method executes once upon script initialization.
	 */ 
	void Start() {
		//Start the Scroll coroutine.
		StartCoroutine("Scroll");
	}
	/**
	 * Coroutine Name: Scroll
	 * Description   : Coroutine enables the credits to scroll/crawl vertically up the screen.
	 */ 
	private IEnumerator Scroll() {
		//Yield for a moment before scrolling.
		yield return new WaitForSeconds(4.0f);
		//While true... (enables code to execute every frame).
		while(this.transform.localPosition.y < 1500) {
			//...Translate the credits vertically.
			this.transform.Translate(transform.up * scrollSpeed * Time.deltaTime, Space.World);
			//...Yield until the next frame before next loop execution.
			yield return 0;
		}
		//Load the main menu.
		GameController.Instance.LoadLevel("000 - Main Menu", 2.0f);
	}
}
/*End of Script*/