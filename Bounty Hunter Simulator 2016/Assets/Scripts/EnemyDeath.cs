using UnityEngine;
using System.Collections;

public class EnemyDeath : MonoBehaviour
{
    private EnemyHealth enemyHealth;
    public float powerupMaxRange;
    public float powerupSpawnThres;
    private PowerupDB powersDB;
    public GameObject deathParticle;

    void Start()
    {
        powersDB = FindObjectOfType<PowerupDB>();
        enemyHealth = GetComponent<EnemyHealth>();
    }
    public void DoDestroy()
    {
        if (enemyHealth != null && enemyHealth.lastHitBy >= 0)
        {
            enemyHealth.score.addScore(enemyHealth.lastHitBy, enemyHealth.scoreValue);
            if(Random.Range(0.0f, powerupMaxRange) < powerupSpawnThres)
            {
                int ind = Random.Range(0, powersDB.powerups.Length);
                Instantiate(powersDB.powerups[ind], this.transform.position + new Vector3(0, 1, 0), powersDB.powerups[ind].transform.rotation);
            }
        }
        Instantiate(deathParticle, transform.position + new Vector3(0.0f,1.0f,0.0f), Quaternion.Euler(Vector3.zero));
        Destroy(this.gameObject);
    }

    public void SetInactive()
    {
        this.gameObject.tag = "Inactive";
    }
}
