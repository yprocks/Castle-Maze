using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace _Scripts.BarbarianScripts
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;
        public Transform[] SpawnPoints;
        public GameObject[] Enemy;
        public int TotalEnemies = 20;
        public Text LevelText;

        [SerializeField] private GameObject _player;
        private int _enemyCount;
        private int _currentLevel;
        private float _generatedSpawnTime = 1;
        private float _currentSpawnTime = 0;

        public int CurrentLevel
        {
            get { return _currentLevel; }
            private set { _currentLevel = value; }
        }
        
        public GameManager()
        {
            GameOver = false;
        }

        public bool GameOver { get; private set; }

        public GameObject Player
        {
            get { return _player; }
        }

        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else if (Instance != this)
                Destroy(gameObject);
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        public void Start()
        {
            _currentLevel = 1;
            _enemyCount = 0;
            LevelText.text = "Level " + _currentLevel;
            InvokeRepeating("SpawnEnemies", 0f, 1f);
        }

        private void SpawnEnemies()
        {
            if (_enemyCount > TotalEnemies) return;
            foreach (var spawnPoint in SpawnPoints)
            {
                if (spawnPoint.transform.childCount != 0) continue;
                var spawnedEnemy = Instantiate(Enemy[0], spawnPoint.transform.position,
                    spawnPoint.transform.rotation);
                spawnedEnemy.transform.parent = spawnPoint;
                spawnedEnemy.GetComponent<BarbarianEnemyController>().enabled = true;
                _enemyCount++;
            }
        }

        private void Update()
        {
            _currentSpawnTime += Time.deltaTime;
            if (_currentSpawnTime > _generatedSpawnTime)
            {
                _currentSpawnTime = 0;
            }
        }

        public void PlayerHit(int currentHp)
        {
            GameOver = currentHp <= 0;
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            this._currentLevel = SceneManager.GetActiveScene().buildIndex + 1;
            LevelText.text = "Level " + _currentLevel;
            if (scene.name == "GameOver")
            {
                Destroy(this.gameObject);
            }
        }
    }
}