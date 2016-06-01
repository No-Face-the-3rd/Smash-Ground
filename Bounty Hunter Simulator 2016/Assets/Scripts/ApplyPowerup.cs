using UnityEngine;
using System.Collections;

public class ApplyPowerup : MonoBehaviour {

    public PlayerController.powerUps power;
    public float powerTime;

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.layer == 8)
        {
            collision.gameObject.GetComponent<PlayerController>().powerup = power;
            collision.gameObject.GetComponent<PlayerController>().powerupTime = powerTime;
            Destroy(gameObject);
        }
    }
    
}
