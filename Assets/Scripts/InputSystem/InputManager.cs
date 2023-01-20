using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class InputManager : MonoBehaviour
{
    public static Action onUp;
    public static Action onLeft;
    public static Action onDown;
    public static Action onRight;

    public static InputManager Instance;

    private Inputs currentInput;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }
        Instance = this;

        PlayerInputActions playerInputActions = new();
        playerInputActions.Player.Enable();
        playerInputActions.Player.Up.performed += Up;
        playerInputActions.Player.Left.performed += Left;
        playerInputActions.Player.Down.performed += Down;
        playerInputActions.Player.Right.performed += Right;
    }

    private void Start()
    {
        GameManager.onTick += OnTick;
    }

    private void OnTick()
    {
        if (currentInput != Inputs.None)
        {
            switch(currentInput)
            {
                case Inputs.Up:
                    onUp?.Invoke();
                    break;
                case Inputs.Left:
                    onLeft?.Invoke();
                    break;
                case Inputs.Down:
                    onDown?.Invoke();
                    break;
                case Inputs.Right:
                    onRight?.Invoke();
                    break;
            }
        }
        currentInput = Inputs.None;
    }

    private void Up(InputAction.CallbackContext context)
    {
        if (currentInput == Inputs.Up)
            GameManager.Instance.DoTick();
        else
            currentInput = Inputs.Up;
    }
    private void Left(InputAction.CallbackContext context)
    {
        if (currentInput == Inputs.Left)
            GameManager.Instance.DoTick();
        else
            currentInput = Inputs.Left;
    }
    private void Down(InputAction.CallbackContext context)
    {
        if (currentInput == Inputs.Down)
            GameManager.Instance.DoTick();
        else
            currentInput = Inputs.Down;
    }
    private void Right(InputAction.CallbackContext context)
    {
        if (currentInput == Inputs.Right)
            GameManager.Instance.DoTick();
        else
            currentInput = Inputs.Right;
    }
}
