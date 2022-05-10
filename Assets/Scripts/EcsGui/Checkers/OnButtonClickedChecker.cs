using UnityEngine;
using UnityEngine.UI;

namespace LeoEcsGui
{
    [RequireComponent(typeof(Button))]
    public class OnButtonClickedChecker : MonoBehaviour
    {
        private Button _button;

        private void Awake()
        {
            _button = GetComponent<Button>();
            _button.onClick.AddListener(OnClick);
        }

        private void OnClick()
        {
            EcsGuiEvents.RegisterOnButtonClickedEvent(_button, gameObject);
        }
    }
}