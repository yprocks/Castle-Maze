using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Scripts
{
    public class ButtonManager : MonoBehaviour {

        public void StartBtn(string newLevel)
        {
            SceneManager.LoadScene(newLevel);
        }

  
    }
}
