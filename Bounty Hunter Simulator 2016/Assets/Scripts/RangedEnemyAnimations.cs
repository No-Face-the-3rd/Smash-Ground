﻿using UnityEngine;
using System.Collections;

public class RangedEnemyAnimations : EnemySkin
{

    private Rigidbody rb;
    private Animator anim;
    private ShootClosestPlayer shoot;
    private EnemyHealth health;
    private DestroyAfterTimer timer;


    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        shoot = GetComponent<ShootClosestPlayer>();
        health = GetComponent<EnemyHealth>();
        timer = GetComponent<DestroyAfterTimer>();
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
        if(timer != null)
        {
            anim.SetFloat("timer", timer.timer);
        }
    }
}
