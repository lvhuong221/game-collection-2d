using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Event on which <c>AudioCue</c> components send a message to play SFX and music. <c>AudioManager</c> listens on these events, and actually plays the sound.
/// </summary>
[CreateAssetMenu(menuName = "Events/AudioCue Event Channel")]
public class AudioCueEventChannelSO : DescriptionBaseSO
{
    public UnityAction<AudioCueSO, AudioConfigurationSO, Vector3> OnAudioCuePlayRequested;

    public void Raise(AudioCueSO audioCue, AudioConfigurationSO config, Vector3 position)
    {
        if(OnAudioCuePlayRequested != null)
        {
            OnAudioCuePlayRequested.Invoke(audioCue, config, position);
        }
    }
}

