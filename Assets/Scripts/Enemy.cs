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

    void Update()
    {
        transform.Translate(0, 0, speed * Time.deltaTime);
    }
}
