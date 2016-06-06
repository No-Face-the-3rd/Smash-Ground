using UnityEngine;
using System.Collections;

[RequireComponent(typeof(driveToTarget))]
public class PursuePlayer : MonoBehaviour
{
    #region Agent Info

    public Animator anim;
    private AimAt aim;
    private Vector3 directionToPlayer, checkLoc;
    public bool movePursueRadiusWithSelf;
    public PlayerLocator playerLocator;
    public float maxRadiusPursue;
    public float attackRange;
    private driveToTarget pursueLoc;

    #endregion

    void Start ()
    {
        if(!movePursueRadiusWithSelf)
        {
            checkLoc = transform.position;
        }
        pursueLoc = GetComponent<driveToTarget>();
        playerLocator = FindObjectOfType<PlayerLocator>(); //get the active players
        anim = GetComponent<Animator>();
        aim = GetComponent<AimAt>();
	}
	void Update ()
    {
        if(movePursueRadiusWithSelf)
        {
            checkLoc = transform.position;
        }

        float minDist = float.MaxValue;
        int target = -1;

        for (int i = 0; i < playerLocator.targetable.Length; ++i)
        {
            directionToPlayer = playerLocator.targetable[i].transform.position - checkLoc;
            float tmpDist = directionToPlayer.magnitude;
            if (tmpDist < maxRadiusPursue) //if within distance of radius
            {
                if (tmpDist < minDist) //if the new player position is closer, set minimum distance to tmpDist && set target to i
                {
                    minDist = tmpDist;
                    target = i;
                }
            }
        }

        MoveToStartLoc guard = GetComponent<MoveToStartLoc>();
        Wander wandBehavior = GetComponent<Wander>();
        if (target >= 0)
        {
            if (guard != null)
                guard.enabled = false;
            if (wandBehavior != null)
                wandBehavior.enabled = false;
            Pursue(target);
        }
        else
        {
            if (guard != null)
                guard.enabled = true;
            if (wandBehavior != null)
                wandBehavior.enabled = true;
        }
    }
    void Pursue(int _target)
    {
        pursueLoc.targetLoc = playerLocator.targetable[_target].transform.position + directionToPlayer.normalized * attackRange;

        ShootClosestPlayer shoot = GetComponent<ShootClosestPlayer>();
        if (shoot == null)
        {
            aim.aimAtLoc = pursueLoc.targetLoc;
        }
    }
}

//DONE
/*
    public class EnemyMelee2 : MonoBehaviour
{
    #region Agent info
    private Vector3 startLoc, wanderLoc;
    private Transform tf;
    public GameObject[] players;
    public float maxRange;
    public float moveSpeed;
    public float wandTimer;
    private float originalTimer;
    #endregion

    //Movement finite state machine
    enum MoveState {WANDER, PURSUE};
    private MoveState movementState;

    // Use this for initialization
    void Start()
    {
        tf = GetComponent<Transform>();
        movementState = MoveState.WANDER;
        tf.forward = Vector3.zero;
        startLoc = tf.position;
        wanderLoc = startLoc;
        originalTimer = wandTimer;
    }

    void Update()
    {
        MovementBehavior();
    }

    void MovementBehavior()
    {
        float minDist = float.MaxValue;
        int target = -1;
        players = GameObject.FindGameObjectsWithTag("Player");

        for(int i = 0; i < players.Length; ++i)
        {
            float startRad = Vector3.Distance(startLoc, players[i].transform.position);
            float tmpDist = Vector3.Distance(tf.position, players[i].transform.position);

            if(startRad < maxRange)
            {
                if(tmpDist < minDist)
                {
                    minDist = tmpDist;
                    target = i;
                }
            }
        }

        switch(movementState)
        {
            case MoveState.WANDER:
                Wander(target);
                break;

            case MoveState.PURSUE:
                Pursue(target);
                break;
        }
    }

    void Wander(int id)
    {
        wandTimer -= Time.deltaTime;
        tf.LookAt(wanderLoc);
        tf.position += tf.forward * moveSpeed * Time.deltaTime;
        if(Vector3.Distance(wanderLoc, tf.position) < .25f || wandTimer <= 0)
        {
            wandTimer = originalTimer;
            startLoc = wanderLoc;
            wanderLoc.x = startLoc.x + (Random.insideUnitCircle.x * 5);
            wanderLoc.z = startLoc.z + (Random.insideUnitCircle.y * 5);
        }
        if (id >= 0)
            movementState = MoveState.PURSUE;
    }

    void Pursue(int id)
    {
        if (id >= 0)
        {
            tf.LookAt(players[id].transform);
            tf.position += tf.forward * moveSpeed * Time.deltaTime;
        }
        else if (id < 0)
        {
            movementState = MoveState.WANDER;
        }
    }
}


*/
