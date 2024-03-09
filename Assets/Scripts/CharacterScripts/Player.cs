using System;
using Character.PlayerJumpController;
using InputSystem;
using UnityEngine;
using Zenject;

namespace Character
{
    [RequireComponent(typeof(CharacterController))]
    public class Player : MonoBehaviour, IControllable
    {
        private IMovable _movable;
        private IJumpable _jumpable;

        [Inject]
        public void Construct(IMovable move, IJumpable jump)
        {
            _movable = move ?? throw new ArgumentNullException($"{nameof(move)} is null fix this");
            _jumpable = jump ?? throw new ArgumentNullException($"{nameof(jump)} is null fix this");
        }

        public void Move(Swipe axis)
        {
            _movable.Move(axis);
        }

        public void Jump()
        {
            _jumpable.Jump();
        }
    }
}