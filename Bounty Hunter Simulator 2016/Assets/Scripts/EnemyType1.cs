using UnityEngine;
using System.Collections;

public class EnemyType1 : MonoBehaviour
{
    //movement finite state machine
    enum ControlState {STAND, WALK, RETURN };
    private ControlState movementState;

    //Agent info
    private Vector3 startLoc, targetLoc;
    private Rigidbody rb;
    private Transform tf;
    public GameObject[] players;
    public NavMeshAgent nav;
    public float maxRange;
    public float moveSpeed;

	// Use this for initialization
	void Start ()
    {
        rb = GetComponent<Rigidbody>();
        tf = GetComponent<Transform>();
        nav = GetComponent<NavMeshAgent>();
        movementState = ControlState.STAND;
        tf.forward = Vector3.zero;
        startLoc = tf.position;

        if (players == null)
            players = GameObject.FindGameObjectsWithTag("Player");
    }
	
	// Update is called once per frame
	void Update ()
    {
        //TODO: make logic to see which player is closer, chase closer player
        if(players != null)
        {
            MovementBehavior();
        }
    }
    void MovementBehavior()
    {
        switch(movementState)
        {
            case ControlState.STAND:
                if (Vector3.Distance(startLoc, players[0].transform.position) < maxRange)
                {
                    movementState = ControlState.WALK;
                }
                if (Vector3.Distance(tf.position, startLoc) > 1.0f)
                    movementState = ControlState.RETURN;
                break;

            case ControlState.WALK:
                //nav.destination = player.transform.position;
                tf.LookAt(players[0].transform);
                tf.position += tf.forward * moveSpeed * Time.deltaTime;
                if(Vector3.Distance(startLoc, players[0].transform.position) > maxRange)
                {
                    movementState = ControlState.STAND;
                }
                break;

            case ControlState.RETURN:
                //nav.destination = startLoc;
                tf.LookAt(startLoc);
                tf.position += tf.forward * moveSpeed * Time.deltaTime;
                if(Vector3.Distance(startLoc, tf.position) < 0.25f)
                {
                    movementState = ControlState.STAND;
                }
                if (Vector3.Distance(startLoc, players[0].transform.position) < maxRange)
                {
                    movementState = ControlState.WALK;
                }
                break;
        }
    }
}
