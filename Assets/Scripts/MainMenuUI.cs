using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private Button playButton;
    [SerializeField] private Button quitButton;

    public void Awake()
    {
        Time.timeScale = 1f;
        playButton.onClick.AddListener(
            () =>
            {
                GameManager.ResetData();
                SceneLoader.LoadScene(SceneLoader.Scene.GameScene);
            }
            );
        quitButton.onClick.AddListener(
            () =>
            {
                Debug.Log("Da quit thanh cong");
                Application.Quit();
            }
            );
    }

    private void Start()
    {
        playButton.Select();
    }
}
