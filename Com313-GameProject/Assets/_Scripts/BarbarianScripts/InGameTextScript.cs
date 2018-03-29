using UnityEngine;

namespace _Scripts.BarbarianScripts
{
	public class InGameTextScript : MonoBehaviour
	{
		public string DisplayText;
		public string DisplayTitleText;
		
		private InGameTextManager _manager;
		
		// Use this for initialization
		private void Start () {
			_manager = InGameTextManager.GetInstance();
		}

		private void OnTriggerEnter(Collider other)
		{
			if(other.gameObject.CompareTag("Player"))
			_manager.ShowPanel(DisplayTitleText, DisplayText);
		}

		private void OnTriggerExit(Collider other)
		{
			if(other.gameObject.CompareTag("Player"))
			_manager.HidePanel();
		}
	}
}
