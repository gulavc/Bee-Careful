using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffectPlayer : MonoBehaviour {

    public AudioSource soundSourcePrefab;
    public AudioClip defaultMusic;
    [Range(0, 1)]
    public float musicVolume; 

    private Queue<AudioSource> soundSourcePool;
    private AudioSource musicSource;


	// Use this for initialization
	void Start () {
        soundSourcePool = new Queue<AudioSource>();
        musicSource = GetSourceFromPool();
        musicSource.volume = musicVolume;
        PlayMusic(defaultMusic);
    }
	
	public void PlaySound(AudioClip toPlay, float volume = 1f)
    {
        StartCoroutine(WaitForPlaySound(toPlay, volume));
    }

    IEnumerator WaitForPlaySound(AudioClip toPlay, float volume)
    {
        AudioSource src = GetSourceFromPool();
        src.volume = volume;
        src.PlayOneShot(toPlay);
        while (src.isPlaying)
        {
            yield return new WaitForEndOfFrame();
        }
        Reclaim(src);
    }

    public void PlayMusic(AudioClip music)
    {
        if(musicSource.clip != music)
        {
            musicSource.Stop();
            musicSource.loop = true;
            musicSource.clip = music;
            musicSource.Play();
        }
        
    }

    public void StopMusic()
    {
        musicSource.Stop();
        musicSource.loop = true;
        musicSource.clip = defaultMusic;
        musicSource.Play();
    }

    private AudioSource GetSourceFromPool()
    {
        if(soundSourcePool.Count > 0)
        {
            AudioSource src = soundSourcePool.Dequeue();
            src.gameObject.SetActive(true);
            return src;
        }

        AudioSource src2 = Instantiate(soundSourcePrefab);
        src2.transform.SetParent(this.transform);
        return src2;

    }

    private void Reclaim(AudioSource src)
    {
        src.gameObject.SetActive(false);
        soundSourcePool.Enqueue(src);
    }
}
