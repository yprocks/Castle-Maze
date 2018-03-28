using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Scripts.MenuScripts
{
	public class MenuScript : MonoBehaviour
	{
		[SerializeField] private GameObject _hero;
		[SerializeField] private GameObject _tanker;
		[SerializeField] private GameObject _ranger;
		[SerializeField] private GameObject _soldier;

		private Animator _heroAnimator;
		private Animator _tankerAnimator;
		private Animator _rangerAnimator;
		private Animator _soldierAnimator;
		
		private void Start ()
		{
			_heroAnimator = _hero.GetComponent<Animator>();
			_tankerAnimator = _tanker.GetComponent<Animator>();
			_rangerAnimator = _ranger.GetComponent<Animator>();
			_soldierAnimator = _soldier.GetComponent<Animator>();
			_heroAnimator.SetBool("isWalking", true);
			StartCoroutine(ShowCase());
		}

		// ReSharper disable once FunctionRecursiveOnAllPaths
		private IEnumerator ShowCase()
		{
			yield return new WaitForSeconds(2f);
			_heroAnimator.Play("SpinAttack");	
			yield return new WaitForSeconds(2f);
			_tankerAnimator.Play("Attack");
			yield return new WaitForSeconds(3f);
			_heroAnimator.Play("DoubleChop");
			yield return new WaitForSeconds(2f);
			_soldierAnimator.Play("Attack");
			yield return new WaitForSeconds(2f);
			_rangerAnimator.Play("Attack");
			
			StartCoroutine(ShowCase());
		}

		public void StartGame()
		{
			SceneManager.LoadScene(1);
		}

		public void Quit()
		{
			Application.Quit();
		}
	}
}
