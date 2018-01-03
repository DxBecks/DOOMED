using UnityEngine;
using System.Collections;

public class AudioController : Singleton<AudioController> {

	/*DATA MEMEMBERS*/

	//Declare private class level constant variables.
	private const float MAXIMUM_VOLUME = 1.0f;	//The maximum level of volume of the audio.

	/**
	 * Method Name: AudioFadeIn
	 * Description: Method starts the AudioFadeInCoroutine with a specified duration.
	 * @param duration
	 */ 
	public void AudioFadeIn(float duration) {
		//Start the Audio Fade In coroutine.
		StartCoroutine(AudioFadeInCoroutine(duration));
	}
	/**
	 * Method Name: AudioFadeOut
	 * Description: Method starts the AudioFadeOutCoroutine with a specified duration.
	 * @param duration
	 */ 
	public void AudioFadeOut(float duration) {
		//Start the Audio Fade Out coroutine.
		StartCoroutine(AudioFadeOutCoroutine(duration));
	}
	/**
	 * Method Name: AudioFadeOut
	 * Description: Method either starts the Audio Fade Out coroutine or the Audio Fade Out Normalized coroutine.
	 * @param sources, duration, normalized.
	 */ 
	public void AudioFadeOut(AudioSource[] sources, float duration, bool normalized) {
		//For each audio source in the sources list...
		foreach(AudioSource source in sources) {
			//...If the sound is normalized...
			if(normalized) {
				//...Start the AudioFadeOutNormalized coroutine.
				StartCoroutine(AudioFadeOutNormalizedCoroutine(source, duration));
			}
			//...Else...
			else{
				//...Start the AudioFadeOut coroutine.
				StartCoroutine(AudioFadeOutCoroutine(duration));
			}
		}
	}
	/**
	 * Coroutine Name: AudioFadeInCoroutine
	 * Description   : Coroutine fades in all audio over a set duration.
	 * @param duration
	 */ 
	private IEnumerator AudioFadeInCoroutine(float duration) {
		//While the audio listener volume is less than maximum volume...
		while (AudioListener.volume < 1) {
			//...Increase the audio listener volume by the time passed divided by the total duration.
			AudioListener.volume += Time.smoothDeltaTime / duration;
			//...Yield until the next frame before continuing execution.
			yield return 0;
		}
	}
	/**
	 * Coroutine Name: AudioFadeOutCoroutine
	 * Description   : Coroutine fades out all audio over a set duration via the audio listener.
	 * @param duration
	 */ 
	private IEnumerator AudioFadeOutCoroutine(float duration) {
		//While the audio listener volume is greater than minimum volume...
		while (AudioListener.volume > 0) {
			//...Decrease the audio listener volume by the time passed divided by the total duration.
			AudioListener.volume -= Time.smoothDeltaTime / duration;
			//...Yield until the next frame before continuing execution.
			yield return 0;
		}
	}
	/**
	 * Coroutine Name: AudioFadeOutCoroutine
	 * Description   : Coroutine fades out a single audio source.
	 * @param source, duration
	 */ 
	private IEnumerator AudioFadeOutCoroutine(AudioSource source, float duration) {
		//While the audio source volume is greater than minimum volume...
		while (source.volume > 0) {
			//...Decrease the audio source volume by the time passed divided by the total duration.
			source.volume -= Time.smoothDeltaTime / duration;
			//...Yield until the next frame before continuing execution.
			yield return 0;
		}
	}
	/**
	 * Coroutine Name: AudioFadeOutNormalizedCoroutine
	 * Description   : Coroutine fades out a single audio source.
	 * @param source, duration
	 */ 
	private IEnumerator AudioFadeOutNormalizedCoroutine(AudioSource source, float duration) {
		//Yield a set amount of seconds before continuing execution to ensure that a louder audio source fades at the same time a quieter one.
		yield return new WaitForSeconds ((MAXIMUM_VOLUME - source.volume) * duration);
		//While the audio source volume is greater than minimum volume...
		while (source.volume > 0) {
			//...Decrease the audio source volume by the time passed divided by the total duration.
			source.volume -= Time.smoothDeltaTime / duration;
			//...Yield until the next frame before continuing execution.
			yield return 0;
		}
	}
}