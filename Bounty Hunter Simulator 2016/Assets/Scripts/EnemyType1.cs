using UnityEngine;
using System.Collections;

public class EnemyType1 : MonoBehaviour
{
#region Agent info

    private Vector3 startLoc, targetLoc;
    private Rigidbody rb;
    private Transform tf;
    public GameObject[] players;
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
        if (players != null)
        {
            MovementBehavior();
        }
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
        for (int i = 0; i < players.Length; ++i)
        {
            float dist = Vector3.Distance(startLoc, players[i].transform.position);
            if (dist < maxRange)
            {
                if(dist < minDist)
                {
                    minDist = dist;
                }
            }
            if (Vector3.Distance(tf.position, startLoc) > 1.0f)
                movementState = ControlState.RETURN;
        }
    }


    void Walk()
    {
        //nav.destination = player.transform.position;
        tf.LookAt(players[0].transform);
        tf.position += tf.forward * moveSpeed * Time.deltaTime;
        if (Vector3.Distance(startLoc, players[0].transform.position) > maxRange)
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
        if (Vector3.Distance(startLoc, players[0].transform.position) < maxRange)
        {
            movementState = ControlState.WALK;
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
