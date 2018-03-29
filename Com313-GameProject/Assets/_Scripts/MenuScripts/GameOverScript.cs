using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using _Scripts.Statics;

namespace _Scripts.MenuScripts
{
	public class GameOverScript : MenuScript
	{
		public Text TitleText;
		public Text BarbarianText;

		public GameObject StartButton;
		public GameObject QuitButton;

		public AudioClip EnemyHurtSound;
		public AudioClip HeroHurtSound;
		
		private Color _barbarianColor;
		private Color _orcsColor;
		private AudioSource _audioSource;

		private void Start()
		{
			GetAnimComponents();
			TitleText.enabled = false;
			BarbarianText.enabled = false;
			StartButton.SetActive(false);
			QuitButton.SetActive(false);
			_barbarianColor = new Color((37.0f/255.0f),(81.0f/255.0f),(19.0f/255.0f));
			_orcsColor = new Color((159.0f/255.0f),(8.0f/255.0f),(8.0f/255.0f));
			_audioSource = GetComponent<AudioSource>();
			HeroAnimator.SetBool("isWalking", true);

			var gameOver = PlayerPrefs.GetInt("GameOver");
			
			if (gameOver == (int) GameState.PlayerWon)
			{
				TitleText.color = _barbarianColor;
				BarbarianText.color = _barbarianColor;
				TitleText.text = "Vicrory To";
				BarbarianText.text = "Barbarian";
				_audioSource.clip = EnemyHurtSound;
				StartCoroutine(Victory());		
			}
			else
			{
				TitleText.color = _orcsColor;
				BarbarianText.color = _orcsColor;
				TitleText.text = "Game Over";
				BarbarianText.text = "Orcs won";
				_audioSource.clip = HeroHurtSound;
				StartCoroutine(Defeat());
			}
			PlayerPrefs.SetFloat("PlayerXP", 0);
		}

		private IEnumerator Victory()
		{
			yield return new WaitForSeconds(2f);
			HeroAnimator.Play("SpinAttack");	
			yield return new WaitForSeconds(0.5f);
			SoldierAnimator.Play("Die");
			_audioSource.Play();
			yield return new WaitForSeconds(0.1f);
			RangerAnimator.Play("Die");
			_audioSource.Play();
			yield return new WaitForSeconds(0.1f);
			TankerAnimator.Play("Die");
			_audioSource.Play();
			yield return new WaitForSeconds(0.5f);
			HeroAnimator.SetBool("isWalking", false);
			yield return new WaitForSeconds(0.5f);
			TitleText.enabled = true;
			BarbarianText.enabled = true;
			StartButton.SetActive(true);
			QuitButton.SetActive(true);
			yield return null;
		}
		
		private IEnumerator Defeat()
		{
			yield return new WaitForSeconds(2f);
			TankerAnimator.Play("Attack");
			SoldierAnimator.Play("Attack");
			RangerAnimator.Play("Attack");
			yield return new WaitForSeconds(1f);
			HeroAnimator.Play("Hurt");
			_audioSource.Play();
			yield return new WaitForSeconds(1f);
			HeroAnimator.Play("Die");
			_audioSource.Play();
			TankerAnimator.Play("Idle");
			SoldierAnimator.Play("Idle");
			RangerAnimator.Play("Idle");
			yield return new WaitForSeconds(0.5f);
			TitleText.enabled = true;
			BarbarianText.enabled = true;
			StartButton.SetActive(true);
			QuitButton.SetActive(true);
			yield return null;
		}

	}
}
