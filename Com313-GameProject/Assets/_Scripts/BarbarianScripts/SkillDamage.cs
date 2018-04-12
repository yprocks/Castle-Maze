using UnityEngine;

namespace _Scripts.BarbarianScripts
{
	public class SkillDamage : MonoBehaviour
	{
		public LayerMask EnemyLayer;
		public float Radius;
		public int Damage;

		private EnemyHealth _enemyHealth;
		private bool _collided;

		protected void Update ()
		{
			var hits = Physics.OverlapSphere(transform.position,
				Radius, EnemyLayer);
			
			foreach (var hitCollider in hits)
			{
				_enemyHealth = hitCollider.GetComponent<EnemyHealth>();
				_collided = true;
			}
			
			if (!_collided) return;
			if(_enemyHealth)
				_enemyHealth.TakeHit(Damage);
			_collided = false;
			Destroy(gameObject);
		}
	}
}