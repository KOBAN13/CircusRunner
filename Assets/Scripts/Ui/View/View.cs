using System;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Ui
{
    public abstract class View : MonoBehaviour
    {
        protected ViewModel _viewModel;
    }
}