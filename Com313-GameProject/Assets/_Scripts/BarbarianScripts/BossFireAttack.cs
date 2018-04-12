using UnityEngine;

namespace _Scripts.BarbarianScripts
{
	public class BossFireAttack : BossSkillDamage {

		public float Speed = 7f;

		private GameObject _boss;
		private Vector3 _direction;
		
		private void Start()
		{
			_boss = GameObject.FindGameObjectWithTag("Boss");
			transform.rotation = Quaternion.LookRotation(_boss.transform.forward);
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
