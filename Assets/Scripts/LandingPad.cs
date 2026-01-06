using UnityEngine;

public class LandingPad : MonoBehaviour
{
    [SerializeField] private int scroceMultiplier;

    public int GetScoreM()
    {
        return scroceMultiplier;
    }
}
