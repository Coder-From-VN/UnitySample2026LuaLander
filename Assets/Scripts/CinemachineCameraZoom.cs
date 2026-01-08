using Unity.Cinemachine;
using UnityEngine;

public class CinemachineCameraZoom : MonoBehaviour
{
    public static CinemachineCameraZoom instance { get; private set; }
    private const float Normal_Cam_Size = 10f;
    private float _tagetOrthographicSize = 10f;
    [SerializeField] private CinemachineCamera cinemachineCamera;
    [SerializeField] private float ZoomSpeed = 2f;
    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        cinemachineCamera.Lens.OrthographicSize = Mathf.Lerp(cinemachineCamera.Lens.OrthographicSize, _tagetOrthographicSize, Time.deltaTime * ZoomSpeed);
    }

    public void SettagetOrthographicSize(float tagetOrthographicSize)
    {
        _tagetOrthographicSize = tagetOrthographicSize;
    }

    public void SetNormalCam()
    {
        SettagetOrthographicSize(Normal_Cam_Size);
    }
}
