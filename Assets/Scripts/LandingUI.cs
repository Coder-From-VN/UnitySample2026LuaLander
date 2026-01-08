using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LandingUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI TittleTextMesh;
    [SerializeField] private TextMeshProUGUI statsTextMesh;
    [SerializeField] private TextMeshProUGUI nextButtonTextMesh;
    [SerializeField] private Button nextButton;


    private Action nextButtonClickAction;

    private void Awake()
    {
        nextButton.onClick.AddListener(() =>
        {
            nextButtonClickAction();
        });
    }

    private void Start()
    {
        nextButton.Select();
        Lander.Instance.OnLanded += Lander_OnLanded;
        Hide();
    }

    private void Lander_OnLanded(object sender, Lander.OnLandedEvenArgs e)
    {
        if (e._landingType == Lander.LandingType.Oke)
        {
            TittleTextMesh.text = "OKE NGON";
            nextButtonTextMesh.text = "Tiep";
            nextButtonClickAction = GameManager.Instance.GotoNextLevel;
        }
        else
        {
            TittleTextMesh.text = "<color=#ff0000>CRASH!</color>";
            nextButtonTextMesh.text = "Thu lai";
            nextButtonClickAction = GameManager.Instance.ReTryLevel;
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
