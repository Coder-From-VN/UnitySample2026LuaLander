using UnityEngine;

public class GameLevel : MonoBehaviour
{
    [SerializeField] private int levelNumber;
    [SerializeField] private Transform landerStartPoint;
    [SerializeField] private Transform cameraStartTaget;
    [SerializeField] private float zoomLevel;

    public int GetLevelNUmber()
    {
        return levelNumber;
    }

    public Vector3 GetLanderStartPoint()
    {
        return landerStartPoint.position;
    }

    public Transform GetCameraStartTaget()
    {
        return cameraStartTaget;
    }

    public float GetZoomLevel()
    {
        return zoomLevel;
    }

}
