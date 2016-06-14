using UnityEngine;
using System.Collections;

[RequireComponent (typeof(EnemyDeath))]
public class EnemyHealth : MonoBehaviour
{
    public int health;
    public int lastHitBy;
    public int scoreValue;
    public ScoreManager score;

    void Start()
    {
        lastHitBy = -1;
        score = FindObjectOfType<ScoreManager>();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 9)
        {
            health -= collision.gameObject.GetComponent<Bullet>().damage;
            lastHitBy = collision.gameObject.GetComponent<Bullet>().owner;
        }
    }
}
