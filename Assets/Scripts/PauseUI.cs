using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PauseUI : MonoBehaviour
{
    [SerializeField] private Button continuteButton;
    [SerializeField] private Button MainMenuButton;
    [SerializeField] private Button MusicVolumeButton;
    [SerializeField] private TextMeshProUGUI MusicVolumeText;
    [SerializeField] private Button SoundVolumeButton;
    [SerializeField] private TextMeshProUGUI SoundVolumeText;

    private void Awake()
    {
        MusicVolumeButton.onClick.AddListener(
            () =>
            {
                SoundManager.instance.ChangeSoundVolume();
                SoundVolumeText.text = "SOUND: " + SoundManager.instance.GetSoundVolume();
            }
            );
        SoundVolumeButton.onClick.AddListener(
            () =>
            {
                MusicManager.instance.ChangeMusicVolume();
                MusicVolumeText.text = "MUSIC: " + SoundManager.instance.GetSoundVolume();
            }
            );
        continuteButton.onClick.AddListener(
            () =>
            {
                GameManager.Instance.ContinuteTheGame();
            }
            );

        MainMenuButton.onClick.AddListener(
            () =>
            {
                SceneLoader.LoadScene(SceneLoader.Scene.MainMenuScene);
            }
            );
    }

    private void Start()
    {
        continuteButton.Select();
        GameManager.Instance.OnGamePause += GameManager_OnGamePause;
        GameManager.Instance.OnGameUnPause += GameManager_OnGameUnPause;
        SoundVolumeText.text = "SOUND: " + SoundManager.instance.GetSoundVolume();
        MusicVolumeText.text = "MUSIC: " + MusicManager.instance.GetMusicVolume();
        Hide();
    }

    private void GameManager_OnGameUnPause(object sender, EventArgs e)
    {
        Hide();
    }

    private void GameManager_OnGamePause(object sender, EventArgs e)
    {
        show();
    }

    private void show()
    {
        gameObject.SetActive(true);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
