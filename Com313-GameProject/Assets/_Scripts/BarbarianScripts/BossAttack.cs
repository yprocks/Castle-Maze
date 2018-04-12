using System;
using UnityEngine;

namespace _Scripts.BarbarianScripts
{
	public class BossAttack : MonoBehaviour
	{
		public GameObject FireTornado;
		public float AttackTime;
		
		private AudioSource _audioSource;
		private GameObject _player;
		private bool _specialAttack;
		private float _timer = 5f;
		private Animator _animator;
		
		private void Start ()
		{
			_audioSource = GetComponents<AudioSource>()[1];
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
				_audioSource.Play();
				_timer = AttackTime;
				_specialAttack = true;				
			}
		}
		
		
		public void BossFireTornadoAttack()
		{
			if(!_specialAttack) return;
			Instantiate(FireTornado, transform.position, Quaternion.identity);
			_specialAttack = false;
		}
	}
}
