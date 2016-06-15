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

        Wander wandBehavior = GetComponent<Wander>();

        if (target >= 0)
        {
            if (wandBehavior != null)
            {
                wandBehavior.enabled = false;
            }
            Aim(target);
        }
        else
        {
            if (wandBehavior != null)
            {
                wandBehavior.enabled = true;
            }
        }
    }

    void Aim(int _target)
    {
        fireDelay -= Time.deltaTime;
        if (fireDelay <= 0 && directionToPlayer.magnitude <= attackRange)
        {
            fireDelay = originalTimer;
            shoot = true;
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
                tmp.GetComponent<Bullet>().owner = -1;
                /*
                    set tag to spawning
                    set layer to enemies
                    set drive to target to a random point on the plane
                */
            }
                         
        }
    }

    void FixedUpdate()
    {
        CheckTurnLeft();
    }

    void CheckTurnLeft()
    {
        tf.forward = new Vector3(tf.forward.x, 0, tf.forward.z);
        directionToPlayer = new Vector3(directionToPlayer.x, 0.0f, directionToPlayer.z);
        if(tf.forward != directionToPlayer)
        {
            if (Vector3.Angle(tf.forward, directionToPlayer) < 180.0f)
            {
                turnLeft = true;
            }
            else
            {
                turnLeft = false;
            }
            tf.Rotate(Quaternion.Euler(0, (turnLeft ? 1 : -1) * turnSpeed, 0) * tf.forward);
        }
    }
}
