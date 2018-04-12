using UnityEngine;

namespace _Scripts.BarbarianScripts
{
	public class BossSkillDamage : MonoBehaviour {

		public LayerMask EnemyLayer;
		public float Radius;
		public int Damage;

		private PlayerHealth _playerHealth;
		private bool _collided;
		
		protected void Update ()
		{
			var hits = Physics.OverlapSphere(transform.position,
				Radius, EnemyLayer);
			    
			foreach (var hitCollider  in hits)
			{
				_playerHealth = hitCollider.GetComponent<PlayerHealth>();
				_collided = true;
			}
			
			if (!_collided) return;
			if(_playerHealth)
				_playerHealth.TakeHit(Damage);
			_collided = false;
			Destroy(gameObject);
		}
	}
}
