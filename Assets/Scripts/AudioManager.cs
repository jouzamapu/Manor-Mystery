using UnityEngine;
using System;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private Sound[] sounds;
    public static AudioManager instance;
    
    void Awake()
    {
        if(instance == null) instance=this;
        else
        {
            Destroy(gameObject);
            return;
        }
        
        DontDestroyOnLoad(gameObject);
        
        foreach(Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
            s.source.outputAudioMixerGroup = s.audioMixer;
        }
    }

    void Start() => Play("RainyAmbience");

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);

        if(s == null) return;
        
        s.source.Play();
    }

    public void Stop(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);

        if(s == null) return;
        
        s.source.Stop();
    }
}

[System.Serializable]
public class Sound 
{
    public string name;
    public AudioClip clip;
    [Range(0.0f,1.0f)]
    public float volume;
    [Range(0.1f,3.0f)]
    public float pitch;
    public bool loop;
    [HideInInspector] public AudioSource source;
    public AudioMixerGroup audioMixer;
}