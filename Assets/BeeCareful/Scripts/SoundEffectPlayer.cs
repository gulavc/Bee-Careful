using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffectPlayer : MonoBehaviour {

    private AudioSource soundSource;

	// Use this for initialization
	void Start () {
        soundSource = gameObject.AddComponent<AudioSource>();
	}
	
	public void PlaySound(AudioClip toPlay)
    {
        soundSource.PlayOneShot(toPlay);
    }
}
