using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Pussel : MonoBehaviour
{
    public float health, maxHealth = 10f;
    
    
    void Start()
    {
        health = maxHealth;
    }

    public void TakeDamage(float damageAmount)
    {
       health -= damageAmount;
        if (health <= 0) Destroy(gameObject);
    }
    void Update()
    {
        if (isControlJustPressed.E) TakeDamage();
    }
}
