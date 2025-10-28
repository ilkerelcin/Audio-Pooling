using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class AudioPoolManager : MonoBehaviour
{
    public static AudioPoolManager Instance;
    public GameObject audioSourcePrefab;
    public int poolSize;
    private List<AudioSource> pool;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        pool = new List<AudioSource>();
        for (int i = 0; i <poolSize; i++)
        {
            CreateNewAudioSource();
        }
    }

    public void PlaySound(AudioClip clip, Vector3 position, float volume = 1f)
    {
        if (clip == null)
        {
            Debug.LogWarning("NULL clip!");
            return;
        }

        AudioSource source = GetAvailableSource();
        if (source != null)
        {
            source.transform.position = position;
            source.clip = clip;
            source.volume = volume;
            source.Play();
            StartCoroutine(DisableAfterPlay(source, clip.length));
        }
        else
        {
            Debug.LogWarning("No available AudioSource!");
        }
    }

    private AudioSource GetAvailableSource()
    {
        foreach (var s in pool)
        {
            if (!s.isPlaying)
                return s;
        }

        return CreateNewAudioSource();
    }

    private AudioSource CreateNewAudioSource()
    {
        GameObject obj = Instantiate(audioSourcePrefab, transform);
        AudioSource source = obj.GetComponent<AudioSource>();
        source.playOnAwake = false;
        obj.SetActive(true);
        pool.Add(source);
        return source;
    }

    private IEnumerator DisableAfterPlay(AudioSource source, float duration)
    {
        yield return new WaitForSeconds(duration);

        if (!source.isPlaying)
        {
            source.Stop();
            source.clip = null;
        }
    }
}
