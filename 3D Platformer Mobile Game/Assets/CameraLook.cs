using System;
using UnityEngine;

public class CameraLook : MonoBehaviour
{
    [NonSerialized] public Vector2 LockAxis;
    
    [SerializeField] private Transform _playerBody;
    [SerializeField] private float _sensity = 40f;

    private float _xMove;
    private float _yMove;
    private float _xRotation;
    
    private void Update()
    {
        _xMove = LockAxis.x * _sensity * Time.deltaTime;
        _yMove = LockAxis.y * _sensity * Time.deltaTime;

        _xRotation -= _yMove;

        _xRotation = Mathf.Clamp(_xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(_xRotation, 0, 0);

        _playerBody.Rotate(Vector3.up * _xMove);
    }
}