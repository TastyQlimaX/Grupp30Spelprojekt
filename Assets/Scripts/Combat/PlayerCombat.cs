using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCombat : MonoBehaviour
{
    public PlayerInputActions playerInputActions;
    public ParticleSystem AttackParticles;
    public Vector3 Attackpoint;
    private Rigidbody RB;
    public LayerMask enemyLayers;

    public float attackRange;
    public int attackDamage = 40;
    public float attackRate = 2f;
    private float nextAttackTime = 0f;
    public void Awake()
    {
        RB = GetComponent<Rigidbody>();
        GetComponent<PlayerInput>();
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();
        playerInputActions.Player.Attack.performed += Attack;
    }

    private void Attack(InputAction.CallbackContext context)
    {
        if (Time.time >= nextAttackTime)
        {
            if (context.performed)
            {
                nextAttackTime = Time.time + 1f / attackRate;
                var em = AttackParticles.emission;
                Debug.Log("attack button pressed");

                em.enabled = true;
                AttackParticles.Play();

                Attackpoint = RB.position;
                Collider[] hitEnemies = Physics.OverlapSphere(Attackpoint, attackRange, enemyLayers);
                foreach (Collider enemy in hitEnemies)
                {
                    enemy.GetComponent<Enemy>().TakeDamage(attackDamage);
                }
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (Attackpoint == null)
            return;
        
        Gizmos.DrawWireSphere(Attackpoint, attackRange);
    }
}
