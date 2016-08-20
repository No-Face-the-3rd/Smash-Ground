using UnityEngine;
using System.Collections;

public class BossShoot : MonoBehaviour
{

    #region Agent Info

    private Transform tf;
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
    public GameObject planeToUse;

    #endregion

    void Start()
    {
        tf = GetComponent<Transform>();
        originalTimer = fireDelay;
        shoot = false;
        
    }

    void Update()
    {
        if (gameObject.tag != "Inactive")   //if I can shoot
        {
            shoot = false;
            ShootBehavior();
        }
    }

    void ShootBehavior()
    {
        float minDist = float.MaxValue;
        int target = -1;

        for (int i = 0; i < PlayerLocator.locator.targetable.Length; ++i)
        {
            directionToPlayer = (PlayerLocator.locator.targetable[i].transform.position - tf.position);
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

        if (target >= 0)
        {
            CheckTurnLeft();
            if(turn == false)   //if I am looking at the player
            {
                Shoot();
            }
        }
        else
        {
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

    void DoFire() //animation event
    {
        int rand = Random.Range(0, attackPre.Length); //chooses a random prefab
        if (attackPre[rand].GetComponent<Bullet>() != null) //if its a bullet
        {
            float bulletSpawnOffset = attackPre[rand].GetComponent<Bullet>().spawnOffsetLength;
            Vector3 bulletYOffset = attackPre[rand].GetComponent<Bullet>().spawnOffsetHeight;
            GameObject tmp = (GameObject)Instantiate(attackPre[rand], tf.position + tf.forward * bulletSpawnOffset + bulletYOffset,
                    Quaternion.LookRotation(tf.forward));
            tmp.layer = 11;
            tmp.GetComponent<Bullet>().owner = -1;
            //spawn it
        }
        else
        {
            //if it's not a bullet
            GameObject tmp = (GameObject)Instantiate(attackPre[rand], tf.position + tf.forward * attackSpawnOffset + attackYOffset,
                    Quaternion.LookRotation(tf.forward));
            tmp.layer = 10; //set to enemies layer
            tmp.gameObject.tag = "Spawning";    //set tag to spawning
            tmp.GetComponent<driveToTarget>().targetLoc = new Vector3(Random.Range(planeToUse.gameObject.transform.position.x, planeToUse.gameObject.transform.position.x + 5),
                0.0f,
                Random.Range(planeToUse.gameObject.transform.position.z, planeToUse.gameObject.transform.position.z + 5));
                //drive to a random point next to the center of the room
        }
    }

    void CheckTurnLeft()
    {
        tf.forward = new Vector3(tf.forward.x, 0, tf.forward.z);
        directionToPlayer = new Vector3(directionToPlayer.x, 0.0f, directionToPlayer.z); //zero out for calculations
        float angle = (Mathf.Atan2(directionToPlayer.z, directionToPlayer.x) - Mathf.Atan2(tf.forward.z, tf.forward.x)) * Mathf.Rad2Deg; //angle from direction to my forward, converted to degrees
        if(angle < 0.0f)
        {
            //fixed turning bug
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

    void OnCollisionEnter(Collision collision) //used for enemy spawning in any room
    {
        if (collision.gameObject.layer == 0)
        {
            if(planeToUse == null)
            {
                planeToUse = collision.gameObject.transform.parent.FindChild("env_floor").gameObject;
            }
        }

    }
}
