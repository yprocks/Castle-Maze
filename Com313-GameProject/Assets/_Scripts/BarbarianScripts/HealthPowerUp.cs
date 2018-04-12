using UnityEngine;

namespace _Scripts.BarbarianScripts
{
	public class HealthPowerUp : PowerUp
	{
		public Sprite HealthSprite;
		
		private void OnTriggerEnter(Collider other)
		{
			if(!other.gameObject.CompareTag("Player")) return;
			PlayerHealth.PowerUpHealth();
			GameManager.Instance.UnRegisterPowerUp();
			StartCoroutine(PowerUpPanel.Instance.PowerUp(HealthSprite, 2, "health"));
			this.GetComponent<SpriteRenderer>().enabled = false;
			this.GetComponent<SphereCollider>().enabled = false;
			Destroy(gameObject, 4);
		}
	}
}