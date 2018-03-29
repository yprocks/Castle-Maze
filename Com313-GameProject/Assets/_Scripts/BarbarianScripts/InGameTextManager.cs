using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace _Scripts.BarbarianScripts
{
	public class InGameTextManager : MonoBehaviour
	{
		private static InGameTextManager _instance;

		public string StartText;
		public string StartTextTitle;
		public Text InGameText;
		public Text InGameTextTitle;
		
		// ReSharper disable once InconsistentNaming
		public GameObject InGameTextUIPanel; 
	
		private void Awake()
        {
            if (_instance == null)
                _instance = this;
            else if (_instance != this)
                Destroy(gameObject);        
        }

		public static InGameTextManager GetInstance()
		{
			return _instance;
		}
		
		// Use this for initialization
		private void Start () 
		{
			SetText(StartTextTitle, StartText);
			Invoke("DisablePanel", 5f);
		}

		private void DisablePanel()
		{
			SetText("", "");
			HidePanel();
		}
		
		private void SetText(string title, string text)
		{
			InGameTextTitle.text = title;
			InGameText.text = text;
		}

		private void ShowPanel()
		{
			InGameTextUIPanel.SetActive(true);
		}
		
		private IEnumerator ShowPanel(int seconds)
		{
			InGameTextUIPanel.SetActive(true);
			yield return new WaitForSeconds(seconds);
			InGameTextUIPanel.SetActive(false);
		}

		public void ShowPanel(string title, string text)
		{
			SetText(title, text);
			ShowPanel();
		}

		public void HidePanel()
		{
			InGameTextUIPanel.SetActive(false);
		}

		public void ShowPanelForSeconds(string title, string text, int seconds)
		{
			SetText(title, text);
			StartCoroutine(ShowPanel(seconds));
		}
	}
}