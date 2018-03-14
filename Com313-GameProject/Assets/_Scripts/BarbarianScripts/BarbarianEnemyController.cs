using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Assertions;

public class BarbarianEnemyController : MonoBehaviour
{
    private Transform player;
    private NavMeshAgent navAgent;
    private Animator anim;
    private EnemyHealth enemyHealth;

    void Start()
    {
        player = GameManager.instance.player.transform;
        navAgent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        enemyHealth = GetComponent<EnemyHealth>();
    }

    void Update()
    {
        if (enemyHealth.IsAlive)
        {
            if (!GameManager.instance.GameOver)
                navAgent.SetDestination(player.position);
            else
            {
                navAgent.enabled = false;
                anim.Play("Idle");
            }
        }
        else
            navAgent.enabled = false;
        
    }
}
