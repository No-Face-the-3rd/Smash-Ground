using UnityEngine;
using System.Collections;

public class EnemyDeath : MonoBehaviour
{
    public int health;
    public int lastHitBy;
    public int scoreValue;
    private ScoreManager score;
    private Animator anim;

	void Start ()
    {
        anim = GetComponent<Animator>();
        lastHitBy = -1;
        score = FindObjectOfType<ScoreManager>();
	}
	
	void Update ()
    {
        if (health <= 0)
        {
           // score.addScore(lastHitBy, scoreValue);
            //Instantiate(deathanim)
//            anim.
//            Destroy(this.gameObject);
            if(this.anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 10.0f)
            {
                //Destroy(this.gameObject);
            }
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

    public void DoDestroy()
    {
        Destroy(this.gameObject);
    }
}
