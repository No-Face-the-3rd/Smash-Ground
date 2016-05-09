using UnityEngine;
using System.Collections;

public class EnemyType1 : MonoBehaviour
{
    //movement finite state machine
    enum ControlState {STAND, WALK };
    private ControlState movementState;

    //

    Vector3 startLoc;
    private Rigidbody rb;
    private Transform tf;
	// Use this for initialization
	void Start ()
    {
        rb = GetComponent<Rigidbody>();
        tf = GetComponent<Transform>();
        movementState = ControlState.STAND;
        tf.forward = Vector3.zero;
    }
	
	// Update is called once per frame
	void Update ()
    {
	    
	}
    void MovementBehavior()
    {

        switch(movementState)
        {
            case ControlState.STAND:
                break;

            case ControlState.WALK:
                break;
        }
    }
}

