using System;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;

public class Lander : MonoBehaviour
{
    private Rigidbody2D Landerrigidbody2d;
    [SerializeField] private float Garavity_Normal = 0.7f;
    [SerializeField] private float thrust = 700f;
    [SerializeField] private float torquePower = 100f;
    [SerializeField] private float softLandingVelocity = 4f;
    [SerializeField] private float dotVector;
    [SerializeField] private float minDotVector = .90f;
    [SerializeField] private float maxScoreAmountLandingAngle = 100;
    [SerializeField] private float scoreDotVectorMutiplier = 10f;
    [SerializeField] private float maxScoreAmountLandingSpeed = 100;

    public static Lander Instance
    {
        get; private set;
    }

    //fuel event
    public event EventHandler OnLForce;
    public event EventHandler OnUpForce;
    public event EventHandler OnRForce;
    public event EventHandler OnBeforeForce;


    [SerializeField] private float fuelAmount;
    [SerializeField] private float fuelAmountMax = 1000f;
    [SerializeField] private float addFuel = 100f;
    [SerializeField] private float fuelConsumptionAmount = 1f;

    //coin even
    public event EventHandler OnCoinPickUp;
    public event EventHandler OnFuelPickUp;
    //Land even
    public event EventHandler<OnLandedEvenArgs> OnLanded;
    public class OnLandedEvenArgs : EventArgs
    {
        public LandingType _landingType;
        public int _score;
        public float _dotVector;
        public float _langdingSpeed;
        public float _scoreMutiplite;
    }

    public enum LandingType
    {
        Oke,
        NotOke,
        TooSteepAngle,
        TooFastLanding
    }

    public event EventHandler<OnStateChangeEventArgs> OnStateChange;
    public class OnStateChangeEventArgs : EventArgs
    {
        public State _state;
    }
    public enum State
    {
        WattingToStart,
        Normal,
        GameOver
    }

    private State state;

    private void Awake()
    {
        Instance = this;
        fuelAmount = fuelAmountMax;
        state = State.WattingToStart;

        Landerrigidbody2d = GetComponent<Rigidbody2D>();
        Landerrigidbody2d.gravityScale = 0f;
    }

    // Update is called depent on time run
    private void FixedUpdate()
    {
        //input syterm
        OnBeforeForce?.Invoke(this, EventArgs.Empty);

        switch (state)
        {
            default:
            case State.WattingToStart:
                if (GameInput.instance.IsUpActionPressed() ||
                    GameInput.instance.IsRightActionPressed() ||
                    GameInput.instance.IsLeftActionPressed() ||
                    GameInput.instance.GetMovermentInputV2() != Vector2.zero
                    )
                {
                    Landerrigidbody2d.gravityScale = Garavity_Normal;
                    SetState(State.Normal);
                }
                break;
            case State.Normal:

                if (fuelAmount <= 0f)
                {
                    //no fuel
                    return;
                }

                if (GameInput.instance.IsUpActionPressed() ||
                    GameInput.instance.IsRightActionPressed() ||
                    GameInput.instance.IsLeftActionPressed() ||
                    GameInput.instance.GetMovermentInputV2() != Vector2.zero
                    )
                {
                    ConSumFuel();
                }

                float gamepadDeathzone = .4f;
                if (GameInput.instance.IsUpActionPressed() ||
                    GameInput.instance.GetMovermentInputV2().y > gamepadDeathzone)
                {
                    Landerrigidbody2d.AddForce(transform.up * thrust);
                    OnUpForce?.Invoke(this, EventArgs.Empty);
                }
                if (GameInput.instance.IsRightActionPressed() ||
                    GameInput.instance.GetMovermentInputV2().x < -gamepadDeathzone)
                {
                    Landerrigidbody2d.AddTorque(-torquePower);
                    OnLForce?.Invoke(this, EventArgs.Empty);
                }
                if (GameInput.instance.IsLeftActionPressed() ||
                    GameInput.instance.GetMovermentInputV2().x > gamepadDeathzone)
                {
                    Landerrigidbody2d.AddTorque(torquePower);
                    OnRForce?.Invoke(this, EventArgs.Empty);
                }
                break;
            case State.GameOver:

                break;
        }


    }

