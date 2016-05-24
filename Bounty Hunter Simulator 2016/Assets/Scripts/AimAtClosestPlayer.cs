using UnityEngine;
using System.Collections;

public class AimAtClosestPlayer : MonoBehaviour
{
    #region Agent Info

    private Transform tf;
    private GameObject[] players;
    public GameObject bulletPre;
    public Vector3 bulletYOffset;
    public float bulletSpawnOffset;
    public float maxRange;
    public float fireDelay;
    private float originalTimer;

    #endregion

    // Use this for initialization
    void Start ()
    {
        tf = GetComponent<Transform>();
        originalTimer = fireDelay;
    }
	
	// Update is called once per frame
	void Update ()
    {
        ShootBehavior();
	}

    void ShootBehavior()
    {
        float minDist = float.MaxValue;
        players = GameObject.FindGameObjectsWithTag("Player"); // get active players
        int target = -1;

        for (int i = 0; i < players.Length; ++i)
        {
            float startRad = Vector3.Distance(tf.position, players[i].transform.position);
            float tmpDist = Vector3.Distance(tf.position, players[i].transform.position);

            if (startRad < maxRange)
            {
                if (tmpDist < minDist)
                {
                    minDist = tmpDist;
                    target = i;
                }
            }
        }

        if(target >= 0)
        {
            Aim(target);
        }
    }

    void Aim(int _target)
    {
        tf.LookAt(players[_target].transform.position);
        tf.forward = new Vector3(tf.forward.x, 0, tf.forward.z);
        fireDelay -= Time.deltaTime;
        if (fireDelay <= 0)
        {
            fireDelay = originalTimer;
            GameObject tmp = (GameObject)Instantiate(bulletPre, tf.position + tf.forward * bulletSpawnOffset + bulletYOffset,
                    Quaternion.LookRotation(tf.forward));
            tmp.layer = 11;
        }
    }
}