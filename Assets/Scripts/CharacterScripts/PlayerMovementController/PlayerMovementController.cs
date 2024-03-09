using System;
using System.Collections;
using Configs;
using InputSystem;
using ModestTree.Util;
using UnityEngine;

namespace Character.PlayerJumpController
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerMovementController : MonoBehaviour, IMovable, IPlayerSettings, IUseConfigable
    {
        [field: SerializeField] public СlownPlayerSettings ConfigPlayer { get; private set; }

        [field: Header("Player Settings")]
        [field: SerializeField] public float LineDistance { get; private set; }
        [field: SerializeField] public float FirstPosition { get; private set; }
        [field: SerializeField] public float SliderSpeed { get; private set; }
        
        public void Start()
        {
            _speed = ConfigPlayer.MaxSpeed;
        }

        public float TargetDirectionY
        {
            get => _targetDirection.y;
            set => _targetDirection.y = value;
        }

        public float Speed
        {
            get => _speed;
            set
            {
                if (value <= 0)
                    throw new ArgumentException("Value must be a positive number", nameof(value));
                _speed = value;
            }
        }

        public CharacterController CharacterController { get; private set; }

        private Vector3 _targetDirection;
        private int _lineToMove = 1;
        private float _speed;

        public void Awake() => CharacterController = GetComponent<CharacterController>();

        public void Update()
        {
            MoveForward();
        }

        public void Move(Swipe axis)
        {
            switch (axis)
            {
                case Swipe.Right:
                {
                    if (_lineToMove < 2)
                        _lineToMove++;
                    break;
                }
                case Swipe.Left:
                {
                    if (_lineToMove > 0)
                        _lineToMove--;
                    break;
                }
            }
        }

        private void MoveForward()
        {
            _targetDirection.x = _speed;
            CharacterController.Move(_targetDirection * Time.deltaTime);
            Vector3 newPosition = transform.position;
            newPosition.z = Mathf.Lerp(newPosition.z, FirstPosition + _lineToMove * LineDistance, Time.deltaTime * SliderSpeed);
            transform.position = newPosition;
        }
    }
}