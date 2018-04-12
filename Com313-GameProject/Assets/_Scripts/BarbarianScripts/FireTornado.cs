using UnityEngine;

namespace _Scripts.BarbarianScripts
{
	public class FireTornado : SkillDamage {

		public float Speed = 7f;
		
		private GameObject _player;
		
		private void Start()
		{
			_player = GameManager.Instance.Player;
			transform.rotation = Quaternion.LookRotation(_player.transform.forward);
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
