using UnityEngine;

namespace _Scripts.BarbarianScripts
{
	public class NeedleTrapScripts : MonoBehaviour
	{
		private Animation _animation;
		private AnimationClip[] _animationClip;

		private void Start()
		{
			_animationClip = new AnimationClip[2];
			_animation = GetComponent<Animation>();
			_animationClip[0] = _animation.GetClip("Anim_TrapNeedle_Idle");
			_animationClip[1] = _animation.GetClip("Anim_TrapNeedle_Play");
		}

		private void OnTriggerEnter(Collider other)
		{
			if(!other.gameObject.CompareTag("Player")) return;
			_animation.clip = _animationClip[1];
			_animation.Play();
		}

		private void OnTriggerExit(Collider other)
		{
			if(!other.gameObject.CompareTag("Player"))return;
			_animation.clip = _animationClip[0];
			_animation.Play();
		}
	}
}
