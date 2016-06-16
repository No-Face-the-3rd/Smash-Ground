using UnityEngine;
using System.Collections;

public class ShootClosestPlayer : MonoBehaviour
{
    #region Agent Info

    private Transform tf;
    public PlayerLocator playerLocator;
    private AimAt aim;
    public GameObject attackPre;
    public Vector3 attackYOffset;
    public float attackSpawnOffset;
    public float maxRadiusForAim;
    public float fireDelay;
    private float originalTimer;
    public float attackRange; // little bigger than actual attack distance, Minimum range is 2.5
    private Vector3 directionToPlayer;
    public bool shoot;

    #endregion

    void Start ()
    {
        tf = GetComponent<Transform>();
        originalTimer = fireDelay;
        playerLocator = GameObject.FindObjectOfType<PlayerLocator>();

        aim = GetComponent<AimAt>();
        attackSpawnOffset = attackPre.GetComponent<Bullet>().spawnOffsetLength;
        attackYOffset = attackPre.GetComponent<Bullet>().spawnOffsetHeight;
        shoot = false;
    }

	void Update ()
    {
        if (gameObject.tag != "Inactive")   //if i can shoot
        {
            shoot = false;
            ShootBehavior();
        }
	}

    void ShootBehavior()
    {
        float minDist = float.MaxValue; //used to see which player is closer
        int target = -1;                //start out without a target

        for (int i = 0; i < playerLocator.targetable.Length; ++i)
        {
            directionToPlayer = (playerLocator.targetable[i].transform.position - tf.position); //find the direction to the current player
            float tmpDist = directionToPlayer.magnitude;                    ////distance between me and the player

            if (tmpDist < maxRadiusForAim)//if within distance of radius
            {
                if (tmpDist < minDist) //if the new player position is closer, set minimum distance to tmpDist && set target to i
                {
                    minDist = tmpDist;
                    target = i;
                }
            }
        }

        MoveToStartLoc guard = GetComponent<MoveToStartLoc>();  //used for fixing previous bugs where the behaviors were additive
        Wander wandBehavior = GetComponent<Wander>();

        if (target >= 0)    //if I have a target
        {
            if (guard != null)
            {
                guard.enabled = false;
            }
            if (wandBehavior != null)
            {
                wandBehavior.enabled = false;
            }

            Aim(target);
        }
        else
        {
            //if not go back to whatever I was doing
            if (guard != null)
            {
                guard.enabled = true;
            }
            if (wandBehavior != null)
            {
                wandBehavior.enabled = true;
            }
        }
    }

    void Aim(int _target)   //organization
    {
        aim.aimAtLoc = playerLocator.targetable[_target].transform.position;    //aim at the current player's position
        tf.forward = new Vector3(tf.forward.x, 0, tf.forward.z);    //0 out the Y so they don't tilt
        fireDelay -= Time.deltaTime;            
        if (fireDelay <= 0 && directionToPlayer.magnitude <= attackRange)   //if I can shoot
        {
            fireDelay = originalTimer;  //reset timer
            shoot = true;   //shoot to true
        }
    }

    void Fire() //animation event
    {
        GameObject tmp = (GameObject)Instantiate(attackPre, tf.position + tf.forward * attackSpawnOffset + attackYOffset,
                    Quaternion.LookRotation(tf.forward));
        tmp.layer = 11;
        tmp.GetComponent<Bullet>().owner = -1;
        //spawning attack prefab/bullet
    }
}
