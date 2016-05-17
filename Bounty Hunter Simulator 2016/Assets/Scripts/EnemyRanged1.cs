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
    private float fireDelay;

    #endregion

    //movement finite state machine
    enum ControlState { WAIT, RETURN};
    private ControlState movementState;

    enum ShootState { STILL, AIM, FIRE };
    private ShootState fireState;

    // Use this for initialization
    void Start ()
    {
        tf = GetComponent<Transform>();
        startLoc = tf.position;
        fireDelay = 0;
        movementState = ControlState.WAIT;
        fireState = ShootState.STILL;
	}
	
	// Update is called once per frame
	void Update ()
    {
        MovementBehavior();
	}

    void MovementBehavior()
    {
        float minDist = float.MaxValue;
        players = GameObject.FindGameObjectsWithTag("Player"); // get active players
        int target = -1;

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
            case ControlState.WAIT:
                Wait(target);
                break;

            case ControlState.RETURN:
                Return(target);
                break;
        }

        switch (fireState)
        {
            case ShootState.STILL:
                Still(target);
                break;

            case ShootState.AIM:
                Aim(target);
                break;

            case ShootState.FIRE:
                Fire(target);
                break;
        }


    }

    void Wait(int id)
    {
        if (Vector3.Distance(startLoc, tf.position) > .25f)
        {
            movementState = ControlState.RETURN;
        }
    }

    void Return(int id)
    {
        tf.LookAt(startLoc);
        tf.position += tf.forward * moveSpeed * Time.deltaTime;
        if (Vector3.Distance(startLoc, tf.position) < .25f)
        {
            movementState = ControlState.WAIT;
        }
    }

    void Aim(int id)
    {
        if(id >= 0)
        {
            tf.LookAt(players[id].transform);
            fireState = ShootState.FIRE;
        }
    }
    //Instantiate(gameobjectprefab,global spawn position, global forward)
    void Fire(int id)
    {
        fireDelay += Time.deltaTime;
        if(id >= 0 && fireDelay >= 5)
        {
            fireDelay = 0;
            GameObject tmp = (GameObject)Instantiate(bulletPre, tf.position + tf.forward * 0.5f + new Vector3(0.0f, 1.0f, 0.0f), Quaternion.Euler(tf.forward));
            tmp.layer = 11;
            fireState = ShootState.STILL;
        }
        
    }

    void Still(int id)
    {
        if (id >= 0)
        {
            fireState = ShootState.AIM;
        }
    }
}
