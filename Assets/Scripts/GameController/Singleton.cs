/*Import Statements*/
using UnityEngine;
using System.Collections;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour {

	/*DATA MEMBERS*/
	private static T _instance;	//The instance of the singleton.
	
	public static T Instance {
		get {
			//Check if the instance is null.
			_instance = _instance == null ? GameObject.FindObjectOfType<T>() : _instance;
			//If the instance is null attempt to locate an instance.
			_instance = _instance == null ? (new GameObject(typeof(T).Name)).AddComponent<T>() : _instance;
			//Return the instance of the Singleton.
			return _instance;
		}
	}
	/**
	 * Method Name: Awake
	 * Description: Method is the first to execute, executes only once.
	 */ 
	public virtual void Awake() {
		//If the instance of the singleton is null...
		if (_instance == null) {
			//...Instantiate new instance...
			_instance = this as T;
			//...Don't destroy the instance on load.
			DontDestroyOnLoad(gameObject);
		}
		//...Else...
		else {
			//...Destroy the GameObject.
			Destroy(gameObject);
		}
	}
}