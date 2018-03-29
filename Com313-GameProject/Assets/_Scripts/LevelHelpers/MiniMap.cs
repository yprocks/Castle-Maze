using UnityEngine;

namespace _Scripts
{
	public class MiniMap : MonoBehaviour {

		public Transform Target;

		// Use this for initialization

		// Update is called once per frame
		private void Update()
		{
			transform.position = new Vector3(Target.position.x, transform.position.y, Target.position.z);
		}
	}
}
