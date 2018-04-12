using UnityEngine;

namespace _Scripts.BarbarianScripts
{
	public class Arrow : MonoBehaviour {
		
		private void Start()
		{
			Destroy(gameObject, 5f);	
		}

		private void OnCollisionEnter(Collision other)
		{
			Destroy(gameObject);
		}
	}
}
