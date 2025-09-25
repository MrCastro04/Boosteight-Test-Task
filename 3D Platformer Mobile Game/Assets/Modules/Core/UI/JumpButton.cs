using System;
using Modules.Content.Player;
using UnityEngine;
using UnityEngine.UI;

public class JumpButton : MonoBehaviour
{
    [SerializeField] private Button _button;
    
    private void Start()
    {
        _button.onClick.AddListener( () => PlayerEvents.ExecuteEventPlayerJump());
    }
}