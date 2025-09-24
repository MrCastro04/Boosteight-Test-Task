using System;
using UnityEngine;
using UnityEngine.UI;

public class JumpButton : MonoBehaviour
{
    [SerializeField] private Button _button;
    
    public event Action OnJump;
    private void Start()
    {
        _button.onClick.AddListener(() => OnJump?.Invoke());
    }
}