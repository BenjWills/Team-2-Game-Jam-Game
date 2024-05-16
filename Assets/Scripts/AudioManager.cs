using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public AudioMixer audioMixer;
    public Sound[] sounds;
    public Sound[] walkSounds;
    public Sound[] pWalkSounds;
    //public Sound[] music;
    public static AudioManager Instance { get; private set; }
    //public AudioSource MusicSource;
    GameManagerStateMachine gameManager;

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManagerStateMachine>();
        DontDestroyOnLoad(this);
        if (Instance != null && Instance != this)

        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        foreach (Sound s in sounds)
        {
            s.audioSource = gameObject.AddComponent<AudioSource>();
            s.audioSource.clip = s.clip;
            s.audioSource.volume = s.volume;
            s.audioSource.pitch = s.pitch;
            s.audioSource.loop = s.loop;
            s.audioSource.playOnAwake = s.playOnAwake;
            s.audioSource.panStereo = s.direction;
            s.audioSource.outputAudioMixerGroup = s.mixerGroup;
            
        }
        foreach (Sound s in walkSounds)
        {
            s.audioSource = gameObject.AddComponent<AudioSource>();
            s.audioSource.clip = s.clip;
            s.audioSource.volume = s.volume;
            s.audioSource.pitch = s.pitch;
            s.audioSource.loop = s.loop;
            s.audioSource.playOnAwake = s.playOnAwake;
            s.audioSource.panStereo = s.direction;
            s.audioSource.outputAudioMixerGroup = s.mixerGroup;
        }
        foreach (Sound s in pWalkSounds)
        {
            s.audioSource = gameObject.AddComponent<AudioSource>();
            s.audioSource.clip = s.clip;
            s.audioSource.volume = s.volume;
            s.audioSource.pitch = s.pitch;
            s.audioSource.loop = s.loop;
            s.audioSource.playOnAwake = s.playOnAwake;
            s.audioSource.panStereo = s.direction;
            s.audioSource.outputAudioMixerGroup = s.mixerGroup;
        }
    }
    private void Start()
    {
        foreach (Sound s in sounds)
        {
            if (s.audioSource.playOnAwake)
            {
                s.audioSource.Play();
            }
        }
    }


    private void Update()
    {
        //HandleMusic();
    }


    public void Play(string name)
    {
        try
        {
            Sound s = Array.Find(sounds, sound => sound.name == name);
            s.audioSource.Play();
        }
        catch (Exception e)
        {
            Debug.Log($"{e.Message}");
            throw;
        }
    }
    public void PlayWalk()
    {
        try
        {
            Sound s = walkSounds[UnityEngine.Random.Range(0, walkSounds.Length)];
            s.audioSource.Play();
        }
        catch (Exception e)
        {
            Debug.Log($"{e.Message}");
            throw;
        }
    }

    public void PlayPWalk()
    {
        try
        {
            Sound s = pWalkSounds[UnityEngine.Random.Range(0, pWalkSounds.Length)];
            s.audioSource.Play();
        }
        catch (Exception e)
        {
            Debug.Log($"{e.Message}");
            throw;
        }
    }

    public void Stop(string name)
    {
        try
        {
            Sound s = Array.Find(sounds, sound => sound.name == name);
            s.audioSource.Stop();
        }
        catch (Exception e)
        {
            Debug.Log($"{e.Message}");
            throw;
        }
    }

    //private void HandleMusic()
    //{
    //    if (playMusic)
    //    {
    //        MusicUp();
    //    }
    //    else
    //    {
    //        MusicDown();
    //    }
    //}

    //public void PlayMusic(string name)
    //{
    //    try
    //    {
    //        Sound s = Array.Find(music, sound => sound.name == name);
    //        MusicSource.clip = s.clip;
    //        playMusic = true;
    //        MusicSource.Play();
    //    }
    //    catch (Exception e)
    //    {
    //        Debug.Log($"{e.Message}");
    //        Debug.Log(name);
    //        throw;
    //    }
    //}

    //public void StopMusic()
    //{
    //    playMusic = false;
    //}

    //private void MusicUp()
    //{
    //    if(!gameManager.inMenu)
    //    {
    //        if (MusicSource.volume < .1f)
    //        {
    //            MusicSource.volume += .05f * Time.deltaTime;
    //        }
    //        else
    //        {
    //            MusicSource.volume = .1f;
    //        }
    //    }
    //    else
    //    {
    //        MusicSource.volume = .1f;
    //    }
    //}
    //private void MusicDown()
    //{
    //    if (MusicSource.volume > 0)
    //    {
    //        MusicSource.volume -= .05f * Time.deltaTime;
    //    }
    //    else
    //    {
    //        MusicSource.volume = 0f;
    //    }
    //}
}