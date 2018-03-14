using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    private int Enemy;
    private int TotalEnemies;
    public Image EnemyCountBar;

    Text text;

    // Use this for initialization
    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
        
    }

    private void Start()
    {
        text = GetComponent<Text>();
        TotalEnemies = GameManager.instance.TotalEnemies;
        Enemy = TotalEnemies;
        text.text = "Enemies: " + Enemy;
    }

    public void KillEnemy()
    {
        Enemy--;
        text.text = "Enemies: " + Enemy;
        EnemyCountBar.fillAmount = (float)Enemy / TotalEnemies;
    }
}