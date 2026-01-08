using System;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    [SerializeField] private AudioSource MusicAudioSource;
    private static float musicTime;
    private static int musicVolume = 4;
    private const int musicVolumeMax = 10;

    public static MusicManager instance;

    public event EventHandler OnMusicChange;

    private void Awake()
    {
        instance = this;
        MusicAudioSource = GetComponent<AudioSource>();
        MusicAudioSource.time = musicTime;
    }

    private void Start()
    {
        MusicAudioSource.volume = GetMusicVolumeNormalized();
    }

    private void Update()
    {
        musicTime = MusicAudioSource.time;
    }

    public void ChangeMusicVolume()
    {
        OnMusicChange?.Invoke(this, EventArgs.Empty);
        musicVolume = (musicVolume + 1) % musicVolumeMax;
        MusicAudioSource.volume = GetMusicVolumeNormalized();
    }

    public int GetMusicVolume()
    {
        return musicVolume;
    }

    public float GetMusicVolumeNormalized()
    {
        return ((float)musicVolume) / musicVolumeMax;
    }
}
