using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TODO: Check which settings we really need at this level
[CreateAssetMenu(menuName = "Audio/Audio Configuration")]
public class AudioConfigurationSO : ScriptableObject
{

    [Header("Sound properties")]
    public bool Mute = false;
    [Range(0f, 1f)] public float Volume = 1f;
    [Range(-3f, 3f)] public float Pitch = 1f;

    [Header("Spatialisation")]
    [Range(0f, 1f)] public float SpatialBlend = 1f;

    public void ApplyTo(AudioSource audioSource)
    {
        //audioSource.outputAudioMixerGroup = this.OutputAudioMixerGroup;
        audioSource.mute = this.Mute;
        //audioSource.bypassEffects = this.BypassEffects;
        //audioSource.bypassListenerEffects = this.BypassListenerEffects;
        //audioSource.bypassReverbZones = this.BypassReverbZones;
        //audioSource.priority = this.Priority;
        audioSource.volume = this.Volume;
        audioSource.pitch = this.Pitch;
        //audioSource.panStereo = this.PanStereo;
        //audioSource.spatialBlend = this.SpatialBlend;
        //audioSource.reverbZoneMix = this.ReverbZoneMix;
        //audioSource.dopplerLevel = this.DopplerLevel;
        //audioSource.spread = this.Spread;
        //audioSource.rolloffMode = this.RolloffMode;
        //audioSource.minDistance = this.MinDistance;
        //audioSource.maxDistance = this.MaxDistance;
        //audioSource.ignoreListenerVolume = this.IgnoreListenerVolume;
        //audioSource.ignoreListenerPause = this.IgnoreListenerPause;
    }
}
