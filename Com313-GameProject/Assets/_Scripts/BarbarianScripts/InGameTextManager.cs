using System;
using UnityEngine;
using UnityEngine.UI;

namespace _Scripts.BarbarianScripts
{
	public class InGameTextManager : MonoBehaviour
	{
		private static InGameTextManager _instance;
		
		public Text InGameText;
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
			Invoke("DisablePanel", 5f);
		}

		private void DisablePanel()
		{
			SetText("");
			HidePanel();
		}
		
		private void SetText(string text)
		{
			InGameText.text = text;
		}
		
		private void ShowPanel()
		{
			InGameTextUIPanel.SetActive(true);
		}

		public void ShowPanel(string text)
		{
			SetText(text);
			ShowPanel();
		}

		public void HidePanel()
		{
			InGameTextUIPanel.SetActive(false);
		}

		public void ShowPanelForSeconds(string text, int seconds)
		{
			SetText(text);
			Invoke("ShowPanel", seconds);
		}
	}
}