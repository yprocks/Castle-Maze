using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using UnityEngine.SceneManagement;

public class CountdownTimer : MonoBehaviour
{
    //    public float timeLeft = 300.0f;
    //    public bool stop = true;

    //    private float minutes;
    //    private float seconds;

    //    public Text text;

    //    public void startTimer(float from)
    //    {
    //        stop = false;
    //        timeLeft = from;
    //        Update();
    //        StartCoroutine(updateCoroutine());
    //    }

    //    void Update()
    //    {
    //        if (stop) return;
    //        timeLeft -= Time.deltaTime;

    //        minutes = Mathf.Floor(timeLeft / 60);
    //        seconds = timeLeft % 60;
    //        if (seconds > 59) seconds = 59;
    //        if (minutes < 0)
    //        {
    //            stop = true;
    //            minutes = 0;
    //            seconds = 0;
    //        }
    //        //        fraction = (timeLeft * 100) % 100;
    //    }

    //    private IEnumerator updateCoroutine()
    //    {
    //        while (!stop)
    //        {
    //            text.text = string.Format("{0:0}:{1:00}", minutes, seconds);
    //            yield return new WaitForSeconds(0.2f);
    //        }
    //    }
    //}

    //public static CountdownTimer instance;
//    public float TotalTime = 180;
//    private float timeLeft;
//    public Image timeBar;
//    private Text text;
//
//
//    void Awake()
//    {
//        text = GetComponent<Text>();
//        timeLeft = TotalTime;
//    }
//
//    void Update()
//    {
//        timeLeft -= Time.deltaTime;
//        text.text = "Time Left: " + Mathf.Round(timeLeft);
//        timeBar.fillAmount = (float)timeLeft / TotalTime;
//
//        if (timeLeft <= 0)
//            SceneManager.LoadScene("StartMenu");
//    }
}