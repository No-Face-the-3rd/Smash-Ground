using UnityEngine;
using System.Collections;

public class RangedEnemyAnimations : MonoBehaviour
{
    private Rigidbody rb;
    private Animator anim;
    private bool shoot;
    private int health;
	
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        shoot = GetComponent<ShootClosestPlayer>().shoot;
        health = GetComponent<EnemyDeath>().health;
    }

    void Update()
    {
        if(rb.velocity == Vector3.zero)
        {
            anim.Play("idle", -1, 0f);
        }
        else if(shoot)
        {
            anim.Play("shooting", -1, 0f);
        }
        else if(health <= 0)
        {
            anim.SetInteger("health", health);
        }
        else if(rb.velocity != Vector3.zero)
        {
            anim.Play("walking", -1, 0f);
        }
    }
}
