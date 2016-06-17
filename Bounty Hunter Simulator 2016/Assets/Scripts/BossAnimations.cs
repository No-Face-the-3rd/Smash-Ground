using UnityEngine;
using System.Collections;

public class BossAnimations : MonoBehaviour
{
    private Rigidbody rb;
    private Animator anim;
    private BossShoot attack;
    private EnemyHealth health;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        attack = GetComponent<BossShoot>();
        health = GetComponent<EnemyHealth>();
    }

    void Update()
    {
        //set stuff
        anim.SetFloat("magOfVelocity", Vector3.Magnitude(rb.velocity));
        anim.SetBool("TurnLeft", attack.turnLeft);
        anim.SetBool("turn", attack.turn);
        anim.SetInteger("health", health.health);
        anim.SetBool("Shoot", attack.shoot);
    }
}
