using Character;
using Character.Collisions;
using Character.Physics;
using Character.PlayerJumpController;
using Coupon;
using CreateCoupon;
using InputSystem;
using Ui;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

public class SceneInstaller : MonoInstaller
{
    [field: SerializeField] public Player Player { get; private set; }
    [field: SerializeField] public InputSystemPC InputSystemPC { get; private set; }
    [field: SerializeField] public CharacterInputController CharacterController { get; private set; }
    [field: SerializeField] public PlayerMovementController MovementController { get; private set; }
    [field: SerializeField] public PlayerJumpController JumpController { get; private set; }
    [field: SerializeField] public CoroutineRunner CoroutineRunner { get; private set; }
    [field: SerializeField] public TicketSpawner TicketSpawner { get; private set; }

    public override void InstallBindings()
    {
        BindPlayer();
        BindInput();
        BindInputInterface();
        BindAllInterface();
        BindPhysics();
        BindCollisionHandler();
        BindCourutine();
        BindStopMove();
        BindPoolObject();
        BindTimeManager();
        BindTicketSpawner();
    }

    private void BindTicketSpawner()
    {
        Container.BindInterfacesAndSelfTo<TicketSpawner>().FromInstance(TicketSpawner).NonLazy();
    }

    private void BindPoolObject()
    {
        Container.BindInterfacesAndSelfTo<PoolObject<Ticket>>().AsSingle().NonLazy();
    }

    private void BindTimeManager()
    {
        Container.BindInterfacesAndSelfTo<TimeManager>().AsSingle().NonLazy();
    }
    
    private void BindAllInterface()
    {
        Container.BindInterfacesAndSelfTo<PlayerMovementController>().FromInstance(MovementController).AsCached().NonLazy();
        Container.BindInterfacesAndSelfTo<PlayerJumpController>().FromInstance(JumpController).AsCached().NonLazy();
    }

    private void BindCollisionHandler()
    {
        Container.BindInterfacesAndSelfTo<CollisionHandler>().AsSingle().NonLazy();
    }

    private void BindPhysics()
    {
        Container.BindInterfacesAndSelfTo<Gravity>().AsSingle().NonLazy();
    }

    private void BindCourutine()
    {
        Container.BindInterfacesAndSelfTo<CoroutineRunner>().FromInstance(CoroutineRunner).AsSingle();
        Container.BindInterfacesAndSelfTo<CoroutineHelper>().AsSingle().NonLazy();
    }

    private void BindStopMove()
    {
        Container.BindInterfacesAndSelfTo<StopMovements>().AsSingle().NonLazy();
    }
    
    private void BindInputInterface()
    {
        Container.BindInterfacesAndSelfTo<NewInputSystem>().AsSingle().NonLazy();
    }

    private void BindInput()
    {
        Container.BindInterfacesAndSelfTo<CharacterInputController>().FromInstance(CharacterController).AsCached().NonLazy();
        Container.BindInterfacesAndSelfTo<InputSystemPC>().FromInstance(InputSystemPC).AsCached().NonLazy();
    }

    private void BindPlayer()
    {
        Container.BindInterfacesAndSelfTo<Player>().FromInstance(Player).AsCached().NonLazy();
    }
}
