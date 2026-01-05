using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class Lander : MonoBehaviour
{
    private Rigidbody2D Landerrigidbody2;
    public float thrust = 700f;
    public float torquePower = 100f;
    public float softLandingVelocity = 4f;
    public float dotVector;
    public float minDotVector = .90f;

    private void Awake()
    {
        Landerrigidbody2 = GetComponent<Rigidbody2D>();

    }

    // Update is called depent on time run
    private void FixedUpdate()
    {
        //input manager 
        //if (Input.GetKey(KeyCode.Escape))
        //{
        //    Debug.Log("fly");
        //}

        //input syterm
        if (Keyboard.current.spaceKey.isPressed)
        {
            //Landerrigidbody2.AddForce(transform.up * Time.deltaTime);
            Landerrigidbody2.AddForce(transform.up * thrust);
        }
        if (Keyboard.current.dKey.isPressed)
        {
            //Landerrigidbody2.AddTorque(+5 * Time.deltaTime);
            Landerrigidbody2.AddTorque(-torquePower);
        }
        if (Keyboard.current.aKey.isPressed)
        {
            //Landerrigidbody2.AddTorque(-5 * Time.deltaTime);
            Landerrigidbody2.AddTorque(torquePower);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision2D)
    {
        if (!collision2D.gameObject.TryGetComponent(out LandingPad landingPad))
        {
            Debug.Log($"Not the pad");
            return;
        }
        float relativeVelocityMagnitude = collision2D.relativeVelocity.magnitude;
        if (relativeVelocityMagnitude > softLandingVelocity)
        {
            Debug.Log($"not oke {collision2D.relativeVelocity.magnitude}");
            return;
        }
        dotVector = Vector2.Dot(Vector2.up, transform.up);
        if (dotVector < minDotVector)
        {
            Debug.Log("land too hard");
            return;
        }
        Debug.Log($"oke {collision2D.relativeVelocity.magnitude}");

        float maxScoreAmountLandingAngle = 100;
        float scoreDotVectorMutiplier = 10f;
        float LandingAngleScore = maxScoreAmountLandingAngle - Mathf.Abs(dotVector - 1f) * scoreDotVectorMutiplier * maxScoreAmountLandingAngle;
        float maxScoreAmountLandingSpeed = 100;
        float LangdingSpeedScore = (softLandingVelocity - relativeVelocityMagnitude) * maxScoreAmountLandingSpeed;

        Debug.Log($"LandingAngleScore: {LandingAngleScore}");
        Debug.Log($"LangdingSpeedScore: {LangdingSpeedScore}");
    }
}
