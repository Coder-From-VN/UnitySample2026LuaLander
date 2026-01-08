using System;
using UnityEngine;

public class LanderAudio : MonoBehaviour
{
    [SerializeField] private AudioSource ThrusterAudioSource;

    private Lander lander;

    private void Awake()
    {
        lander = GetComponent<Lander>();
    }

    private void Start()
    {
        lander.OnBeforeForce += Lander_OnBeforeForce;
        lander.OnRForce += Lander_OnRForce;
        lander.OnLForce += Lander_OnLForce;
        lander.OnUpForce += Lander_OnUpForce;

        SoundManager.instance.OnSoundChange += SM_OSC;

        ThrusterAudioSource.Pause();
    }

    private void SM_OSC(object sender, EventArgs e)
    {
        ThrusterAudioSource.volume = SoundManager.instance.GetSoundVolumeNormalized();
    }

    private void Lander_OnRForce(object sender, EventArgs e)
    {
        if (!ThrusterAudioSource.isPlaying)
        {
            ThrusterAudioSource.Play();
        }
    }
    private void Lander_OnLForce(object sender, EventArgs e)
    {
        if (!ThrusterAudioSource.isPlaying)
        {
            ThrusterAudioSource.Play();
        }

    }
    private void Lander_OnUpForce(object sender, EventArgs e)
    {
        if (!ThrusterAudioSource.isPlaying)
        {
            ThrusterAudioSource.Play();
        }
    }

    private void Lander_OnBeforeForce(object sender, EventArgs e)
    {
        ThrusterAudioSource.Pause();
    }
}
