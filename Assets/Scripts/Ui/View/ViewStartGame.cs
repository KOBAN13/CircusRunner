using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Ui
{
    public class ViewStartGame : MenuView
    {
        [field: SerializeField] public Button Play { get; private set; }
        [field: SerializeField] public Button Authors { get; private set; }
        
        [field: SerializeField] public Button TelegramDaniil { get; private set; }
        [field: SerializeField] public Button TelegramDanil { get; private set; }
        
        [field: SerializeField] public Button TelegramPavel { get; private set; }
        [field: SerializeField] public Button TelegramNurik { get; private set; }
        [field: SerializeField] public Button Back { get; private set; }

        [field: SerializeField] public Canvas CanvasAuthors { get; private set; }
        [field: SerializeField] public Canvas CanvasStartGame { get; private set; }

        [Inject]
        public void Construct(ViewModel viewModel)
        {
            _viewModel = viewModel;
            CanvasAuthors.enabled = false;
        }

        public void AddListenerButtonPlay()
        {
            Play.onClick.AddListener(() =>
            {
                Time.timeScale = 0f;
                _viewModel.NameLoadScene.Value = "FinalLevel";
            });
        }

        public void AddListenerAuthors()
        {
            CanvasStartGame.enabled = false;
            CanvasAuthors.enabled = true;
        }
        
        public void AddListenerBack()
        {
            CanvasAuthors.enabled = false;
            CanvasStartGame.enabled = true;
        }

        public void AddListenerButtonDaniil(string text)
        {
            TelegramDaniil.onClick.AddListener(() => _viewModel.TelegramLink.Value = text);
        }
        
        public void AddListenerTelegramDanil(string text)
        {
            TelegramDanil.onClick.AddListener(() => _viewModel.TelegramLink.Value = text);
        }
        
        public void AddListenerTelegramPavel(string text)
        {
            TelegramPavel.onClick.AddListener(() => _viewModel.TelegramLink.Value = text);
        }
        
        public void AddListenerTelegramNurik(string text)
        {
            TelegramNurik.onClick.AddListener(() => _viewModel.TelegramLink.Value = text);
        }
    }
}