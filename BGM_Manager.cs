using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGM_Manager : MonoBehaviour
{
    static public BGM_Manager instance;

    public AudioClip[] clips;
    private AudioSource source;

    private WaitForSeconds waitTime = new WaitForSeconds(0.01f);

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            DontDestroyOnLoad(this.gameObject);
            instance = this;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
    }

    public void Play(int _playMusicTrack)
    {
        source.volume = 1f;
        source.clip = clips[_playMusicTrack];
        source.Play();
    }
    public void SetVolume(float _volume)
    {
        source.volume = _volume;
    }
    public void Pause()
    {
        source.Pause();
    }
    public void UnPause()
    {
        source.UnPause();
    }
    public void Stop()
    {
        source.Stop();
    }
    public void FadeOutMusic()
    {
        StopAllCoroutines();
        StartCoroutine(FadeOutMusicCoroutine());
    }
    IEnumerator FadeOutMusicCoroutine()
    {
        for(float i = 1.0f; i >= 0f; i -= 0.01f)
        {
            source.volume = i;
            yield return waitTime;
        }
    }
    public void FadeInMusic()
    {
        StopAllCoroutines();
        StartCoroutine(FadeInMusicCoroutine());
    }
    IEnumerator FadeInMusicCoroutine()
    {
        for (float i = 0f; i <= 1f; i += 0.01f)
        {
            source.volume = i;
            yield return waitTime;
        }
    }
}
