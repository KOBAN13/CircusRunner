using Configs;
using UnityEngine;
using Zenject;

namespace Character.PlayerJumpController
{
    public abstract class PlayerBaseMovement
    {
        protected PlayerComponents PlayerComponents;
        protected Vector3 TargetDirection;
        protected float speed;
    }
}