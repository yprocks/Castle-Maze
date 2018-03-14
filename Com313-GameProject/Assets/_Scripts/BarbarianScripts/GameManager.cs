using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    public Transform[] spawnPoints;
    public GameObject[] enemy;
    public int TotalEnemies = 20;

    [SerializeField] public GameObject player;
    private bool gameOver = false;
    private int enemyCount;

    public bool GameOver
    {
        get { return gameOver; }
    }

    public GameObject Player
    {
        get { return player; }
    }

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    public void Start()
    {
        enemyCount = 0;
        InvokeRepeating("SpawnEnemies", 0f, 1f);
    }

    private void SpawnEnemies()
    {
        if (enemyCount <= TotalEnemies)
            foreach (var spawnPoint in spawnPoints)
            {
                if (spawnPoint.transform.childCount == 0)
                {
                    GameObject spawnedEnemy = Instantiate(enemy[0], spawnPoint.transform.position, spawnPoint.transform.rotation);
                    spawnedEnemy.transform.parent = spawnPoint;
                    spawnedEnemy.GetComponent<BarbarianEnemyController>().enabled = true;
                    enemyCount++;
                }
            }
    }

    public void PlayerHit(int currentHP)
    {
        if (currentHP > 0)
            gameOver = false;
        else
            gameOver = true;
    }

    void OnSceneLoaded(UnityEngine.SceneManagement.Scene scene, UnityEngine.SceneManagement.LoadSceneMode mode)
    {
        if (scene.name == "GameOver")
        {
            Destroy(gameObject);
        }
    }

}