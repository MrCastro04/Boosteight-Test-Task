using System;
using DG.Tweening;
using Modules.Core.Managers;
using UnityEngine;

namespace Modules.Core.UI
{
    public class WinScreen : MonoBehaviour
    {
        [SerializeField] private float _scaleDuration;
        [SerializeField] private ManagerScene managerScene;
        
        private void OnEnable()
        {
            transform.localScale = Vector3.zero;
        }

        public async void Open()
        {
           await transform.DOScale(Vector3.one, _scaleDuration).AsyncWaitForCompletion();
           
           managerScene.StopTime();
        }

        public void Close()
        {
            transform.DOScale(Vector3.zero, _scaleDuration);
        }
    }
}