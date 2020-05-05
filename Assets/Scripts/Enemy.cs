using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    float speed = 5f;

    float maxHealth;
    float currentHealth;

    public void SetHealth(float health)
    {
        maxHealth = health;
        currentHealth = health;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth < 0)
            Destroy(gameObject);
    }

    void Update()
    {
        transform.Translate(0, 0, speed * Time.deltaTime);
    }
}
