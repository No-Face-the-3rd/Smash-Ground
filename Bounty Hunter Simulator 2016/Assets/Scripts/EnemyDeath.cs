using UnityEngine;
using System.Collections;

public class EnemyDeath : MonoBehaviour
{
    private EnemyHealth enemyHealth;
    public float powerupMaxRange;
    public float powerupSpawnThres;
    private PowerupDB powersDB;
    public GameObject deathParticle;
    public int [] powerupsICanSpawn;

    void Start()
    {
        powersDB = FindObjectOfType<PowerupDB>();
        enemyHealth = GetComponent<EnemyHealth>();
    }
    public void DoDestroy() //used whenever an Enemy needs to die
    {
        if (enemyHealth != null && enemyHealth.lastHitBy >= 0)
        {
            enemyHealth.score.addScore(enemyHealth.lastHitBy, enemyHealth.scoreValue);
            if(Random.Range(0.0f, powerupMaxRange) < powerupSpawnThres)
            {
                if (powerupsICanSpawn.Length > 0)       //If i have a different powerup spawning than default
                {

                    int ind = Random.Range(0, powerupsICanSpawn.Length);    //get a random number for chooseing which powerup

                    if (powerupsICanSpawn[ind] < powersDB.powerups.Length)  //Used to catch if the number is valid to spawn the powerup
                    {
                        Instantiate(powersDB.powerups[powerupsICanSpawn[ind]], this.transform.position + new Vector3(0, 1, 0), powersDB.powerups[ind].transform.rotation);  //spawn the powerup

                    }
                    else
                    {
                        Debug.LogError("Chosen Index Exceeds Array Size");  
                    }   
                }
                else
                {
                    int ind = Random.Range(0, powersDB.powerups.Length);    //use the default spawning, choose a powerup randomly
                    Instantiate(powersDB.powerups[ind], this.transform.position + new Vector3(0, 1, 0), powersDB.powerups[ind].transform.rotation); //spawn it
                }
            }
        }
        Instantiate(deathParticle, transform.position + new Vector3(0.0f,1.0f,0.0f), Quaternion.Euler(Vector3.zero));
        Destroy(this.gameObject);
    }

    public void SetInactive()   //used for animation event
    {
        this.gameObject.tag = "Inactive";
    }
}
