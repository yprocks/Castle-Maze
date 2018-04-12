using UnityEngine;

namespace _Scripts.BarbarianScripts
{
	public class BossAttack : MonoBehaviour
	{
		public GameObject FireTornado;
		public float AttackTime;
		
		private GameObject _player;
		private float _timer = 5f;
		private Animator _animator;
		
		private void Start ()
		{
			_player = GameManager.Instance.Player;
			_animator = GetComponent<Animator>();
		}

		private void Update()
		{
			if (!(Vector3.Distance(transform.position, _player.transform.position) > 5f)) return;
			_timer -= Time.deltaTime;
			
			// ReSharper disable once InvertIf
			if (_timer <= 0)
			{
				_animator.Play("Attack");
				_timer = AttackTime;
			}
		}
		
		
		public void BossFireTornadoAttack()
		{
			Instantiate(FireTornado, transform.position, Quaternion.identity);

		}
	}
}
