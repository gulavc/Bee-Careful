using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiAudioClip : MonoBehaviour {

    public AudioClip[] sounds;

    public AudioClip GetRandomSound()
    {
        int rand = Random.Range(0, sounds.Length);
        return sounds[rand];
    }

}
