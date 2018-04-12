using UnityEngine;

namespace _Scripts.BarbarianScripts
{
	public class BossFireAttack : SkillDamage {

		public float Speed = 7f;

		private GameObject _player;
		private Vector3 _direction;
		
		private void Start()
		{
			_player = GameManager.Instance.Player;
			transform.rotation = Quaternion.LookRotation(_player.transform.forward);
			_direction = _player.transform.position - transform.position;
		}

		protected new void Update ()
		{
			base.Update();
			MoveForward();
		}

		private void MoveForward()
		{
			transform.Translate(Vector3.forward * Speed * Time.deltaTime);
		}

	}
}
