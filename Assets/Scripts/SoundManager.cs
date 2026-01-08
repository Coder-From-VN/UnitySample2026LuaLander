using System;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private static int SoundVolume = 6;
    private const int SoundVolumeMax = 10;
    [SerializeField] private AudioClip fuelPickUpSound;
    [SerializeField] private AudioClip coinPickUpSound;
    [SerializeField] private AudioClip LandedOkeSound;
    [SerializeField] private AudioClip LandedMotOkeSound;

    public event EventHandler OnSoundChange;

    public static SoundManager instance;

    private void Start()
    {
        Lander.Instance.OnFuelPickUp += Lander_OnFuelPickUp;
        Lander.Instance.OnCoinPickUp += Lander_OnCoinPickUp;
        Lander.Instance.OnLanded += Lander_OnLanded;
    }

    private void Awake()
    {
        instance = this;
    }

    private void Lander_OnLanded(object sender, Lander.OnLandedEvenArgs e)
    {
        switch (e._landingType)
        {
            case Lander.LandingType.Oke:
                AudioSource.PlayClipAtPoint(LandedOkeSound, Camera.main.transform.position, GetSoundVolumeNormalized());
                break;
            default:
                AudioSource.PlayClipAtPoint(LandedMotOkeSound, Camera.main.transform.position, GetSoundVolumeNormalized());
                break;
        }
    }

    private void Lander_OnCoinPickUp(object sender, EventArgs e)
    {
        AudioSource.PlayClipAtPoint(coinPickUpSound, Camera.main.transform.position, GetSoundVolumeNormalized());
    }

    private void Lander_OnFuelPickUp(object sender, EventArgs e)
    {
        AudioSource.PlayClipAtPoint(fuelPickUpSound, Camera.main.transform.position, GetSoundVolumeNormalized());
    }

    public void ChangeSoundVolume()
    {
        OnSoundChange?.Invoke(this, EventArgs.Empty);
        SoundVolume = (SoundVolume + 1) % SoundVolumeMax;
    }

    public int GetSoundVolume()
    {
        return SoundVolume;
    }

    public float GetSoundVolumeNormalized()
    {
        return ((float)SoundVolume) / SoundVolumeMax;
    }

}
