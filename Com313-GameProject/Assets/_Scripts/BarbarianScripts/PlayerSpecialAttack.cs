using UnityEngine;
using UnityEngine.UI;

namespace _Scripts.BarbarianScripts
{
	public class PlayerSpecialAttack : MonoBehaviour {

		public Image FillImage1;
		public Image FillImage2;

		public Transform FireTornadoSpawn;
		public GameObject FireTornado;
		public GameObject Thunder;

		private readonly int[] _fadeImages = new int[] {0, 0};
		private bool _canAttack = true;

		private Animator _animator;

		public bool LigntningSpecialAttack { get; private set; }
		public bool TornadoSpecialAttack { get; private set; }

		private void Start ()
		{
			_animator = GetComponent<Animator>();
			LigntningSpecialAttack = false;
			TornadoSpecialAttack = false;
		}

		private void Update ()
		{
			CheckInput();
			CheckToFace();
		}

		private void CheckToFace()
		{
			if (_fadeImages[0] == 1)
			{
				if (FadeAndWait(FillImage1, 0.3f))
					_fadeImages[0] = 0;
			}
			// ReSharper disable once InvertIf
			if (_fadeImages[1] == 1)
			{
				if (FadeAndWait(FillImage2, 0.3f))
					_fadeImages[1] = 0;
			}
		}

		private void CheckInput()
		{
			if (Input.GetKeyDown(KeyCode.Alpha1) && _canAttack)
			{
				_fadeImages[0] = 1;
				_animator.Play("DoubleChop");
				_canAttack = false;
				LigntningSpecialAttack = true;
			}
			else if (Input.GetKeyDown(KeyCode.Alpha2) && _canAttack)
			{
				_fadeImages[1] = 1;
				_animator.Play("SpinAttack");
				_canAttack = false;
				TornadoSpecialAttack = true;
			}
		}

		public void LightningSpecialAttack()
		{
			for (var i = 0; i < 8; i++)
			{
				var pos = Vector3.zero;

				switch (i)
				{
					case 0:
						pos = new Vector3(transform.position.x - 4, transform.position.y + 2f, transform.position.z);
						break;
					case 1:
						pos = new Vector3(transform.position.x + 4, transform.position.y + 2f, transform.position.z);
						break;
					case 2:
						pos = new Vector3(transform.position.x, transform.position.y + 2f, transform.position.z - 4f);
						break;
					case 3:
						pos = new Vector3(transform.position.x, transform.position.y + 2f, transform.position.z + 4f);
						break;
					case 4:
						pos = new Vector3(transform.position.x + 2.5f, transform.position.y + 2f, transform.position.z - 2.5f);
						break;
					case 5:
						pos = new Vector3(transform.position.x -2.5f, transform.position.y + 2f, transform.position.z + 2.5f);
						break;
					case 6:
						pos = new Vector3(transform.position.x + -2.5f, transform.position.y + 2f, transform.position.z-2.5f);
						break;
					case 7:
						pos = new Vector3(transform.position.x + 2.5f, transform.position.y + 2f, transform.position.z + 2.5f);
						break;
					default: break;
				}
				
				Instantiate(Thunder, pos, Quaternion.identity);
				LigntningSpecialAttack = false;
			}
		}

		public void FireTornadoSpecilAttak()
		{
			Instantiate(FireTornado, FireTornadoSpawn.transform.position, Quaternion.identity);
			TornadoSpecialAttack = false;
		}
		
		private bool FadeAndWait(Image fadeImg, float fadeTime)
		{
			var faded = false;

			if (fadeImg == null)
				return false;

			if (!fadeImg.gameObject.activeInHierarchy)
			{
				fadeImg.gameObject.SetActive(true);
				fadeImg.fillAmount = 1;
			}

			fadeImg.fillAmount -= fadeTime * Time.deltaTime;

			// ReSharper disable once InvertIf
			if (fadeImg.fillAmount <= 0)
			{
				fadeImg.gameObject.SetActive(false);
				faded = true;
				_canAttack = true;
			}

			return faded;
		}
	}
}
