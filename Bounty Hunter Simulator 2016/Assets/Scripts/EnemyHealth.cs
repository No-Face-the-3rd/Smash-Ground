using UnityEngine;
using System.Collections;

[RequireComponent (typeof(EnemyDeath))]
public class EnemyHealth : MonoBehaviour
{
    public int health;
    public int lastHitBy;
    public int scoreValue;
    public ScoreManager score;
    public AudioSource audioSource;
    public AudioClip enemyHit;

    void Start()
    {
        lastHitBy = -1;
        score = FindObjectOfType<ScoreManager>();
        audioSource = GetComponent<AudioSource>();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 9) //if collides with anything on the player bullet layer, subtract from health/update last hit by
        {
            audioSource.timeSamples = 0;
            audioSource.PlayOneShot(enemyHit);
            health -= collision.gameObject.GetComponent<Bullet>().damage;
            lastHitBy = collision.gameObject.GetComponent<Bullet>().owner;
        }
    }
}
