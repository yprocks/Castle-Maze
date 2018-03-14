using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ToLevel3 : MonoBehaviour
{

    void OnTriggerEnter(Collider other)
    {
        SceneManager.LoadScene("Level 3");
    }
}