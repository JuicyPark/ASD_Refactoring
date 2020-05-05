using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    [SerializeField]
    float damage = 10f;
    [SerializeField]
    float attackDelay = 0.5f;
    float colapseTime = 0;

    bool attackable = true;

    [SerializeField]
    Animator animator;

    void Update()
    {
        if(colapseTime < attackDelay)
        {
            colapseTime += Time.deltaTime;
        }
        else
        {
            colapseTime = attackDelay;
            attackable = true;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (!attackable)
            return;
        Attack(other);
    }

    void Attack(Collider enemy)
    {
        transform.LookAt(enemy.transform);
        animator.SetTrigger("isAttack");
        enemy.GetComponent<Enemy>().TakeDamage(damage);
        attackable = false;
    }
}
