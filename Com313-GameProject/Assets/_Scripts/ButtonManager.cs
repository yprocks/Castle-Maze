using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour {

	public void StartBtn(string newLevel)
    {
        SceneManager.LoadScene(newLevel);
    }

  
}
