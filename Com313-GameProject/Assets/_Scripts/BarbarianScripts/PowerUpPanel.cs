using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace _Scripts.BarbarianScripts
{
	public class PowerUpPanel : MonoBehaviour
	{
		public Image PowerUpImage;
		public Sprite DefaultImage;
		private RectTransform _rectTransform;

		public static PowerUpPanel Instance { get; private set; }

		private void Awake () {
			if(Instance == null)
				Instance = this;
			else if (Instance != this)
				Destroy(gameObject);
		}
		
		private void Start ()
		{
			_rectTransform = PowerUpImage.GetComponent<RectTransform>();
			StartCoroutine(PowerUp(DefaultImage, 0, "none"));
		}

		public IEnumerator PowerUp(Sprite image, int loop, string powerUp)
		{
			SetImage(image, powerUp);
			yield return new WaitForSeconds(loop);
			SetImage(DefaultImage, "none");
		}

		private void SetImage(Sprite image, string powerUp)
		{
			_rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, powerUp == "speed" ? 50 : 80);
			PowerUpImage.color = powerUp == "none" ? new Color(0,0,0,0.2f) : new Color(1,1,1,1);
			PowerUpImage.sprite = image;
		}
	}
}
