using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private float range = 3f;
    [SerializeField] private float attackRate = 1f;

    private Animator anim;
    private GameObject player;
    private bool playerInRange;
    private EnemyHealth enemyHealth;
    private BoxCollider[] weaponColliders;

    void Start()
    {
        weaponColliders = GetComponentsInChildren<BoxCollider>();
        enemyHealth = GetComponent<EnemyHealth>();
        player = GameManager.instance.Player;
        anim = GetComponent<Animator>();
        StartCoroutine(Attack());
    }

    void Update()
    {
        if (Vector3.Distance(transform.position, player.transform.position) < range && enemyHealth.IsAlive)
            playerInRange = true;
        else
            playerInRange = false;
    }

    IEnumerator Attack()
    {
        if (playerInRange && !GameManager.instance.GameOver)
        {
            anim.Play("Attack");
            yield return new WaitForSeconds(attackRate);
        }

        yield return null;
        StartCoroutine(Attack());

    }

    public void EnemyBeginAttack()
    {
        foreach (var _collider in weaponColliders)
            _collider.enabled = true;
    }

    public void EnemyEndAttack()
    {
        foreach (var _collider in weaponColliders)
            _collider.enabled = false;
    }
}
