using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class Bullet : MonoBehaviour
{
    public int owner;
    public int damage;
    public float speed;
    public float upForce;
    public float lifetime;
    public Vector3 spawnOffsetHeight;
    public float spawnOffsetLength;
    public Vector3 rotateRate;
    private float curLife;
    private Rigidbody rb;

	void Start ()
    {
        rb = GetComponent<Rigidbody>();
        curLife = 0.0f;

        rb.AddForce(new Vector3(0.0f, upForce, 0.0f), ForceMode.Impulse);
	}

	void Update ()
    {
	   
	}

    void FixedUpdate()
    {
        curLife += Time.deltaTime;
        if(curLife > lifetime)
        {
            Destroy(gameObject);
        }
        float tmp = rb.velocity.y;
        rb.velocity = Vector3.Normalize(new Vector3(transform.forward.x, 0.0f, transform.forward.z)) * speed;
        rb.velocity = new Vector3(rb.velocity.x, tmp, rb.velocity.z);
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + rotateRate);
    }
}
