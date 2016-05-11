using UnityEngine;
using System.Collections;

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

/*
working with player 1
    public class EnemyType1 : MonoBehaviour
{
#region Agent info

    private Vector3 startLoc, targetLoc;
    private Rigidbody rb;
    private Transform tf;
    public GameObject player;
    public NavMeshAgent nav;
    public float maxRange;
    public float moveSpeed;
    private float minDist;

#endregion

    //movement finite state machine
    enum ControlState {STAND, WALK, RETURN };
    private ControlState movementState;

    // Use this for initialization
    void Start ()
    {
        rb = GetComponent<Rigidbody>();
        tf = GetComponent<Transform>();
        nav = GetComponent<NavMeshAgent>();
        movementState = ControlState.STAND;
        tf.forward = Vector3.zero;
        startLoc = tf.position;
        minDist = float.MaxValue;

        if (players == null)
            players = GameObject.FindGameObjectsWithTag("Player");
    }
	
	// Update is called once per frame
	void Update ()
    {
        //TODO: make logic to see which player is closer, chase closer player
        MovementBehavior();
    }
    void MovementBehavior()
    {
        switch(movementState)
        {
            case ControlState.STAND:
                Stand();               
                break;

            case ControlState.WALK:
                Walk();
                break;

            case ControlState.RETURN:
                Return();
                break;
        }
    }

    void Stand()
    {
    float dist = Vector3.Distance(startLoc, player.transform.position);
    if (dist < maxRange)
            {
               movementState = ControlState.WALK;
            }
            if (Vector3.Distance(tf.position, startLoc) > 1.0f)
                movementState = ControlState.RETURN;
    }


    void Walk()
    {
        //nav.destination = player.transform.position;
        tf.LookAt(player.transform);
        tf.position += tf.forward * moveSpeed * Time.deltaTime;
        if (Vector3.Distance(startLoc, player.transform.position) > maxRange)
        {
            movementState = ControlState.STAND;
        }
    }

    void Return()
    {
        //nav.destination = startLoc;
        tf.LookAt(startLoc);
        tf.position += tf.forward * moveSpeed * Time.deltaTime;
        if (Vector3.Distance(startLoc, tf.position) < 0.25f)
        {
            movementState = ControlState.STAND;
        }
        if (Vector3.Distance(startLoc, player.transform.position) < maxRange)
        {
            movementState = ControlState.WALK;
        }
    }

}

*/
