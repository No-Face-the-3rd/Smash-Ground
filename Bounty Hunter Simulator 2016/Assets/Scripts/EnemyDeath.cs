using UnityEngine;
using System.Collections;

public class EnemyDeath : MonoBehaviour
{
    private EnemyHealth enemyHealth;
    public float powerupMaxRange;
    public float powerupSpawnThres;
    private PowerupDB powersDB;

    void Start()
    {
        powersDB = FindObjectOfType<PowerupDB>();
        enemyHealth = GetComponent<EnemyHealth>();
    }
    public void DoDestroy()
    {
        this.gameObject.tag = "Inactive";

        if (enemyHealth != null && enemyHealth.lastHitBy >= 0)
        {
            enemyHealth.score.addScore(enemyHealth.lastHitBy, enemyHealth.scoreValue);
            if(Random.Range(0.0f, powerupMaxRange) < powerupSpawnThres)
            {
                int ind = Random.Range(0, powersDB.powerups.Length);
                Instantiate(powersDB.powerups[ind], this.transform.position + new Vector3(0, 1, 0), powersDB.powerups[ind].transform.rotation);
            }
        }
        Destroy(this.gameObject);
    }
}
