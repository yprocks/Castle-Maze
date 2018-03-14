using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int health = 100;
    [SerializeField] private float timeSinceLastHit = 2f;
    public Image healthIcon;

    private float timer;

    private CharacterController charController;
    private PlayerController playerController;
    private Animator anim;
    private AudioSource audioSource;
    private int currentHealth;

    void Start()
    {
        anim = GetComponent<Animator>();
        charController = GetComponent<CharacterController>();
        playerController = GetComponent<PlayerController>();
        audioSource = GetComponent<AudioSource>();
        currentHealth = health;
        healthIcon.fillAmount = (float) currentHealth / health;
    }

    void Update()
    {
        timer += Time.deltaTime;

    }

    void OnTriggerEnter(Collider other)
    {
        if (timer >= timeSinceLastHit && !GameManager.instance.GameOver)
        {
            if (other.tag == "Weapon")
            {
                TakeHit();
                timer = 0;
            }
        }
    }

    private void TakeHit()
    {
        if (currentHealth > 0)
        {
            currentHealth -= 10;
            healthIcon.fillAmount = (float) currentHealth / health;
            GameManager.instance.PlayerHit(currentHealth);
            anim.Play("Hurt");
            audioSource.PlayOneShot(audioSource.clip);
        }

        if (currentHealth <= 0)
            KillPlayer();

    }

    private void KillPlayer()
    {
        GameManager.instance.PlayerHit(currentHealth);
        anim.SetTrigger("heroDie");
        charController.enabled = false;
        playerController.enabled = false;
        audioSource.PlayOneShot(audioSource.clip);
    }


}