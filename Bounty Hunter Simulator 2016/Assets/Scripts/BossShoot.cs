using UnityEngine;
using System.Collections;

public class BossShoot : MonoBehaviour
{

    #region Agent Info

    private Transform tf;
    public PlayerLocator playerLocator;
    public GameObject [] attackPre;
    public Vector3 attackYOffset;
    public float attackSpawnOffset;
    public float maxRadiusForAim;
    public float fireDelay;
    private float originalTimer;
    public float attackRange; // little bigger than actual attack distance, Minimum range is 2.5
    private Vector3 directionToPlayer;
    public float turnSpeed;
    public bool shoot;
    public bool turnLeft;
    public bool turn;
    public float bossTurnFOV;

    #endregion

    void Start()
    {
        tf = GetComponent<Transform>();
        originalTimer = fireDelay;
        playerLocator = GameObject.FindObjectOfType<PlayerLocator>();
        shoot = false;
        
    }

    void Update()
    {
        if (gameObject.tag != "Inactive")
        {
            shoot = false;
            ShootBehavior();
        }
    }

    void ShootBehavior()
    {
        float minDist = float.MaxValue;
        int target = -1;

        for (int i = 0; i < playerLocator.targetable.Length; ++i)
        {
            directionToPlayer = (playerLocator.targetable[i].transform.position - tf.position);
            float tmpDist = directionToPlayer.magnitude;

            if (tmpDist < maxRadiusForAim)
            {
                if (tmpDist < minDist)
                {
                    minDist = tmpDist;
                    target = i;
                }
            }
        }

//        Wander wandBehavior = GetComponent<Wander>();

        if (target >= 0)
        {
            //if (wandBehavior != null)
            //{
            //    wandBehavior.enabled = false;
            //}
            CheckTurnLeft();
            if(turn == false)
            {
                Shoot();
            }
        }
        else
        {
            //if (wandBehavior != null)
            //{
            //    wandBehavior.enabled = true;
            //}
            turn = false;
        }
    }

    void Shoot()
    {
        fireDelay -= Time.deltaTime;
        if (fireDelay <= 0 && directionToPlayer.magnitude <= attackRange)
        {
            fireDelay = originalTimer;
            shoot = true;              
        }
    }

    void DoFire()
    {
        int rand = Random.Range(0, attackPre.Length);
        if (attackPre[rand].GetComponent<Bullet>() != null)
        {
            float bulletSpawnOffset = attackPre[rand].GetComponent<Bullet>().spawnOffsetLength;
            Vector3 bulletYOffset = attackPre[rand].GetComponent<Bullet>().spawnOffsetHeight;
            GameObject tmp = (GameObject)Instantiate(attackPre[rand], tf.position + tf.forward * bulletSpawnOffset + bulletYOffset,
                    Quaternion.LookRotation(tf.forward));
            tmp.layer = 11;
            tmp.GetComponent<Bullet>().owner = -1;
        }
        else
        {
            GameObject tmp = (GameObject)Instantiate(attackPre[rand], tf.position + tf.forward * attackSpawnOffset + attackYOffset,
                    Quaternion.LookRotation(tf.forward));
            tmp.layer = 10;
            tmp.gameObject.tag = "Spawning";
//            tmp.GetComponent<driveToTarget>().targetLoc =
            /*
                set tag to spawning
                set layer to enemies
                set drive to target to a random point on the plane
            */
        }
    }

    void FixedUpdate()
    {
    }

    void CheckTurnLeft()
    {
        tf.forward = new Vector3(tf.forward.x, 0, tf.forward.z);
        directionToPlayer = new Vector3(directionToPlayer.x, 0.0f, directionToPlayer.z);
        float angle = (Mathf.Atan2(directionToPlayer.z, directionToPlayer.x) - Mathf.Atan2(tf.forward.z, tf.forward.x)) * Mathf.Rad2Deg;
        if(angle < 0.0f)
        {
            angle += 360.0f;
        }
        if (angle > bossTurnFOV * 0.5f && angle < 360.0f - bossTurnFOV * 0.5f)
        {
            turn = true;
            if (angle < 180.0f)
            {
                turnLeft = true;
            }
            else
            {
                turnLeft = false;
            }
            tf.rotation = Quaternion.Euler(tf.rotation.eulerAngles + new Vector3(0, ((turnLeft) ? -1 : 1) * turnSpeed * Time.deltaTime, 0));
        }
        else
        {
            turn = false;
        }
    }
}
