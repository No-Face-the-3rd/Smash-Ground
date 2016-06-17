using UnityEngine;
using System.Collections;

public class MeleeEnemyAnimations : EnemySkin
{
    private Rigidbody rb;
    private Animator anim;
    private ShootClosestPlayer attack;
    private EnemyHealth health;
    private DestroyAfterTimer timer;

    void Awake ()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        attack = GetComponent<ShootClosestPlayer>();
        health = GetComponent<EnemyHealth>();
        timer = GetComponent<DestroyAfterTimer>();
    }


    public override void Update()
    {
        base.Update(); // sets color of the base material to whatever color I select on the inspector
        
        anim.SetFloat("magOfVelocity", Vector3.Magnitude(rb.velocity)); //sets animation parameters
        anim.SetBool("attack", attack.shoot);
        if (health != null)
        {
            anim.SetInteger("health", health.health);
        }
        if (timer != null)
        {
            anim.SetFloat("timer", timer.timer);
        }
    }
}
