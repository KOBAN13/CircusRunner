using System;
using InputSystem;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

public class InputSystemPC : MonoBehaviour, IInputSystem
{
    private NewInputSystem _input;
    private Swipe _swipe;
    private IUseInputSystem _classUseInputSystem;

    [Inject]
    public void Construct(NewInputSystem input, CharacterInputController inputSystemUse)
    {
        _input = input ?? throw new ArgumentNullException($"{nameof(input)} is null");
        _classUseInputSystem = inputSystemUse ? inputSystemUse : throw new ArgumentNullException($"{nameof(inputSystemUse)} is null");
        _input.Enable();
    }

    public Swipe Move() => _swipe;

    public void Jump()
    {
        _classUseInputSystem.InvokeJump();
    }

    public void OnEnable()
    {
        _input.Move.MoveLeft.performed += MoveLeft;
        _input.Move.MoveRight.performed += MoveRight;
        
        _input.OtherMove.MoveLeft.performed += MoveLeft;
        _input.OtherMove.MoveRight.performed += MoveRight;
        
        _input.Move.Jump.performed += Jump;
        _input.OtherJump.JumpW.performed += Jump;
        _input.OtherJump.JumpUpArrow.performed += Jump;
    }
    
    public void OnDisable()
    {
        _input.Move.MoveLeft.performed -= MoveLeft;
        _input.Move.MoveRight.performed -= MoveRight;
        _input.Move.Jump.performed -= Jump;
    }
    
    private void Jump(InputAction.CallbackContext obj)
    {
        Jump();
    }
    
    private void MoveRight(InputAction.CallbackContext obj)
    {
        _swipe = Swipe.Right;
        _classUseInputSystem.InvokeMove();
    }

    private void MoveLeft(InputAction.CallbackContext obj)
    {
        _swipe = Swipe.Left;
        _classUseInputSystem.InvokeMove();
    }
    
}
