using UnityEngine;
using System.Collections;

public class EnemyDeath : MonoBehaviour
{
    public int health;
    public int lastHitBy;
    public int scoreValue;

	void Start ()
    {
        lastHitBy = -1;
	}
	
	void Update ()
    {
        if (health <= 0)
        {
            //Instantiate(deathanim)
            //GameObject gamemanager = findobjectoftype<gamemanager>().gameobject;
            //gamemanager.scores[lasthitby] += scoreValue;
            Destroy(this.gameObject);
        }

    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.layer == 9)
        {
            health -= collision.gameObject.GetComponent<Bullet>().damage;
            lastHitBy = collision.gameObject.GetComponent<Bullet>().owner;
        }
    }
}
