using UnityEngine;
using _Scripts.BarbarianScripts;

namespace _Scripts.LevelHelpers
{
	public class LevelExitAnimation : MonoBehaviour
	{
		public GameObject AnimObject;
		public GameObject Camera;
		public GameObject Level2DoorGameObject;
		
		private bool _hasPlayedAnim;

		private void Start()
		{
			_hasPlayedAnim = false;
		}

		private void OnTriggerEnter(Collider other)
		{
			if(!other.gameObject.CompareTag("Player") || _hasPlayedAnim) return;
			
			if (GameManager.Instance.CurrentLevel == 1 )
			{
				GameManager.Instance.Player.SetActive(false);
				AnimObject.SetActive(true);
				Camera.GetComponent<Animator>().Play("Level1Door");
				_hasPlayedAnim = true;
			}
			else
			{
				if (!GameManager.Instance.EnemyKilled()) return;

				Level2DoorGameObject.GetComponent<Animator>().Play("Level2Door");

				_hasPlayedAnim = true;
			}
		}
		
		
	}
}
