using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoSingleton<AudioManager>
{
    [SerializeField] private AudioClip[] _soundClips;
    [SerializeField] private AudioSource[] _audioSources;
    [SerializeField] private AudioMixer _mixer;

    private void Start()
    {
     // "MasterVolume"
        _mixer.SetFloat("MasterVolume", 0f);
     // "AmbientVolume"
        _mixer.SetFloat("AmbientVolume", -24.0f);
        _audioSources[0].clip = _soundClips[0];
        _audioSources[0].Play();
     // "SFXVolume"
        _mixer.SetFloat("SFXVolume", 0f);
    }

    public void PlayClick()
    {
        _audioSources[1].Play();
    }
}
