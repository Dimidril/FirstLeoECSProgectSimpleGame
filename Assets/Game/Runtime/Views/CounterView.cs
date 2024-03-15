using TMPro;
using UnityEngine;

namespace Game.Runtime.Views
{
    public class CounterView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _counterText;

        public void SetText(string text) => _counterText.text = text;
    }
}