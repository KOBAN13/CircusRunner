using System;
using Character.PlayerJumpController;
using InputSystem;
using UnityEngine;

namespace Character
{
    public class Player : MonoBehaviour, IControllable, IUseConfigable, IMovement
    {
        public IConfigable Config { get; private set; }

        public IMovable Movable { get; private set; }
        public IJumpable Jumpable { get; private set; }
        
        public void Init(IMovable move, IJumpable jump, IConfigable config)
        {
            Movable = move ?? throw new ArgumentNullException($"{nameof(move)} is null fix this");
            Jumpable = jump ?? throw new ArgumentNullException($"{nameof(jump)} is null fix this");
            Config = config ?? throw new ArgumentNullException($"{nameof(jump)} is null fix this");
        } 

        public void Move(Swipe axis)
        {
            Movable.Move(axis);
        }

        public void Jump()
        {
            Jumpable.Jump();
        }
    }
}