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

    #endregion

    void Start ()
    {
        tf = GetComponent<Transform>();
        originalTimer = fireDelay;
        playerLocator = GameObject.FindObjectOfType<PlayerLocator>();

        aim = GetComponent<AimAt>();
        attackSpawnOffset = attackPre.GetComponent<Bullet>().spawnOffsetLength;
        attackYOffset = attackPre.GetComponent<Bullet>().spawnOffsetHeight;
    }

	void Update ()
    {
        ShootBehavior();
	}

    void ShootBehavior()
    {
        float minDist = float.MaxValue;
        int target = -1;


        for (int i = 0; i < playerLocator.players.Length; ++i)
        {
            directionToPlayer = (playerLocator.players[i].transform.position - tf.position);
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

        MoveToStartLoc guard = GetComponent<MoveToStartLoc>();

        if (target >= 0)
        {
            if (guard != null)
            {
                guard.enabled = false;
            }
            Aim(target);
        }
        else
            guard.enabled = true;
    }

    void Aim(int _target)
    {
        aim.aimAtLoc = playerLocator.players[_target].transform.position;
        tf.forward = new Vector3(tf.forward.x, 0, tf.forward.z);
        fireDelay -= Time.deltaTime;
        if (fireDelay <= 0 && directionToPlayer.magnitude <= attackRange)
        {
            fireDelay = originalTimer;
            GameObject tmp = (GameObject)Instantiate(attackPre, tf.position + tf.forward * attackSpawnOffset + attackYOffset,
                    Quaternion.LookRotation(tf.forward));
            tmp.layer = 11;
            tmp.GetComponent<Bullet>().owner = -1;
        }
    }
}
