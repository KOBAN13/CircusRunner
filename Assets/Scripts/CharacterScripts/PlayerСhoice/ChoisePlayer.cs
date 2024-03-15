using System;
using System.Threading.Tasks;
using Character.Loader;
using Character.PlayerJumpController;
using Configs;
using UnityEngine;
using Zenject;
using Task = UnityEditor.VersionControl.Task;

namespace Character.PlayerChoise
{
    public class СhoicePlayer : IInitializable, IDisposable, IInvoke
    {
        private event Action ClownChoise;
        private event Action TeleporterChoise;

        private AddressableLoader _addressableLoader;
        
        private const string ClownConfig = "ClownConfig";
        private const string ClownPrefab = "";
        private СlownPlayerSettings _clownPlayerSettings;
        
        private const string TeleporterConfig = "TeleporterConfig";
        private const string TeleporterPrefab = "";
        private TeleporterPlayerSettings _teleporterPlayerSettings;

        private DiContainer _diContainer;
        private Player _player;

        public void Initialize()
        {
            ClownChoise += OnPlayerClownPlay;
            TeleporterChoise += OnPlayerTeleporterPlay;
        }

        public void Dispose()
        {
            ClownChoise += OnPlayerClownPlay;
            TeleporterChoise += OnPlayerTeleporterPlay;
        }
        
        public void Invoke(Type typeConfig)
        {
            if(typeConfig == typeof(СlownPlayerSettings)) ClownChoise?.Invoke();
            if(typeConfig == typeof(TeleporterPlayerSettings)) TeleporterChoise?.Invoke();
        }

        [Inject]
        public void Construct(DiContainer container, Player player, AddressableLoader loader)
        {
            _diContainer = container;
            _player = player;
            _addressableLoader = loader;
        }

        private void OnPlayerClownPlay()
        {
            PlayerTypePlayer(LoadConfigs(_clownPlayerSettings, ClownConfig), 
                typeof(PlayerClownMovementController), typeof(PlayerClownJumpController), _clownPlayerSettings);
        }

        private void OnPlayerTeleporterPlay()
        {
            PlayerTypePlayer(LoadConfigs(_teleporterPlayerSettings, TeleporterConfig),
                typeof(PlayerTeleportMovementController), typeof(PlayerTeleportJumpController), _teleporterPlayerSettings);
        }

        private async void PlayerTypePlayer<T>(Task<T> loadConf, Type moverType, Type jumpType, T configSettings)
        {
            configSettings = await loadConf;

            var playerMover = GetItemFromContainer(moverType) as IInit;
            var playerJump = GetItemFromContainer(jumpType) as IInit;

            playerMover?.Init(configSettings);
            playerJump?.Init(configSettings);
            
            
            _player.Init(playerMover as IMovable, playerJump as IJumpable, configSettings as IConfigable);
            Time.timeScale = 1f;
        }

        private async Task<T> LoadConfigs<T>(T typeConfSetting, string configSetting)
            where T : ScriptableObject
        {
            if (typeConfSetting == null)
            {
                typeConfSetting = await _addressableLoader.Loader<T>(configSetting);
            }

            return typeConfSetting;
        }

        private object GetItemFromContainer(Type type) => _diContainer.Resolve(type);
    }
}