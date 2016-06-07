using UnityEngine;
using System.Collections;

public class RangedEnemyAnimations : MonoBehaviour {

    private Rigidbody rb;
    private Animator anim;
    private ShootClosestPlayer shoot;
    private EnemyDeath health;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        shoot = GetComponent<ShootClosestPlayer>();
        health = GetComponent<EnemyDeath>();
    }

    void Update()
    {
        Debug.Log(rb.velocity);
        anim.SetFloat("magOfVelocity", Vector3.Magnitude(rb.velocity));
        anim.SetBool("shoot", shoot.shoot);
        anim.SetInteger("health", health.health);
        //if (Vector3.Magnitude(rb.velocity) < float.Epsilon)
        //{
        //    anim.Play("idle", -1, 0f);
        //}
        //else if (shoot)
        //{
        //    anim.Play("shooting", -1, 0f);
        //}
        //else if (health <= 0)
        //{
        //    anim.SetInteger("health", health);
        //}
        //else if (Vector3.Magnitude(rb.velocity) > float.Epsilon)
        //{
        //    anim.Play("walking", -1, 0f);
        //}
    }
}
