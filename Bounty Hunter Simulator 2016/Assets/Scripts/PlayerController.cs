using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{

    public int playerNum;

    public float speed;

    private float horizMove, horizFace, vertMove, vertFace;
    private Rigidbody rb;
    private Transform tf;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        tf = GetComponent<Transform>();
        if (playerNum == 0)
            playerNum = 1;
    }

    void Update()
    {
        horizMove = Input.GetAxis("moveHoriz_P" + playerNum);
        horizFace = Input.GetAxis("faceHoriz_P" + playerNum);
        vertMove = Input.GetAxis("moveVert_P" + playerNum);
        vertFace = Input.GetAxis("faceVert_P" + playerNum);
    }

    void FixedUpdate()
    {
        tf.forward = new Vector3(horizFace, 0.0f, vertFace);
        rb.velocity = new Vector3(horizMove, 0.0f, vertMove) * speed;
    }

    public void setNum(int id)
    {
        playerNum = id;
    }
}
