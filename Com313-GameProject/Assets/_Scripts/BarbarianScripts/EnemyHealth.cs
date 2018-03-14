using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int Health = 20;
    [SerializeField] private float timeSinceLastHit = 0.5f;
    [SerializeField] private float destroySpeed = 2f;

    private AudioSource audioSource;
    private NavMeshAgent navAgent;
    private Rigidbody _rigidbody;
    private Animator anim;
    private CapsuleCollider capsuleCollider;
    private bool isAlive;
    private int currentHealth;
    private bool destroyEnemy = false;
    private float timer = 0f;

    public bool IsAlive
    {
        get { return isAlive; }
    }

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        capsuleCollider = GetComponent<CapsuleCollider>();
        navAgent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        isAlive = true;
        currentHealth = Health;
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (destroyEnemy)
        {
            transform.Translate(-Vector3.up * destroySpeed * Time.deltaTime);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (timer >= timeSinceLastHit && !GameManager.instance.GameOver && isAlive)
        {
            if (other.tag == "PlayerWeapon")
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
            anim.Play("Hurt");
            audioSource.PlayOneShot(audioSource.clip);
        }

        if (currentHealth <= 0)
        {
            isAlive = false;
            ScoreManager.instance.KillEnemy();
            KillEnemy();
        }
    }

    private void KillEnemy()
    {
        capsuleCollider.enabled = false;
        anim.SetTrigger("enemyDie");
        _rigidbody.isKinematic = true;
        navAgent.enabled = false;
        audioSource.PlayOneShot(audioSource.clip);
        StartCoroutine(RemoveEnemy());
    }

    IEnumerator RemoveEnemy()
    {
        yield return new WaitForSeconds(4f);
        destroyEnemy = true;
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }

}
