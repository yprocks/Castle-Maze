using UnityEngine;

namespace _Scripts.BarbarianScripts
{
	public class TriggerBoss : MonoBehaviour {
		private void OnTriggerEnter(Collider other)
		{
			if(!other.gameObject.CompareTag("Player")) return;
			GameManager.Instance.TriggerBoss = true;
		}
	}
}
