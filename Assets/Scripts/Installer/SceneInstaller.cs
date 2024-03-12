using Character;
using Character.Collisions;
using Character.Loader;
using Character.Physics;
using Character.PlayerChoise;
using Character.PlayerJumpController;
using Character.ReloadTeleport;
using Coupon;
using CreateCoupon;
using InputSystem;
using Ui;
using UnityEngine;
using Zenject;

public class SceneInstaller : MonoInstaller
{
    [field: SerializeField] public Player Player { get; private set; }
    [field: SerializeField] public InputSystemPC InputSystemPC { get; private set; }
    [field: SerializeField] public CharacterInputController CharacterController { get; private set; }
    [field: SerializeField] public CoroutineRunner CoroutineRunner { get; private set; }
    [field: SerializeField] public TicketSpawner TicketSpawner { get; private set; }
    [field: SerializeField] public ReloadTeleports ReloadTeleports { get; private set; }
    [field: SerializeField] public PlayerComponents PlayerComponents { get; private set; }
    
    public override void InstallBindings()
    {
        BindLoader();
        BindPlayer();
        BindInput();
        BindInputInterface();
        BindCollisionHandler();
        BindCourutine();
        BindStopMove();
        BindPoolObject();
        BindTimeManager();
        BindTicketSpawner();
        BindReloadTeleport();
        BindPlayerComponents();
        BindGravity();
        BindPlayerJump();
        BindPlayerMovement();
        BindChoisePlayer();
    }

    private void BindChoisePlayer()
    {
        Container.BindInterfacesAndSelfTo<СhoicePlayer>().AsSingle().NonLazy();
    }

    private void BindLoader()
    {
        Container.BindInterfacesAndSelfTo<AddressableLoader>().AsSingle().NonLazy();
    }

    private void BindGravity()
    {
        Container.BindInterfacesAndSelfTo<Gravity>().AsSingle().NonLazy();
    }

    private void BindReloadTeleport()
    {
        Container.BindInterfacesAndSelfTo<ReloadTeleports>().FromInstance(ReloadTeleports).AsTransient().NonLazy();
    }

    private void BindPlayerJump()
    {
        Container.BindInterfacesAndSelfTo<PlayerTeleportJumpController>().AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<PlayerClownJumpController>().AsSingle().NonLazy();
    }
    
    private void BindPlayerMovement()
    {
        Container.BindInterfacesAndSelfTo<PlayerClownMovementController>().AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<PlayerTeleportMovementController>().AsSingle().NonLazy();
    }

    private void BindPlayerComponents()
    {
        Container.BindInterfacesAndSelfTo<PlayerComponents>().FromInstance(PlayerComponents).AsSingle().NonLazy();
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

    private void BindCollisionHandler()
    {
        Container.BindInterfacesAndSelfTo<CollisionHandler>().AsSingle().NonLazy();
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
