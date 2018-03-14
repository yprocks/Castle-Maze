using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Tolevel2 : MonoBehaviour
{

    public static int finalScore;
    void OnTriggerEnter(Collider other)
    {
        
        SceneManager.LoadScene("GameOver");
        //ScoreManager.Score = PlayerShooting.finalScore;
        //ScoreManager.Score += PlayerShooting.scoreValue;
        //finalScore = ScoreManager.Score;
    }
}