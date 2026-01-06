using TMPro;
using UnityEngine;

public class LandingPadVisual : MonoBehaviour
{
    [SerializeField]
    private TextMeshPro scoreMTextMesh;
    private void Awake()
    {
        LandingPad landingpad = GetComponent<LandingPad>();
        scoreMTextMesh.text = "x" + landingpad.GetScoreM();
    }
}
