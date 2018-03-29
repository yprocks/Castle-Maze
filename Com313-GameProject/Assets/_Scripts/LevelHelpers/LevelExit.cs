using UnityEngine;
using _Scripts.BarbarianScripts;

namespace _Scripts.LevelHelpers
{
    public class LevelExit : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (!other.gameObject.CompareTag("Player")) return;
            GameManager.Instance.FoundExit();
        }
    }
}