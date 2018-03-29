using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace _Scripts
{
	// ReSharper disable once InconsistentNaming
	public class XPScript : MonoBehaviour {

		// ReSharper disable once MemberCanBePrivate.Global
		public static XPScript Instance;
		
		// ReSharper disable once InconsistentNaming
		public Image XPBar;
		// ReSharper disable once InconsistentNaming
		public float XPBarLimit;
		
		private Text _text;

		private float _newFill;
		private bool _startFill;

		// Use this for initialization
		private void Awake () {
			if (Instance == null)
				Instance = this;
			else if (Instance != this)
				Destroy(gameObject);
		}
		
		private void Start()
		{
			_text = GetComponent<Text>();
			_text.text = "0 XP";
		}

		// ReSharper disable once InconsistentNaming
		public void UpdateXP(float xP)
		{
			_newFill = (xP / XPBarLimit) % XPBarLimit;
			_text.text = xP + " XP";
			_startFill = true;
		}

		private void Update()
		{
			if (!_startFill) return;
			XPBar.fillAmount = Mathf.Lerp(XPBar.fillAmount, _newFill, Time.deltaTime * 2.5f);
			if (!(XPBar.fillAmount >= _newFill)) return;
			_startFill = false;
		}

	}
}
