using UnityEngine;
using System.Collections;

public class character : MonoBehaviour
{
    public float primaryDelay, secondaryDelay, dodgeDelay, globalDelay, dodgeSpd, dodgeTime;
    private float nextPrimary, nextSecondary, nextDodge;
    public GameObject primaryPref, secondaryPref;
    private Transform ptf, tf;
    private Rigidbody prb;
    private bool dodging;
    private Vector3 dodgeDir;
    void Start()
    {
        nextPrimary = nextSecondary = nextDodge = 0.0f;
        tf = GetComponent<Transform>();
        ptf = tf.parent;
        prb = ptf.GetComponent<Rigidbody>();
        dodging = false;
        dodgeDir = new Vector3(0.0f,1.0f,0.0f);
    }
    public virtual void processInput(float primary, float secondary, float dodge)
    {
        if (Mathf.Abs(primary) > 0.0f && Time.time > nextPrimary)
        {
            nextPrimary = Time.time + primaryDelay;
            nextSecondary = Time.time + globalDelay;
            nextDodge = Time.time + globalDelay;
            doPrimary();
        }
        if (Mathf.Abs(secondary) > 0.0f && Time.time > nextSecondary)
        {
            nextPrimary = Time.time + globalDelay;
            nextSecondary = Time.time + secondaryDelay;
            nextDodge = Time.time + globalDelay;
            doSecondary();
        }
        if (Mathf.Abs(dodge) > 0.0f && Time.time > nextDodge)
        {
            if(!(nextPrimary > Time.time + globalDelay + dodgeTime))
                nextPrimary = Time.time + dodgeTime + globalDelay;
            nextSecondary = Time.time + dodgeTime + globalDelay;
            nextDodge = Time.time + dodgeTime + dodgeDelay;
            dodging = true;
        }
        if (dodging && nextDodge - dodgeDelay - Time.time > 0.0f)
            doDodge();
        else
        {
            ptf.gameObject.layer = 8;
            dodgeDir = new Vector3(0.0f, 1.0f, 0.0f);
        }
    }

    public virtual void doPrimary()
    {
        Instantiate(primaryPref, tf.position + tf.forward * 0.5f + new Vector3(0.0f, 0.5f, 0.0f), Quaternion.Euler(tf.forward));
    }

    public virtual void doSecondary()
    {
        Instantiate(secondaryPref, tf.position + tf.forward * 0.5f + new Vector3(0.0f, 0.5f, 0.0f), Quaternion.Euler(tf.forward));
    }

    public virtual void doDodge()
    {
        if (dodgeDir == new Vector3(0.0f, 1.0f, 0.0f))
            dodgeDir = Vector3.Normalize(prb.velocity);
        Vector3 newVel = ((dodgeDir * dodgeSpd) + prb.velocity);
        prb.velocity = newVel;
        ptf.gameObject.layer = 15;
    }
}


public class PlayerController : MonoBehaviour
{

    public int playerNum;

    public float speed;
    public float panSpeed;

    private float horizMove, horizFace, vertMove, vertFace;
    private float primaryIn, secondaryIn, dodgeIn;
    private Rigidbody rb;
    private Transform tf;
    private Vector3 csTarg;
    public Camera cs;

    private character child;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        tf = GetComponent<Transform>();
        setNum(playerNum);
        child = tf.GetChild(0).GetComponent<character>();
//            if (playerNum == 0)
//                playerNum = 1;
    }

    void Update()
    {
        horizMove = Input.GetAxis("moveHoriz_P" + playerNum);
        horizFace = Input.GetAxis("faceHoriz_P" + playerNum);
        vertMove = Input.GetAxis("moveVert_P" + playerNum);
        vertFace = Input.GetAxis("faceVert_P" + playerNum);
        primaryIn = Input.GetAxis("primaryAttack_P" + playerNum);
        secondaryIn = Input.GetAxis("secondaryAttack_P" + playerNum);
        dodgeIn = Input.GetAxis("dodge_P" + playerNum);
    }

    void FixedUpdate()
    {
        if (tf.childCount > 0)
        {
            movePlayer();
            child.processInput(primaryIn, secondaryIn, dodgeIn);
        }
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
