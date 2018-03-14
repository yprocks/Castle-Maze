using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameOverScript : MonoBehaviour
{

    void OnTriggerEnter(Collider other)
    {
        SceneManager.LoadScene("GameOver");
    }
}