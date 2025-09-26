using UnityEngine;
using UnityEngine.SceneManagement;

namespace Modules.Core.Managers
{
    public class ManagerScene : MonoBehaviour
    {
        public void StopTime()
        {
            Time.timeScale = 0f;
        }

        public void ExitGame()
        {
            Application.Quit();
        }
    }
}