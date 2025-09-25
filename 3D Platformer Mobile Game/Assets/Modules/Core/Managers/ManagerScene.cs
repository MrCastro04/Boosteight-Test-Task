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

        public void SetNormalTime()
        {
            Time.timeScale = 1f;
        }
        
        public void LoadScene(int sceneIndex)
        {
            SetNormalTime();
            
            SceneManager.LoadScene(sceneIndex);
        }
        
        public void RestartScene()
        {
            Scene currentScene = SceneManager.GetActiveScene();
            
            SetNormalTime(); 
            
            SceneManager.LoadScene(currentScene.buildIndex);
        }
    }
}