using System;
using Modules.Content.Coin_And_Prize.Prize;
using Modules.Core.UI;
using UnityEngine;

namespace Modules.Core.Managers
{
    public class ManagerScreens : MonoBehaviour
    {
        [SerializeField] private WinScreen _winScreen;

        private void OnEnable()
        {
            PrizeEvents.OnCollect += OpenWinScreen;
        }

        private void OnDisable()
        {
            PrizeEvents.OnCollect -= OpenWinScreen;
        }

        private void Start()
        {
            _winScreen.Close();
        }

        private void OpenWinScreen()
        {
             _winScreen.Open();
        }
    }
}