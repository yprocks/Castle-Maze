using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Scripts
{
	public class LevelExit : MonoBehaviour {
		
		private void OnTriggerEnter(Collider other)
		{
			if(other.gameObject.CompareTag("Player"))
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
		}
	}
}