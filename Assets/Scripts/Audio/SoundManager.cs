using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    [Header("Listening on channels")]
    [Tooltip("The SoundManager listens to this event, fired by objects in any scene, to play SFXs")]
    [SerializeField] private AudioCueEventChannelSO _SFXEventChannel = default;
    [Tooltip("The SoundManager listens to this event, fired by objects in any scene, to play Music")]
    [SerializeField] private AudioCueEventChannelSO _musicEventChannel = default;
    [Tooltip("The SoundManager listens to this event, fired by objects in any scene, to change SFXs volume")]
    [SerializeField] private FloatEventChannelSO _SFXVolumeEventChannel = default;
    [Tooltip("The SoundManager listens to this event, fired by objects in any scene, to change Music volume")]
    [SerializeField] private FloatEventChannelSO _musicVolumeEventChannel = default;
    [Tooltip("The SoundManager listens to this event, fired by objects in any scene, to change Master volume")]
    [SerializeField] private FloatEventChannelSO _masterVolumeEventChannel = default;


    [Header("Audio control")]
    [SerializeField] private AudioMixer audioMixer = default;
    [Range(0f, 1f)]
    [SerializeField] private float _masterVolume = 1f;
    [Range(0f, 1f)]
    [SerializeField] private float _musicVolume = 1f;
    [Range(0f, 1f)]
    [SerializeField] private float _sfxVolume = 1f;

    [Header("Audio source")]
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private GameObject sfxSourceRoot;

    private List<AudioSource> listSFXSource = new List<AudioSource>();

    private void Start()
    {
        SetupSFXSource();
    }

    private void OnEnable()
    {
        _SFXEventChannel.OnAudioCuePlayRequested += PlayAudioCue;
        _musicEventChannel.OnAudioCuePlayRequested += PlayMusic;

    }

    private void OnDisable()
    {
        _SFXEventChannel.OnAudioCuePlayRequested -= PlayAudioCue;
        _musicEventChannel.OnAudioCuePlayRequested -= PlayMusic;
    }

    private void SetupSFXSource()
    {
        for(int i=0; i<5;i++)
        {
            AddSFXSource();
        }
    }

    private void PlayAudioCue(AudioCueSO audioCue, AudioConfigurationSO config, Vector3 positon = default)
    {
        AudioClip[] clipsToPlay = audioCue.GetClips();

        AudioSource availableSource = null;

        for(int i=0; i<clipsToPlay.Length; i++)
        {
            for (int j = 0; j < listSFXSource.Count; j++)
            {
                if (listSFXSource[j].isPlaying == false)
                {
                    availableSource = listSFXSource[j];
                }
            }
            if (availableSource == null)
            {
                availableSource = AddSFXSource();
            }
            availableSource.clip = clipsToPlay[i];
            config.ApplyTo(availableSource);
            availableSource.transform.position = positon;
            availableSource.time = 0f; //Reset in case this AudioSource is being reused for a short SFX after being used for a long music track
            availableSource.Play();
        }
    }

    private void PlayMusic(AudioCueSO audioCue, AudioConfigurationSO config, Vector3 positon = default)
    {
        if (audioCue == null)
            return;

        musicSource.Stop();
        musicSource.clip = audioCue.GetClips()[0];
        config.ApplyTo(musicSource);
        musicSource.transform.position = positon;
        musicSource.loop = audioCue.looping;
        musicSource.time = 0f; //Reset in case this AudioSource is being reused for a short SFX after being used for a long music track
        musicSource.Play();
    }

    private AudioSource AddSFXSource()
    {
        AudioSource newAudioSource = new GameObject().AddComponent<AudioSource>();
        newAudioSource.transform.SetParent(sfxSourceRoot.transform);
        listSFXSource.Add(newAudioSource);
        return newAudioSource;
    }
}
