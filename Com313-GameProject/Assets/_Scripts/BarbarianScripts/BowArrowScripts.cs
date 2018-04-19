using UnityEngine;

namespace _Scripts.BarbarianScripts
{
	public class BowArrowScripts : MonoBehaviour
	{
		public GameObject[] Arrows;
		public string AnimClipName;
		
		private void OnTriggerEnter(Collider other)
		{
			if(!other.gameObject.CompareTag("Player")) return;
			foreach (var arrow in Arrows)
				arrow.GetComponent<Animation>().Play(AnimClipName);
			
		}
		
		private void OnTriggerExit(Collider other)
		{
			if(!other.gameObject.CompareTag("Player")) return;

			foreach (var arrow in Arrows)
			{
				var anim = arrow.GetComponent<Animation>();
				var clip = anim.GetClip(AnimClipName);
				anim.Stop();
				clip.SampleAnimation(arrow, 0);
			}
		}
	}
}
