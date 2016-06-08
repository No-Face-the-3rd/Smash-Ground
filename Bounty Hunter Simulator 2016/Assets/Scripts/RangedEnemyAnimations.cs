using UnityEngine;
using System.Collections;

public class RangedEnemyAnimations : MonoBehaviour {

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

    void Update()
    {
        Debug.Log(rb.velocity);
        anim.SetFloat("magOfVelocity", Vector3.Magnitude(rb.velocity));
        anim.SetBool("shoot", shoot.shoot);
        if (health != null)
        {
            anim.SetInteger("health", health.health);
        }
    }
}
