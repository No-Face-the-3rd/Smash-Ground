using UnityEngine;
using System.Collections;

public class EnemyDeath : MonoBehaviour
{
    private EnemyHealth enemyHealth;
    public float powerupMaxRange;
    public float powerupSpawnThres;
    public GameObject deathParticle;
    public int [] powerupsICanSpawn;
    public AudioSource audioSource;
    public AudioClip deathFall;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        enemyHealth = GetComponent<EnemyHealth>();
    }
    public void DoDestroy() //used whenever an Enemy needs to die
    {
        if (enemyHealth != null && enemyHealth.lastHitBy >= 0)
        {
           ScoreManager.scorer.addScore(enemyHealth.lastHitBy, enemyHealth.scoreValue);
            if(Random.Range(0.0f, powerupMaxRange) < powerupSpawnThres)
            {
                if (powerupsICanSpawn.Length > 0)       //If i have a different powerup spawning than default
                {

                    int ind = Random.Range(0, powerupsICanSpawn.Length);    //get a random number for chooseing which powerup

                    if (powerupsICanSpawn[ind] < PowerupDB.powerUpData.getNumPowerUps())  //Used to catch if the number is valid to spawn the powerup
                    {
                        GameObject power = PowerupDB.powerUpData.getPowerUp(ind);
                        spawnPowerUp(power); //spawn the powerup

                    }
                    else
                    {
                        Debug.LogError("Chosen Index Exceeds Array Size");
                    }
                }
                else
                {
                    int ind = Random.Range(0, PowerupDB.powerUpData.getNumPowerUps());    //use the default spawning, choose a powerup randomly
                    GameObject power = PowerupDB.powerUpData.getPowerUp(ind);
                    spawnPowerUp(power); //spawn it
                }
            }
        }
        Instantiate(deathParticle, transform.position + new Vector3(0.0f,1.0f,0.0f), Quaternion.Euler(Vector3.zero));
        Destroy(this.gameObject);
    }

    public void SetInactive()   //used for animation event
    {
        PlayClipForTimer();
        this.gameObject.tag = "Inactive";
    }

    public void PlayClipForTimer()
    {
        audioSource.timeSamples = 0;
        audioSource.PlayOneShot(deathFall);
    }

    private void spawnPowerUp(GameObject power)
    {
        Instantiate(power, this.transform.position + new Vector3(0, 1, 0), power.transform.rotation);
    }
}
