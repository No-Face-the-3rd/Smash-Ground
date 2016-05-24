using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class character : MonoBehaviour
{
    public int health;
    public float speed;
    public float primaryDelay, secondaryDelay, dodgeDelay, globalDelay, dodgeSpd, dodgeTime;
    private float nextPrimary, nextSecondary, nextDodge;
    public GameObject primaryPref, secondaryPref;
    public Vector3 camTarget;
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
            if(!(nextSecondary > Time.time + globalDelay))
                nextSecondary = Time.time + globalDelay;
            if(!(nextDodge > Time.time + globalDelay))
                nextDodge = Time.time + globalDelay;
            doPrimary();
        }
        if (Mathf.Abs(secondary) > 0.0f && Time.time > nextSecondary)
        {
            if(!(nextPrimary > Time.time + globalDelay))
                nextPrimary = Time.time + globalDelay;
            nextSecondary = Time.time + secondaryDelay;
            if(!(nextDodge > Time.time + globalDelay))
                nextDodge = Time.time + globalDelay;
            doSecondary();
        }
        if (Mathf.Abs(dodge) > 0.0f && Time.time > nextDodge)
        {
            if(!(nextPrimary > Time.time + dodgeTime))
                nextPrimary = Time.time + dodgeTime;
            if(!(nextSecondary > Time.time + dodgeTime))
                nextSecondary = Time.time + dodgeTime;
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
        GameObject tmp = (GameObject)Instantiate(primaryPref, tf.position + tf.forward * 0.5f + new Vector3(0.0f, 0.5f, 0.0f), Quaternion.LookRotation(primaryPref.transform.forward));
        tmp.layer = 9;
    }

    public virtual void doSecondary()
    {
        GameObject tmp = (GameObject)Instantiate(secondaryPref, tf.position + tf.forward * 0.5f + new Vector3(0.0f, 0.5f, 0.0f), Quaternion.LookRotation(secondaryPref.transform.forward));
        tmp.layer = 9;
    }

    public virtual void doDodge()
    {
        if (dodgeDir == new Vector3(0.0f, 1.0f, 0.0f))
            dodgeDir = Vector3.Normalize(prb.velocity);
        Vector3 newVel = ((dodgeDir * dodgeSpd) + prb.velocity);
        prb.velocity = newVel;
        ptf.gameObject.layer = 15;
    }

    public virtual void getDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
            Destroy(gameObject);
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

    public List<GameObject> prefabs;
    private List<GameObject> nextRoom, curRoom;
    private int curInd;

    private character child;

    void Start()
    {

        rb = GetComponent<Rigidbody>();
        tf = GetComponent<Transform>();
        setNum(playerNum);
        child = tf.GetChild(0).GetComponent<character>();

        curRoom.Add(prefabs[0]);
        curRoom.Add(prefabs[1]);
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
        {
            moveCam();
            selectCharacter();
        }
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

        cs.pixelRect = new Rect((0.013f + (float)(playerNum - 1) * 0.7815f) * cs.pixelRect.width, 0.785f * cs.pixelRect.height, 0.1925f * cs.pixelRect.width, 0.195f * cs.pixelRect.height);
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

        rb.velocity = Vector3.Normalize(new Vector3(horizMove, -0.0f, vertMove)) * (GetComponent<PlayerController>().speed + child.GetComponent<character>().speed);
        rb.velocity = new Vector3(rb.velocity.x, Physics.gravity.y * 10.0f * Time.deltaTime, rb.velocity.z);
    }

    void selectCharacter()
    {

    }
}
