using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {
	/*DATA MEMBERS*/
	
	//Instantiate public class level objects.
	public GameObject target;	//The target object to follow.

	//Instantiate private class level objects.
	private Vector3 offset; //The offest of the camera from the target

	/**
	 * Method Name: Start
	 * Description: Method executes once.
	 */ 
	void Start () {
		//Initialize the offset to the difference between the camera position and the target location.
		offset = transform.position - target.transform.position;
	}
	/**
	 * Method Name: Update
	 * Description: Method executes once per frame.
	 */ 
	void Update () {
		//Update the position of the camera to the target's position plus the offset.
		this.transform.position = target.transform.position + offset;
	}
}