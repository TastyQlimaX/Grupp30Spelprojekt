using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int maxhealth;
    private int currentHealth;
    private SpriteRenderer RedFlash;
    public ParticleSystem deathParticles;
    

    public float flashDuration = 0.3f;
    private void Start()
    {
        currentHealth = maxhealth;
        RedFlash = GetComponent<SpriteRenderer>();
    }

    public void TakeDamage(int damage)
    {
        RedFlash.color = Color.red;
        currentHealth -= damage;
        
        //play hurt animation
        Invoke(nameof(ColorChange), flashDuration);

        if (currentHealth <= 0)
        {
            var em = deathParticles.emission;
            
            em.enabled = true;
            deathParticles.Play();

            RedFlash.enabled = false;
            Invoke(nameof(Die), 5);
        }
    }

    void ColorChange()
    {
        RedFlash.color = Color.white;
    }
    void Die()
    {
        Destroy(this);
        //Die animation
        
        
        //Disable enemy
    }
}
