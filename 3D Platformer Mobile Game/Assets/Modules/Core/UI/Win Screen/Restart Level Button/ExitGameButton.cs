using Modules.Core.Managers;
using UnityEngine;
using UnityEngine.UI;

namespace Modules.Core.UI
{
    [RequireComponent(typeof(Button))]
    public class ExitGameButton : MonoBehaviour
    {
        [SerializeField] private ManagerScene _managerScene;

        private Button _button;

        private void Awake()
        {
            _button = GetComponent<Button>();
        }

        private void Start()
        {
            _button.onClick.AddListener(() => _managerScene.ExitGame());
        }
    }
}