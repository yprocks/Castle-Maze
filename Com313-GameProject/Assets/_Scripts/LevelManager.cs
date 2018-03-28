using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Scripts
{
    public class LevelManager : MonoBehaviour {

        public void LoadScene(int changeLevel)
        {
            SceneManager.LoadScene(changeLevel);
        }

    }
}
