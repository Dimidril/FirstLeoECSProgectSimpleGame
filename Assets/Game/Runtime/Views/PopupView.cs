using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Game.Runtime.Views
{
    public class PopupView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _descriptionText;
        [SerializeField] private TMP_Text _buttonText;
        [SerializeField] private GameObject _root;
        [SerializeField] public Button _button;

        public void SetDescription(string text)
        {
            _descriptionText.text = text;
        }

        public void SetButtonText(string text)
        {
            _buttonText.text = text;
        }

        public void SetActive(bool state)
        {
            _root.SetActive(state);
        }

        public void AddListenerToButton(Action action)
        {
            var unityAction = new UnityAction(action);
            _button.onClick.AddListener(unityAction);
        }
        
        public void RemoveAllListeners()
        {
            _button.onClick.RemoveAllListeners();
        }
    }
}
