using UnityEngine;
using _Scripts.BarbarianScripts;

namespace _Scripts.LevelHelpers
{
	public class CameraAnimScript : MonoBehaviour
	{
		public GameObject DoorAnim;
		
		public void OnAnimationComplete()
		{
			DoorAnim.GetComponent<Animation>().Play();
		}
	}
}
