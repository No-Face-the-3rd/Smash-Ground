using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{

    public int playerNum;

    public float speed;

    private float horizMove, horizFace, vertMove, vertFace;
    private Rigidbody rb;
    private Transform tf;
    public Camera cs;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        tf = GetComponent<Transform>();
        if (playerNum == 0)
            playerNum = 1;

        GameObject orig = GameObject.FindGameObjectWithTag("MainCamera");
        cs = ((GameObject)Instantiate(orig, new Vector3(0.0f,4.0f,4.0f), Quaternion.Euler(0.0f,180.0f,0.0f))).GetComponent<Camera>();
        cs.name = "Player " + playerNum + " Camera";
        cs.tag = "CharSelectCam";
        cs.backgroundColor = Color.black;
        cs.depth = playerNum;
        cs.cullingMask = 1 << 12;
        cs.orthographic = false;

        cs.pixelRect = new Rect(cs.pixelRect.xMin, cs.pixelRect.yMin, cs.pixelRect.width * 0.15f, cs.pixelRect.height * 0.15f);
        ((AudioListener)cs.GetComponent(typeof(AudioListener))).enabled = false;
        ((GUILayer)cs.GetComponent(typeof(GUILayer))).enabled = false;
        RenderTexture texture = new RenderTexture(512, 512, 16);
        cs.targetTexture = texture;
        cs.fieldOfView = 20.0f;


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
        if(Mathf.Abs(horizFace) > Mathf.Epsilon || Mathf.Abs(vertFace) > Mathf.Epsilon)
            tf.forward = new Vector3(horizFace, 0.0f, vertFace);
        rb.velocity = Vector3.Normalize(new Vector3(horizMove, 0.0f, vertMove)) * speed;
    }

    public void setNum(int id)
    {
        playerNum = id;
    }
}
