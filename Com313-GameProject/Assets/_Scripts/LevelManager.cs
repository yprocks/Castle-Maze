using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {

	public void LoadScene(int changeLevel)
    {
        SceneManager.LoadScene(changeLevel);
    }

}
