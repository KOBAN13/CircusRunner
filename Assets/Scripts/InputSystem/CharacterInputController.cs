using System;
using Character;
using UnityEngine;
using Zenject;

namespace InputSystem
{
    public class CharacterInputController : MonoBehaviour, IUseInputSystem
    {
        private IControllable _controllable;
        private IInputSystem _input;
        public event Action OnJumpCharacter;
        public event Action OnMoveCharacter;

        [Inject]
        public void Construct(Player player, InputSystemPC inputSystem)
        {
            _controllable = player ? player : throw new ArgumentNullException($"{nameof(inputSystem)} is null");
            _input = inputSystem ? inputSystem : throw new ArgumentNullException($"{nameof(inputSystem)} is null");
        }
        
        private void MoveCharacter()
        {
            _controllable.Move(_input.Move());
        }

        private void Jump()
        {
           _controllable.Jump();
        }

        public void OnEnable()
        {
            OnJumpCharacter += Jump;
            OnMoveCharacter += MoveCharacter;
        }

        public void OnDisable()
        {
            OnJumpCharacter -= Jump;
            OnMoveCharacter -= MoveCharacter;
        }

        public void InvokeMove()
        {
            OnMoveCharacter?.Invoke();
        }

        public void InvokeJump()
        {
            OnJumpCharacter?.Invoke();
        }
    }
}