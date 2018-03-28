using System.Collections;
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
        public GameObject Portal;
            
        public float PortalSpawnTime;

        // ReSharper disable once InconsistentNaming
        private float _playerXP;
        
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
        private int _currentLevel;
        private readonly float _generatedSpawnTime = 1;
        private float _currentSpawnTime = 0;

        private GameObject[] _portalsSpawns;
        private bool _spawnedPortals = false;
        private float _portalSpawnTimer;

        public int CurrentLevel
        {
            get { return _currentLevel; }
            private set { _currentLevel = value; }
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
//            DontDestroyOnLoad(gameObject);
//            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        public void Start()
        {
            _playerXP = 0;
            _currentLevel = SceneManager.GetActiveScene().buildIndex;
            _enemyCount = 0;
            LevelText.text = "Level " + _currentLevel;
            _cutScenesCamera = GameObject.Find("Follow Camera");
            GameOver = false;
            _portalSpawnTimer = PortalSpawnTime;
            InvokeRepeating("SpawnEnemies", 2f, 1f);
        }

        private void SpawnEnemies()
        {
            if (_enemyCount > TotalEnemies) return;
            foreach (var spawnPoint in SpawnPoints)
            {
                if (spawnPoint.transform.childCount != 0) continue;
                var spawnedEnemy = Instantiate(Enemy[Random.Range(0, _currentLevel)], spawnPoint.transform.position,
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
                _currentSpawnTime = 0;
            
            if (CurrentLevel <= 1) return;

            if (CurrentLevel == 2)
                if (_enteredPortal)
                    MoveCameraToTargetPortal();

            if (_portalsSpawns == null)
                _portalsSpawns = GameObject.FindGameObjectsWithTag("PortalSpawn");
            else if (!_spawnedPortals)
            {
                _portalSpawnTimer -= Time.deltaTime;

                if (_portalSpawnTimer <= 0)
                    SpawnPortals();
            }
        }

        private void SpawnPortals()
        {
            var i = 0;
            while (i < 2)
            {
                var spawn = _portalsSpawns[Random.Range(0, _portalsSpawns.Length)];
                if (spawn.transform.childCount != 0) continue;
                var obj = Instantiate(Portal, spawn.transform.position, Quaternion.identity);
                obj.transform.parent = spawn.transform;
                i++;
            }

            _spawnedPortals = true;
            _portalSpawnTimer = PortalSpawnTime;
        }

        public void PlayerHit(int currentHp)
        {
            GameOver = currentHp <= 0;
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
            _playerObjectCollided.transform.position = _targetPortalPosition + new Vector3(2, 0, 2);
            _playerObjectCollided.SetActive(true);
            _cutScenesCamera.GetComponent<CameraFollow>().enabled = true;
            if (_spawnedPortals) DestroyPortals();
            _portalSpawnTimer = PortalSpawnTime;
            _spawnedPortals = false;
        }

        private void DestroyPortals()
        {
            foreach (var portal in _portals)
                Destroy(portal);
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

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            this._currentLevel = SceneManager.GetActiveScene().buildIndex;
            LevelText.text = "Level " + _currentLevel;
            if (scene.name == "GameOver")
            {
                Destroy(this.gameObject);
            }
        }

        // ReSharper disable once InconsistentNaming
        public void AddXP(float xP)
        {
            _playerXP += xP;
            XPScript.Instance.UpdateXP(_playerXP);
        }
    }
}