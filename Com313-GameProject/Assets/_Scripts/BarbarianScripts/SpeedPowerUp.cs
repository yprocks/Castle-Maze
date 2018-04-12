using UnityEngine;

namespace _Scripts.BarbarianScripts
{
	public class SpeedPowerUp : PowerUp
	{
		public Sprite SpeedSprite;
		
		private void OnTriggerEnter(Collider other)
		{
			if(!other.gameObject.CompareTag("Player"))
				return;
			PlayerController.SpeedPowerUp();
			GameManager.Instance.UnRegisterPowerUp();
			StartCoroutine(PowerUpPanel.Instance.PowerUp(SpeedSprite, 10, "speed"));
			GetComponent<SpriteRenderer>().enabled = false;
			GetComponent<SphereCollider>().enabled = false;
			Destroy(gameObject, 12);
		}
	}
}