using UnityEngine;

namespace _Scripts.BarbarianScripts
{
	public class DestroyAfterTime : MonoBehaviour
	{
		public float DestroyTime;

		private void Start () {
			Destroy(gameObject, DestroyTime);
		}	

	}
}
