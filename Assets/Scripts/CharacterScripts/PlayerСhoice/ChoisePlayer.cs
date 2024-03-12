using System;
using System.Threading.Tasks;
using Character.Loader;
using Character.PlayerJumpController;
using Configs;
using UnityEngine;
using Zenject;

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

        private async void OnPlayerClownPlay()
        {
            _clownPlayerSettings = await LoadConfigs(_clownPlayerSettings, ClownConfig);
            var playerClownMover = GetItemFromContainer<PlayerClownMovementController>();
            var playerClownJump = GetItemFromContainer<PlayerClownJumpController>();
            
            playerClownMover.Init(_clownPlayerSettings);
            playerClownJump.Init(_clownPlayerSettings);
            
            _player.Init(playerClownMover, playerClownJump, _clownPlayerSettings);
            Time.timeScale = 1f;
        }

        private async void OnPlayerTeleporterPlay()
        {
            _teleporterPlayerSettings = await LoadConfigs(_teleporterPlayerSettings,TeleporterConfig);
            var playerTeleporterMover = GetItemFromContainer<PlayerTeleportMovementController>();
            var playerTeleporterJump = GetItemFromContainer<PlayerTeleportJumpController>();
            
            playerTeleporterMover.Init(_teleporterPlayerSettings);
            playerTeleporterJump.Init(_teleporterPlayerSettings);
            
            _player.Init(playerTeleporterMover, playerTeleporterJump, _teleporterPlayerSettings);
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

        private T GetItemFromContainer<T>() => _diContainer.Resolve<T>();
    }
}