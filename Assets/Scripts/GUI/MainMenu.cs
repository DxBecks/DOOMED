using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {

	// Update is called once per frame
	void Update () {
		//If the Start button is pressed...
		if(Input.GetButtonDown("Start")) {
			//...Load the demo level...
			GameController.Instance.LoadLevel("001 - Demo Level", 5.0f);
		}
		//If the escape button is pressed...
		else if(Input.GetButtonDown("Escape")) {
			Debug.Log("Abandon All Hope Ye Who Enter Here");
			//...Quit the application...
			Application.Quit();
		}
	}
}