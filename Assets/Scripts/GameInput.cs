using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{

    public static GameInput instance { get; private set; }

    private InputActions inputActions;

    public event EventHandler OnMenuButtonPressd;

    private void Awake()
    {
        instance = this;
        inputActions = new InputActions();
        inputActions.Enable();

        inputActions.Player.PauseGame.performed += Pause_Performed;
    }

    private void Pause_Performed(InputAction.CallbackContext context)
    {
        OnMenuButtonPressd.Invoke(this, EventArgs.Empty);
    }

    public bool IsUpActionPressed()
    {
        return inputActions.Player.Up.IsPressed();
    }

    public bool IsLeftActionPressed()
    {
        return inputActions.Player.Left.IsPressed();
    }

    public bool IsRightActionPressed()
    {
        return inputActions.Player.Right.IsPressed();
    }

    private void OnDestroy()
    {
        inputActions.Disable();
    }

    public Vector2 GetMovermentInputV2()
    {
        return inputActions.Player.Moverment.ReadValue<Vector2>();
    }

}
