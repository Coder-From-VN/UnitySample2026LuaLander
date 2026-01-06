using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LandingUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI TittleTextMesh;
    [SerializeField] private TextMeshProUGUI statsTextMesh;
    [SerializeField] private Button nextButton;

    private void Awake()
    {
        nextButton.onClick.AddListener(() =>
        {
            SceneManager.LoadScene(0);
        });
    }

    private void Start()
    {
        Lander.Instance.OnLanded += Lander_OnLanded;
        Hide();
    }

    private void Lander_OnLanded(object sender, Lander.OnLandedEvenArgs e)
    {
        if (e._landingType == Lander.LandingType.Oke)
        {
            TittleTextMesh.text = "OKE NGON";
        }
        else
        {
            TittleTextMesh.text = "<color=#ff0000>CRASH!</color>";
        }
        statsTextMesh.text =
            MathF.Round(e._langdingSpeed * 10f) + "\n" +
            MathF.Round(e._dotVector * 10f) + "\n" +
            "x" + e._scoreMutiplite + "\n" +
            e._score;

        Show();
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }

}
