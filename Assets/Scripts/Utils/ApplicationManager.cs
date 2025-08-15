using UnityEngine;
using UnityEngine.SceneManagement;

namespace Utils
{
    public class ApplicationManager : MonoBehaviour
    {
        public void Play()
        {
            SceneManager.LoadScene(1);
        }
        public void Quit()
        {
            Application.Quit();
        }
    }
}