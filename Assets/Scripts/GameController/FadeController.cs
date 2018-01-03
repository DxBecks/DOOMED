using UnityEngine;
using System.Collections;

public class FadeController : Singleton<FadeController> {

	/*DATA MEMBERS*/
	[SerializeField] private CanvasRenderer canvasRenderer;

	/**
	 * Method Name: Start
	 * Description: Method executes once after the Awake method.
	 */ 
	void Start() {
		canvasRenderer = GetComponentInChildren<CanvasRenderer>();
	}
	/**
	 * Method Name: OnLevelWasLoaded
	 * Description: Method handles behavior when a new level is loaded.
	 * @param index.
	 */ 
	void OnLevelWasLoaded(int index) {
		//If the index of the current level is the loaded level...
		if (index == Application.loadedLevel) {
			//...update the canvas renderer.
			canvasRenderer = GetComponentInChildren<CanvasRenderer> ();
		}
	}
	/**
	 * Method Name: SceneFadeIn
	 * Description: Method calls the SceneFadeIn coroutine.
	 * @param duration.
	 */ 
	public void SceneFadeIn(float duration) {
		//Start the SceneFadeIn coroutine.
		StartCoroutine(SceneFadeInCoroutine(duration));
	}
	/**
	 * Method Name: SceneFadeOut
	 * Description: Method calls the SceneFadeOut coroutine.
	 * @param duration.
	 */ 
	public void SceneFadeOut(float duration) {
		//Start the SceneFadeOut coroutine.
		StartCoroutine(SceneFadeOutCoroutine(duration));
	}
	/**
	 * Coroutine Name: SceneFadeInCoroutine
	 * Description	 : Coroutine handles behavior for fading in a scene.
	 * @param duration
	 */ 
	private IEnumerator SceneFadeInCoroutine(float duration) {
		//Set the alpha of the Canvas Renderer to completely black.
		canvasRenderer.SetAlpha(1.0f);
		//Wait one second before starting fade in.
		yield return new WaitForSeconds(1.0f);
		//While the alpha value of the image is greater than zero fade...
		while(canvasRenderer.GetAlpha() > 0.0f) {
			//...Decrease the alpha value of the image...
			canvasRenderer.SetAlpha(canvasRenderer.GetAlpha() - Time.smoothDeltaTime / duration);
			//Print the alpha value of the CanvasRenderer.
			//Debug.Log(canvasRenderer.GetAlpha());
			//...Yield until next frame update before continuing loop execution...
			yield return 0;
		}
	}
	/**
	 * Coroutine Name: SceneFadeOutCoroutine
	 * Description	 : Coroutine handles behavior for fading out a scene.
	 * @param duration
	 */ 
	private IEnumerator SceneFadeOutCoroutine(float duration) {
		//Set the alpha value of the image to fully transparent.
		canvasRenderer.SetAlpha(0.0f);
		//Wait one second before starting fade out.
		yield return new WaitForSeconds(1.0f);
		//While the alpha value fo the image is less than one fade out...
		while(canvasRenderer.GetAlpha() < 1.0f) {
			//...Increase the alpha value of the image...
			canvasRenderer.SetAlpha(canvasRenderer.GetAlpha() + Time.smoothDeltaTime / duration);
			//Print the alpha value of the CanvasRenderer.
			//Debug.Log(canvasRenderer.GetAlpha());
			//...Yield until the next frame update before continuing loop execution...
			yield return 0;
		}
	}
}