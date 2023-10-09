using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class STM_CharacterAudio : MonoBehaviour
{
    [SerializeField] private AudioCueEventChannelSO _sfxEventChannel = default;
    [SerializeField] private AudioConfigurationSO _sfxConfig = default;
    //[SerializeField] private GameState 

    [SerializeField] private AudioCueSO _shoot;
    [SerializeField] private AudioCueSO _land;
    [SerializeField] private AudioCueSO _die;
    [SerializeField] private AudioCueSO _hitGoal;

    protected void PlayAudio(AudioCueSO audioCue, AudioConfigurationSO audioConfiguration, Vector3 positionInSpace = default)
    {
        _sfxEventChannel.Raise(audioCue, audioConfiguration, positionInSpace);
    }

    public void PlayShoot() => PlayAudio(_shoot, _sfxConfig, transform.position);
    public void PlayLand() => PlayAudio(_land, _sfxConfig, transform.position);
    public void PlayDie() => PlayAudio(_die, _sfxConfig, transform.position);
    public void PlayHitGoal() => PlayAudio(_hitGoal, _sfxConfig, transform.position);
}
