using UnityEngine;
using UnityEngine.UI;
using _Scripts.BarbarianScripts;

namespace _Scripts
{
    public class ScoreManager : MonoBehaviour
    {
        public static ScoreManager Instance;

        public Image EnemyCountBar;

        private Text _text;
        private int _enemy;
        private int _totalEnemies;
    
        // Use this for initialization
        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else if (Instance != this)
                Destroy(gameObject);
        
        }

        private void Start()
        {
            _text = GetComponent<Text>();
            _totalEnemies = GameManager.Instance.TotalEnemies;
            _enemy = _totalEnemies;
            _text.text = "Enemies: " + _enemy;
        }

        public void KillEnemy()
        {
            _enemy--;
            _text.text = "Enemies: " + _enemy;
            EnemyCountBar.fillAmount = (float)_enemy / _totalEnemies;
        }
    }
}