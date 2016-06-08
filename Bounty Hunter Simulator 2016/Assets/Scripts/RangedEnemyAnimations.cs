using UnityEngine;
using System.Collections;

public class RangedEnemyAnimations : EnemySkin
{

    private Rigidbody rb;
    private Animator anim;
    private ShootClosestPlayer shoot;
    private EnemyHealth health;


    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        shoot = GetComponent<ShootClosestPlayer>();
        health = GetComponent<EnemyHealth>();
    }

    public override void Update()
    {
        base.Update();

        anim.SetFloat("magOfVelocity", Vector3.Magnitude(rb.velocity));
        anim.SetBool("shoot", shoot.shoot);
        if (health != null)
        {
            anim.SetInteger("health", health.health);
        }
    }
}
