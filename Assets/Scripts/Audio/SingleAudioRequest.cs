using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleAudioRequest : MonoBehaviour
{
    [SerializeField] private AudioCueEventChannelSO _audioEventChannel = default;
    [SerializeField] private AudioConfigurationSO _audioConfig = default;
    //[SerializeField] private GameState 

    [SerializeField] private AudioCueSO _audioCue;
    [SerializeField] private bool _playOnAwake;

    private void Awake()
    {
        if (_playOnAwake)
        {
            Request();
        }
    }

    public void Request() => PlayAudio(_audioCue, _audioConfig, transform.position);

    protected void PlayAudio(AudioCueSO audioCue, AudioConfigurationSO audioConfiguration, Vector3 positionInSpace = default)
    {
        _audioEventChannel.Raise(audioCue, audioConfiguration, positionInSpace);
    }
}
