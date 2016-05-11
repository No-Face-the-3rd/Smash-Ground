using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{

    public int playerNum;

    public float speed;
    public float panSpeed;

    private float horizMove, horizFace, vertMove, vertFace;
    private Rigidbody rb;
    private Transform tf;
    private Vector3 csTarg;
    public Camera cs;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        tf = GetComponent<Transform>();
        setNum(1);
//            if (playerNum == 0)
//                playerNum = 1;
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
        if (tf.childCount > 0)
            movePlayer();
        else
            moveCam();
    }

    public void setNum(int id)
    {
        playerNum = id;
        createCam();
    }

    void createCam()
    {
        csTarg = new Vector3(0.0f, 3.5f, 4.0f);
        GameObject orig = GameObject.FindGameObjectWithTag("MainCamera");
        cs = ((GameObject)Instantiate(orig, csTarg, Quaternion.Euler(0.0f, 180.0f, 0.0f))).GetComponent<Camera>();
        cs.name = "Player " + playerNum + " Camera";
        cs.tag = "CharSelectCam";
        cs.backgroundColor = Color.gray;
        cs.depth = playerNum;
        cs.cullingMask = 1 << (11 + playerNum);
        cs.orthographic = false;

        cs.pixelRect = new Rect((0.01f + (float)(playerNum - 1) * 0.78f) * cs.pixelRect.width, 0.7f * cs.pixelRect.height, 0.2f * cs.pixelRect.width, 0.26f * cs.pixelRect.height);
        ((AudioListener)cs.GetComponent(typeof(AudioListener))).enabled = false;
        ((GUILayer)cs.GetComponent(typeof(GUILayer))).enabled = false;

        cs.fieldOfView = 20.0f;
        csTarg = new Vector3(0.0f, 3.5f, 0.0f);

    }

    void moveCam()
    {
        Vector3 csPos = cs.GetComponent<Transform>().position;
        float dist = Vector3.Distance(csPos, csTarg);
        cs.GetComponent<Transform>().Rotate(new Vector3(vertMove , -horizMove , 0.0f) * panSpeed);
        cs.GetComponent<Transform>().position = csTarg + -(dist * cs.GetComponent<Transform>().forward);
        cs.GetComponent<Transform>().rotation = Quaternion.Euler(new Vector3(cs.GetComponent<Transform>().rotation.eulerAngles.x, cs.GetComponent<Transform>().rotation.eulerAngles.y, 0.0f));

    }
    void movePlayer()
    {
        //reset cam when playing
        if (cs.GetComponent<Transform>().rotation != Quaternion.Euler(new Vector3(0.0f, 180.0f, 0.0f)))
        {
            Vector3 csPos = cs.GetComponent<Transform>().position;
            float dist = Vector3.Distance(csPos, csTarg);
            cs.GetComponent<Transform>().rotation = Quaternion.Euler(new Vector3(0.0f, 180.0f, 0.0f));
            cs.GetComponent<Transform>().position = csTarg + -(dist * cs.GetComponent<Transform>().forward);
        }
        if (Mathf.Abs(horizFace) > Mathf.Epsilon || Mathf.Abs(vertFace) > Mathf.Epsilon)
            tf.forward = new Vector3(horizFace, 0.0f, vertFace);

        rb.velocity = Vector3.Normalize(new Vector3(horizMove, 0.0f, vertMove)) * speed;
    }
}
