using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using _Scripts.Statics;

namespace _Scripts.MenuScripts
{
	public class MenuScript : MonoBehaviour
	{
		[SerializeField] protected GameObject Hero;
		[SerializeField] protected GameObject Tanker;
		[SerializeField] protected GameObject Ranger;
		[SerializeField] protected GameObject Soldier;

		protected Animator HeroAnimator;
		protected Animator TankerAnimator;
		protected Animator RangerAnimator;
		protected Animator SoldierAnimator;
		
		private void Start ()
		{
			GetAnimComponents();
			HeroAnimator.SetBool("isWalking", true);
			StartCoroutine(ShowCase());
		}

		protected void GetAnimComponents()
		{
			HeroAnimator = Hero.GetComponent<Animator>();
			TankerAnimator = Tanker.GetComponent<Animator>();
			RangerAnimator = Ranger.GetComponent<Animator>();
			SoldierAnimator = Soldier.GetComponent<Animator>();
		}

		// ReSharper disable once FunctionRecursiveOnAllPaths
		private IEnumerator ShowCase()
		{
			yield return new WaitForSeconds(2f);
			HeroAnimator.Play("SpinAttack");	
			yield return new WaitForSeconds(2f);
			TankerAnimator.Play("Attack");
			yield return new WaitForSeconds(3f);
			HeroAnimator.Play("DoubleChop");
			yield return new WaitForSeconds(2f);
			SoldierAnimator.Play("Attack");
			yield return new WaitForSeconds(2f);
			RangerAnimator.Play("Attack");
			
			StartCoroutine(ShowCase());
		}

		public void StartGame()
		{
			PlayerPrefs.SetFloat("PlayerXP", 0);
			PlayerPrefs.SetInt("GameOver", (int)GameState.GameOn);
			SceneManager.LoadScene(1);
		}

		public void Quit()
		{
			Application.Quit();
		}
	}
}