    private void OnCollisionEnter2D(Collision2D collision2D)
    {
        if (!collision2D.gameObject.TryGetComponent(out LandingPad landingPad))
        {
            OnLanded?.Invoke(this, new OnLandedEvenArgs
            {
                _landingType = LandingType.NotOke,
                _dotVector = 0f,
                _langdingSpeed = 0f,
                _scoreMutiplite = 0,
                _score = 0
            });
            SetState(State.GameOver);
            return;
        }
        //landing Too Hard
        float relativeVelocityMagnitude = collision2D.relativeVelocity.magnitude;
        if (relativeVelocityMagnitude > softLandingVelocity)
        {
            OnLanded?.Invoke(this, new OnLandedEvenArgs
            {
                _landingType = LandingType.TooFastLanding,
                _dotVector = 0f,
                _langdingSpeed = relativeVelocityMagnitude,
                _scoreMutiplite = 0,
                _score = 0
            });
            SetState(State.GameOver);
            return;
        }
        //landing TooSteepAngle
        dotVector = Vector2.Dot(Vector2.up, transform.up);
        if (dotVector < minDotVector)
        {
            OnLanded?.Invoke(this, new OnLandedEvenArgs
            {
                _landingType = LandingType.TooSteepAngle,
                _dotVector = dotVector,
                _langdingSpeed = relativeVelocityMagnitude,
                _scoreMutiplite = 0,
                _score = 0
            });
            SetState(State.GameOver);
            return;
        }


        float LandingAngleScore = maxScoreAmountLandingAngle - Mathf.Abs(dotVector - 1f) * scoreDotVectorMutiplier * maxScoreAmountLandingAngle;

        float LangdingSpeedScore = (softLandingVelocity - relativeVelocityMagnitude) * maxScoreAmountLandingSpeed;

        int score = Mathf.RoundToInt((LandingAngleScore + LangdingSpeedScore) * landingPad.GetScoreM());

        //lading Oke
        OnLanded?.Invoke(this, new OnLandedEvenArgs
        {
            _landingType = LandingType.Oke,
            _dotVector = dotVector,
            _langdingSpeed = relativeVelocityMagnitude,
            _scoreMutiplite = landingPad.GetScoreM(),
            _score = score
        });
        SetState(State.GameOver);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out FuelPickUp fuelPickUp))
        {
            Debug.Log("Check fuel");
            fuelAmount += addFuel;
            if (fuelAmount > fuelAmountMax)
            {
                fuelAmount = fuelAmountMax;
            }
            OnFuelPickUp.Invoke(this, EventArgs.Empty);
            fuelPickUp.DestroySeft();
        }

        if (collision.gameObject.TryGetComponent(out CoinPickUp CoinPickUp))
        {
            Debug.Log("Check Cion");
            OnCoinPickUp?.Invoke(this, EventArgs.Empty);
            CoinPickUp.DestroySeft();

        }
    }

    private void SetState(State newState)
    {
        state = newState;
        OnStateChange?.Invoke(this, new OnStateChangeEventArgs
        {
            _state = state
        });
    }

    private void ConSumFuel()
    {
        fuelAmount -= fuelConsumptionAmount * Time.deltaTime;
    }

    public float GetSpeedX()
    {
        return Landerrigidbody2d.linearVelocityX;
    }

    public float GetFuel()
    {
        return fuelAmount;
    }

    public float GetSpeedY()
    {
        return Landerrigidbody2d.linearVelocityY;
    }

    public float GetFeulAmountNormalize()
    {
        return fuelAmount / fuelAmountMax;
    }


}
