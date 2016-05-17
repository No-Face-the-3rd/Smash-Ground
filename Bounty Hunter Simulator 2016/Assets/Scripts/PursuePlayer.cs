using UnityEngine;
using System.Collections;

public class PursuePlayer : MonoBehaviour
{
    private Vector3 targetDirection;
    private Transform tf;
    public GameObject[] players;
    public float maxRange;
    public float moveSpeed;

    // Use this for initialization
    void Start ()
    {
        tf = GetComponent<Transform>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        players = GameObject.FindGameObjectsWithTag("Player"); //get the active players
        float minDist = float.MaxValue;
        int target = -1;
        for (int i = 0; i < players.Length; ++i)
        {
            float startRad = Vector3.Distance(tf.position, players[i].transform.position);
            float tmpDist = Vector3.Distance(tf.position, players[i].transform.position);
            if (startRad < maxRange) //if within distance of radius, range too large
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
        if (_target >= 0)
        {
            tf.LookAt(players[_target].transform);
            tf.position += tf.forward * moveSpeed * Time.deltaTime;
        }
    }
}

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
