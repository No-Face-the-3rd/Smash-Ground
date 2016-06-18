using UnityEngine;
using System.Collections;

public class ApplyPowerup : MonoBehaviour {

    public PlayerController.powerUps power;
    public float powerTime;
    private AudioSource audio;

    void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.layer == 8)
        {
            collision.gameObject.GetComponent<PlayerController>().powerup = power;
            collision.gameObject.GetComponent<PlayerController>().powerupTime = powerTime;
            audio.pitch = Random.Range(0.1f, 3.0f);
            audio.Play();
            GetComponent<Collider>().enabled = false;
            GetComponent<MeshRenderer>().enabled = false;
            Destroy(gameObject,audio.clip.length);
        }
    }
    
}
