using System;
using Modules.Content.Player;
using UnityEngine;

public class PlayerAnimationsUpdater : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private PlayerController _playerController;

    private string _speedParam = "Speed";
    private string _jumpParam = "Jump";  
    
    private void Update()
    {
        UpdateAnimations();
    }

    public void ExecuteJumpAnimation()
    {
        _animator.SetTrigger(_jumpParam);
    }

    private void UpdateAnimations()
    {
        float speed = _animator.GetFloat(_speedParam);

        float smooth = Time.deltaTime * _playerController.PlayerAcceleration;

        if (_playerController.IsMoving)
        {
            speed += smooth;
        }

        else
        {
            speed -= smooth;
        }

        speed = Mathf.Clamp01(speed);

        _animator.SetFloat(_speedParam, speed);
    }
}