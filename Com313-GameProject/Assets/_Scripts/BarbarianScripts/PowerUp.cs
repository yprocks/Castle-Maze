using UnityEngine;

namespace _Scripts.BarbarianScripts
{
	public class PowerUp : MonoBehaviour {
		
		private GameObject _player;
		protected PlayerHealth PlayerHealth;
		protected PlayerController PlayerController;

		protected void Start () {
			_player = GameManager.Instance.Player;
			PlayerHealth = _player.GetComponent<PlayerHealth>();
			PlayerController = _player.GetComponent<PlayerController>();
			GameManager.Instance.RegisterPowerUp();
		}
	
		private void Update()
		{
			transform.RotateAround(transform.position, Vector3.up, 5f);
		}
	}
}
