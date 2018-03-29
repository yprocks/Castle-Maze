using UnityEngine;
using _Scripts.BarbarianScripts;

namespace _Scripts.LevelHelpers
{
	public class DoorAnimScript : MonoBehaviour
	{

		public GameObject DoorAnimGameObject;
		
		public void OnAnimationComplete()
		{
			GameManager.Instance.Player.SetActive(true);
				DoorAnimGameObject.SetActive(false);
		}
	}
}
