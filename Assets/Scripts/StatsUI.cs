using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class StatsUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI statsTextMesh;
    [SerializeField] private GameObject statsLeftGameObject;
    [SerializeField] private GameObject statsRightGameObject;
    [SerializeField] private GameObject SpeedUpArrowGameObject;
    [SerializeField] private GameObject SpeedDownArrowGameObject;
    [SerializeField] private Image fuelImage;

    private void FixedUpdate()
    {
        UpdateTextMesh();
    }

    private void UpdateTextMesh()
    {
        SpeedUpArrowGameObject.SetActive(Lander.Instance.GetSpeedY() >= 0);
        SpeedDownArrowGameObject.SetActive(Lander.Instance.GetSpeedY() < 0);
        statsRightGameObject.SetActive(Lander.Instance.GetSpeedY() >= 0);
        statsLeftGameObject.SetActive(Lander.Instance.GetSpeedY() < 0);

        fuelImage.fillAmount = Lander.Instance.GetFeulAmountNormalize();

        statsTextMesh.text = GameManager.Instance.GetScore() + "\n" +
                             Mathf.Round(GameManager.Instance.GetTimmer()) + "\n" +
                             Mathf.Abs(Mathf.Round(Lander.Instance.GetSpeedX() * 10f)) + "\n" +
                             Mathf.Abs(Mathf.Round(Lander.Instance.GetSpeedY() * 10f));
    }
}
