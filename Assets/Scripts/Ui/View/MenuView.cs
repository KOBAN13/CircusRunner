using UnityEngine;
using UnityEngine.UI;

namespace Ui
{
    public abstract class MenuView : View
    {
        [field: SerializeField] public Button ExitGame { get; private set; }
        
        public virtual void AddListenerButtonExitGame()
        {
            ExitGame.onClick.AddListener(_viewModel.ExitGame);
        }
    }
}