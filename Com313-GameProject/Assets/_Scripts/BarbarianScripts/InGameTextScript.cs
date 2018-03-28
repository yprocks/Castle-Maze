using UnityEngine;

namespace _Scripts.BarbarianScripts
{
	public class InGameTextScript : MonoBehaviour
	{
		public string DisplayText;
		
		private InGameTextManager _manager;
		
		// Use this for initialization
		private void Start () {
			_manager = InGameTextManager.GetInstance();
		}

		private void OnTriggerEnter(Collider other)
		{
			if(other.gameObject.CompareTag("Player"))
			_manager.ShowPanel(DisplayText);
		}

		private void OnTriggerExit(Collider other)
		{
			if(other.gameObject.CompareTag("Player"))
			_manager.HidePanel();
		}
	}
}
