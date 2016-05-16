using UnityEngine;
using System.Collections;


public class EnemyRanged1 : MonoBehaviour
{
    #region Agent Info

    private Vector3 startLoc;
    private Transform tf;
    private GameObject[] players;
    public GameObject bulletPre;
    public float maxRange;
    public float moveSpeed;

    #endregion

    //movement finite state machine
    enum ControlState { RETURN, AIM, FIRE};
    private ControlState movementState;

    // Use this for initialization
    void Start ()
    {
        tf = GetComponent<Transform>();
        startLoc = tf.position;
	}
	
	// Update is called once per frame
	void Update ()
    {
        MovementBehavior();
	}

    void MovementBehavior()
    {
        players = GameObject.FindGameObjectsWithTag("Player"); // get active players
        int target = 0;



        switch(movementState)
        {
            case ControlState.AIM:
                Aim(target);
                break;

            case ControlState.FIRE:
                Fire(target);
                break;

            case ControlState.RETURN:
                Return(target);
                break;
        }


    }

    void Aim(int id)
    {

    }

    void Fire(int id)
    {

    }

    void Return(int id)
    {

    }
}
