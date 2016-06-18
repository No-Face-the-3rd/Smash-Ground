using UnityEngine;
using System.Collections;


[RequireComponent(typeof(Wander))]
[RequireComponent (typeof(PursuePlayer))]
public class BossStateMachine : MonoBehaviour
{
    private EnemyHealth health;
    private Rigidbody rb;
    private PursuePlayer pursue;
    private Wander wander;
    private BossShoot shoot;
    private enum STATE { BEGINNING, MIDDLE, END};
    private STATE currentState;
    public float regenTimer;
    private float originalTimer;
    public float beginningTurnSpeed;
    public float middleTurnSpeed;
    public float endTurnSpeed;

    void Start ()
    {
        health = GetComponent<EnemyHealth>();
        rb = GetComponent<Rigidbody>();
        shoot = GetComponent<BossShoot>();
        pursue = GetComponent<PursuePlayer>();
        wander = GetComponent<Wander>();
        currentState = STATE.BEGINNING;
        originalTimer = regenTimer;
        pursue.enabled = wander.enabled = false;
    }

	void Update ()
    {   
        //state machine
        switch(currentState)
        {
            case STATE.BEGINNING:
                Beginning();
                break;

            case STATE.MIDDLE:
                Middle();
                break;

            case STATE.END:
                End();
                break;
        }


    }

    void Beginning()
    {
        shoot.turnSpeed = beginningTurnSpeed;
        rb.velocity = Vector3.zero;     //can't move
        if(health.health < 17)  //if true, switch state
        {
            currentState = STATE.MIDDLE;
        }
    }

    void Middle()
    {
        shoot.turnSpeed = middleTurnSpeed; 
        regenTimer = originalTimer; // so it doesn't instantly heal when it goes back to END state
        wander.enabled = true;  //wanders around

        if (health.health > 17) //if health somehow goes up
        {
            currentState = STATE.BEGINNING;
            wander.enabled = false;
        }
        if (health.health < 12) //if health lowers past 12
        {
            currentState = STATE.END;
            wander.enabled = false;
        }

    }

    void End()
    {
        shoot.turnSpeed = endTurnSpeed;
        wander.enabled = false;
        pursue.enabled = true;
        if(health.health > 11)
        {
            currentState = STATE.MIDDLE;
            pursue.enabled = false;
        }
        regenTimer -= Time.deltaTime; //regen

        if(regenTimer < 0)
        {
            regenTimer = originalTimer;
            health.health += 1;
        }
        
    }
}
