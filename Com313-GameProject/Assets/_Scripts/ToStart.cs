using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;


public class ToStart : MonoBehaviour {

    void OnTriggerEnter(Collider other)
    {
        SceneManager.LoadScene("GameOver");
    }
}
