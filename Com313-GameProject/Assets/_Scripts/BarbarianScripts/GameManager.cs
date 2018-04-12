using System.Collections;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using _Scripts.Statics;

namespace _Scripts.BarbarianScripts
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private GameObject _arrow;

        public static GameManager Instance;
        public Transform[] SpawnPoints;
        public GameObject[] Enemy;
        public Transform[] PowerUpSpwans;
        public GameObject[] PowerUps;
        public int MaxPowerUps = 4;
        public int TotalEnemies = 20;
        public Text LevelText;
        public GameObject Portal;
        public GameObject ChildCamera;
        private int _enemy;

        private GameState _gameState;
        
        public float PortalSpawnTime;

        // ReSharper disable once InconsistentNaming
        private float _playerXP;

        private float _currentPowerUpSpawnTime = 0;
        [SerializeField] private float _powerUpSpawnTime = 60;
        private int _powerUps = 0;

        
        // Portal Transform Variables
        private GameObject _cutScenesCamera;

        private GameObject[] _portals;

        private bool _enteredPortal = false;
        private GameObject _destPortal;

        private Vector3 _targetPortalPosition;

        private GameObject _playerObjectCollided;

        private Coroutine _teleportCoroutine;
        // Portal Transform Variables

        [SerializeField] private GameObject _player;
        private int _enemyCount;
        private readonly float _generatedSpawnTime = 1;
        private float _currentSpawnTime = 0;

        public GameObject[] PortalsSpawns;
        private bool _spawnedPortals = false;
        private float _portalSpawnTimer;
        private bool _shownHintPanel;
        private int _portalInScene;

        public int CurrentLevel { get; private set; }

        public bool GameOver
        {
            get { return _gameState != GameState.GameOn; }
        }

        public GameObject Player
        {
            get { return _player; }
        }

        public GameObject Arrow
        {
            get { return _arrow; }
        }

        public void RegisterPowerUp()
        {
            _powerUps++;
        }
        
        public void UnRegisterPowerUp()
        {
            _powerUps = _powerUps > 0 ? _powerUps-- : 0;
        }

        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else if (Instance != this)
                Destroy(gameObject);
            
            CurrentLevel = SceneManager.GetActiveScene().buildIndex;
        }

        public void Start()
        {
            _portalInScene = 0;
            _shownHintPanel = false;
            _enemy = TotalEnemies;
            _gameState = GameState.GameOn;
            _playerXP = PlayerPrefs.GetFloat("PlayerXP", 0);
            _enemyCount = 0;
            LevelText.text = "Level " + CurrentLevel;
            _cutScenesCamera = GameObject.Find("Follow Camera");
            _portalSpawnTimer = PortalSpawnTime;
            InvokeRepeating("SpawnEnemies", 2f, 1f);
            if(CurrentLevel == 2)
                ChildCamera.SetActive(false);
            XPScript.Instance.UpdateXP(_playerXP);
            
            StartCoroutine(PowerUpSpawn());

        }

        private void SpawnEnemies()
        {
            if (_enemyCount > TotalEnemies) return;
            foreach (var spawnPoint in SpawnPoints)
            {
                if (spawnPoint.transform.childCount != 0) continue;
                var spawnedEnemy = Instantiate(Enemy[Random.Range(0, CurrentLevel)], spawnPoint.transform.position,
                    spawnPoint.transform.rotation);
                spawnedEnemy.transform.parent = spawnPoint;
                spawnedEnemy.GetComponent<BarbarianEnemyController>().enabled = true;
                _enemyCount++;
            }
        }

        private void Update()
        {
            if (EnemyKilled())
                ShowHintText();
            
            _currentSpawnTime += Time.deltaTime;
            _currentPowerUpSpawnTime += Time.deltaTime;
            
            if (_currentSpawnTime > _generatedSpawnTime)
                _currentSpawnTime = 0;
            
            if (CurrentLevel <= 1) return;

            if (CurrentLevel == 2)
            {
                if (_enteredPortal)
                    MoveCameraToTargetPortal();

                if (_spawnedPortals) return;
                _portalSpawnTimer -= Time.deltaTime;

                if (_portalSpawnTimer <= 0)
                    SpawnPortals();
            } 
        }

        private void ShowHintText()
        {
            if (_shownHintPanel) return;
            InGameTextManager.GetInstance().ShowPanelForSeconds("Hey Barbarian!", "Find a way to exit. Enough play with Orcs!!!", 15);
            _shownHintPanel = true;
        }

        private void SpawnPortals()
        {
            while (_portalInScene < 2)
            {
                var spawn = PortalsSpawns[Random.Range(0, PortalsSpawns.Length)];
                if (spawn.transform.childCount != 0) continue;
                var obj = Instantiate(Portal, spawn.transform.position, Quaternion.identity);
                obj.transform.parent = spawn.transform;
                _portalInScene ++;
            }

            _spawnedPortals = true;
            _portalSpawnTimer = PortalSpawnTime;
        }

        public void PlayerHit(int currentHp)
        {
            if (currentHp > 0) return;
            _gameState = GameState.OrcWon;
            PlayerPrefs.SetInt("GameOver", (int) GameState.OrcWon);
            StartCoroutine(LoadNewLevel("GameOver"));
        }

        public void EnterPortal(GameObject portalCollided, GameObject playerCollided)
        {
            _portals = GameObject.FindGameObjectsWithTag("PortalSpawn");
            _portals = GameObject.FindGameObjectsWithTag("Portal");
            _destPortal = portalCollided;
            while (_destPortal == portalCollided) _destPortal = _portals[Random.Range(0, _portals.Length)];
            _targetPortalPosition = _destPortal.transform.parent.position;
            _cutScenesCamera.GetComponent<CameraFollow>().enabled = false;
            _playerObjectCollided = playerCollided;
            _playerObjectCollided.SetActive(false);
            _teleportCoroutine = StartCoroutine(TriggerTeleportation(true));
        }

        private IEnumerator TriggerTeleportation(bool value)
        {
            yield return new WaitForSeconds(1f);
            _enteredPortal = value;
            if (_enteredPortal) yield break;
            var playerTransform = _targetPortalPosition + new Vector3(2, 0, 2);
            playerTransform.y = 1.8f;
            _playerObjectCollided.transform.position = playerTransform;
            _playerObjectCollided.SetActive(true);
            _cutScenesCamera.GetComponent<CameraFollow>().enabled = true;
            if (_spawnedPortals) DestroyPortals();
            _portalSpawnTimer = PortalSpawnTime;
            _spawnedPortals = false;
        }

        private void DestroyPortals()
        {
            foreach (var portal in _portals)
            {
                _portalInScene--;
                Destroy(portal);
            }
        }

        private void MoveCameraToTargetPortal()
        {
            var position = Vector3.Lerp(_cutScenesCamera.transform.position,
                _targetPortalPosition,
                Time.deltaTime * 2);
            position.y = _cutScenesCamera.transform.position.y;
            _cutScenesCamera.transform.position = position;

            var point1 = new Vector2(_cutScenesCamera.transform.position.x, _cutScenesCamera.transform.position.z);
            var point2 = new Vector2(_targetPortalPosition.x, _targetPortalPosition.z);

            if (Vector2.Distance(point1, point2) > 3.0f)
                return;

            _teleportCoroutine = StartCoroutine(TriggerTeleportation(false));
//            StopCoroutine(_teleportCoroutine);
        }

        // ReSharper disable once InconsistentNaming
        public void AddXP(float xP)
        {
            _playerXP += xP;
            XPScript.Instance.UpdateXP(_playerXP);
        }

        // ReSharper disable once InconsistentNaming
        private float GetXP()
        {
            return _playerXP;
        }
        
        public void KillEnemy()
        {
            _enemy--;
        }

        public bool EnemyKilled()
        {
            return _enemy <= 0;
        }

        public void FoundExit()
        {
            if (CurrentLevel > 1 && !EnemyKilled()) return;
            PlayerPrefs.SetFloat("PlayerXP", GetXP());

            if (CurrentLevel == 2)
            {
                _gameState = GameState.PlayerWon;
                PlayerPrefs.SetInt("GameOver", (int) GameState.PlayerWon);
                StartCoroutine(LoadNewLevel("GameOver"));
            }
            else
                StartCoroutine(LoadNewLevel(CurrentLevel + 1));

        }
        
        private static IEnumerator LoadNewLevel(int index)
        {
            yield return new WaitForSeconds(2f);
            SceneManager.LoadScene(index);
        }
        
        private static IEnumerator LoadNewLevel(string level)
        {            
            yield return new WaitForSeconds(2f);
            SceneManager.LoadScene(level);
        }

        public void PortalDestroyed()
        {
            _spawnedPortals = false;
            _portalInScene--;
        }

        // ReSharper disable once FunctionRecursiveOnAllPaths
        private IEnumerator PowerUpSpawn()
        {
            if (_currentPowerUpSpawnTime > _powerUpSpawnTime)
            {
                _currentPowerUpSpawnTime = 0;
                if (_powerUps < MaxPowerUps)
                {
                    var randomNumber = Random.Range(0, PowerUpSpwans.Length);
                    var spawnLocation = PowerUpSpwans[randomNumber];

                    while (spawnLocation.childCount != 0)
                    {
                        randomNumber = Random.Range(0, PowerUpSpwans.Length);
                        spawnLocation = PowerUpSpwans[randomNumber];
                    }
                    var randomPowerUp = PowerUps[Random.Range(0, PowerUps.Length)];

                    var newPowerup = Instantiate(randomPowerUp);
                    newPowerup.transform.position = spawnLocation.transform.position;
                    newPowerup.transform.parent = spawnLocation;
                }
            }
            yield return null;
            StartCoroutine(PowerUpSpawn());
        }
    }
}