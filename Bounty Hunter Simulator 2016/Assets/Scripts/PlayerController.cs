using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class character : MonoBehaviour
{
    public int health;
    public float speed;
    public float primaryDelay, secondaryDelay, dodgeDelay, globalDelay, dodgeSpd, dodgeTime;
    protected float nextPrimary, nextSecondary, nextDodge;
    public GameObject primaryPref, secondaryPref, auraPref;
    public Vector3 camTarget;


    protected Transform ptf, tf;
    protected Rigidbody prb;
    private bool dodging;
    private Vector3 dodgeDir;
    public int arrayIndex;
    public int owner;
    public int rescueScore;
    public Color immuneAura;

    private Animator anim;
    private Vector3 lastPos;

    public GameObject deathPref;
    void Start()
    {
        anim = GetComponent<Animator>();
        nextPrimary = nextSecondary = nextDodge = 0.0f;
        tf = GetComponent<Transform>();
        ptf = tf.parent;
        if(ptf != null)
            prb = ptf.GetComponent<Rigidbody>();
        dodging = false;
        dodgeDir = new Vector3(0.0f,1.0f,0.0f);

    }
    void Update()
    {
        if (health <= 0)
        {
            if (transform.parent != null)
            {
                character[] others = FindObjectsOfType<character>();
                for(int i = 0;i < others.Length;i++)
                {
                    if (others[i].owner == owner && others[i].health <= 0 && others[i].transform.parent == null)
                        Destroy(others[i].gameObject);
                }
                transform.parent = null;
                transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                GetComponent<Collider>().enabled = true;
                transform.rotation = Quaternion.Euler(Vector3.zero);
                transform.gameObject.layer = 16;
            }

        }
        if(anim != null)
        {
            Vector3 distTest = lastPos - transform.position;
            if (distTest.magnitude > 0.1f)
            {
                anim.speed = 1.0f;
            }
            else
            {
                anim.speed = 0.0f;
            }
            lastPos = transform.position;
        }
    }
    public virtual void processInput(float primary, float secondary, float dodge)
    {
        if (ptf != null)
        {
            if (Mathf.Abs(primary) > 0.0f && Time.time > nextPrimary)
            {
                nextPrimary = Time.time + primaryDelay;
                if (!(nextSecondary > Time.time + globalDelay))
                    nextSecondary = Time.time + globalDelay;
                if (!(nextDodge > Time.time + globalDelay))
                    nextDodge = Time.time + globalDelay;
                doPrimary();
            }
            if (Mathf.Abs(secondary) > 0.0f && Time.time > nextSecondary)
            {
                if (!(nextPrimary > Time.time + globalDelay))
                    nextPrimary = Time.time + globalDelay;
                nextSecondary = Time.time + secondaryDelay;
                if (!(nextDodge > Time.time + globalDelay))
                    nextDodge = Time.time + globalDelay;
                doSecondary();
            }
            if (Mathf.Abs(dodge) > 0.0f && Time.time > nextDodge)
            {
                if (!(nextPrimary > Time.time + dodgeTime))
                    nextPrimary = Time.time + dodgeTime;
                if (!(nextSecondary > Time.time + dodgeTime))
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
                if (tf.GetComponentsInChildren<ImmuneAura>().Length > 0 && tf.parent.GetComponent<PlayerController>().powerup != PlayerController.powerUps.PROT)
                    Destroy(tf.GetComponentInChildren<ImmuneAura>().gameObject);
                if (Time.time > nextDodge - dodgeDelay)
                    dodging = false;
            }
        }
    }

    public virtual void doPrimary()
    {
        GameObject tmp = (GameObject)Instantiate(primaryPref, tf.position + tf.forward * primaryPref.GetComponent<Bullet>().spawnOffsetLength + transform.parent.rotation * primaryPref.GetComponent<Bullet>().spawnOffsetHeight, Quaternion.LookRotation(transform.forward));
        tmp.layer = 9;
        tmp.GetComponent<Bullet>().owner = transform.parent.GetComponent<PlayerController>().playerNum;
        if(transform.parent.GetComponent<PlayerController>().powerup == PlayerController.powerUps.SPREAD)
        {
            GameObject tmp2 = (GameObject)Instantiate(primaryPref, tf.position + tf.forward * primaryPref.GetComponent<Bullet>().spawnOffsetLength + (Quaternion.Euler(0.0f,primaryPref.GetComponent<Bullet>().spreadAngle,0.0f) * transform.parent.rotation) * primaryPref.GetComponent<Bullet>().spawnOffsetHeight, Quaternion.LookRotation(transform.forward) * Quaternion.Euler(0.0f, primaryPref.GetComponent<Bullet>().spreadAngle,0.0f));
            GameObject tmp3 = (GameObject)Instantiate(primaryPref, tf.position + tf.forward * primaryPref.GetComponent<Bullet>().spawnOffsetLength + (Quaternion.Euler(0.0f, -primaryPref.GetComponent<Bullet>().spreadAngle, 0.0f) * transform.parent.rotation) * primaryPref.GetComponent<Bullet>().spawnOffsetHeight, Quaternion.LookRotation(transform.forward) * Quaternion.Euler(0.0f, -primaryPref.GetComponent<Bullet>().spreadAngle, 0.0f));
            tmp2.layer = tmp3.layer = 9;
            tmp2.GetComponent<Bullet>().owner = tmp3.GetComponent<Bullet>().owner = transform.parent.GetComponent<PlayerController>().playerNum;
        }
    }

    public virtual void doSecondary()
    {
        GameObject tmp = (GameObject)Instantiate(secondaryPref, tf.position + tf.forward * secondaryPref.GetComponent<Bullet>().spawnOffsetLength + transform.parent.rotation * secondaryPref.GetComponent<Bullet>().spawnOffsetHeight, Quaternion.LookRotation(transform.forward));
        tmp.layer = 9;
        tmp.GetComponent<Bullet>().owner = transform.parent.GetComponent<PlayerController>().playerNum;
        if (transform.parent.GetComponent<PlayerController>().powerup == PlayerController.powerUps.SPREAD)
        {
            GameObject tmp2 = (GameObject)Instantiate(secondaryPref, tf.position + tf.forward * secondaryPref.GetComponent<Bullet>().spawnOffsetLength + (Quaternion.Euler(0.0f, secondaryPref.GetComponent<Bullet>().spreadAngle, 0.0f) * transform.parent.rotation) * secondaryPref.GetComponent<Bullet>().spawnOffsetHeight, Quaternion.LookRotation(transform.forward) * Quaternion.Euler(0.0f, secondaryPref.GetComponent<Bullet>().spreadAngle, 0.0f));
            GameObject tmp3 = (GameObject)Instantiate(secondaryPref, tf.position + tf.forward * secondaryPref.GetComponent<Bullet>().spawnOffsetLength + (Quaternion.Euler(0.0f, -secondaryPref.GetComponent<Bullet>().spreadAngle, 0.0f) * transform.parent.rotation) * secondaryPref.GetComponent<Bullet>().spawnOffsetHeight, Quaternion.LookRotation(transform.forward) * Quaternion.Euler(0.0f, -secondaryPref.GetComponent<Bullet>().spreadAngle, 0.0f));
            tmp2.layer = tmp3.layer = 9;
            tmp2.GetComponent<Bullet>().owner = tmp3.GetComponent<Bullet>().owner = transform.parent.GetComponent<PlayerController>().playerNum;
        }
    }

    public virtual void doDodge()
    {
        GameObject tmp = (GameObject)Instantiate(auraPref, tf.position, tf.parent.rotation);
        tmp.transform.parent = tf;
        if (dodgeDir == new Vector3(0.0f, 1.0f, 0.0f))
            dodgeDir = Vector3.Normalize(prb.velocity);
        Vector3 newVel = ((dodgeDir * dodgeSpd) + prb.velocity);
        prb.velocity = newVel;
        ptf.gameObject.layer = 15;
    }

    public virtual void getDamage(int damage)
    {
        health -= damage;
    }
}


public class PlayerController : MonoBehaviour
{
    public enum powerUps { NONE, EVADE, INVIS, NUKE, PROT, SPREAD };

    public int playerNum;

    public float speed;
    public float panSpeed;

    private float horizMove, horizFace, vertMove, vertFace;
    private float primaryIn, secondaryIn, dodgeIn;
    private Rigidbody rb;
    private Transform tf;
    private Vector3 csTarg;
    public Camera cs;
    private float camDist;

    public GameObject charDB;
    [SerializeField]
    public List<int> nextRoom, curRoom;
    private int curInd, prevInd;
    private GameObject curChar, nextChar;

    private bool charSelUsed, charSwitching;
    private int charSel,charSwitchTime;
    private bool charChoose;

    private character child;

    public powerUps powerup;
    public float powerupTime;

    private ScoreManager scorer;
    private GameObject main;

    void Start()
    {
        main = GameObject.FindObjectOfType<MainCameraController>().gameObject;
        scorer = FindObjectOfType<ScoreManager>();
        powerup = powerUps.NONE;
        powerupTime = 0.0f;
        charDB = FindObjectOfType<CharacterDB>().gameObject;
        charSelUsed = false;
        curInd = 0;
        charChoose = false;
        curRoom = new List<int>();
        nextRoom = new List<int>();

        curRoom.Add(0);
        curRoom.Add(1);
        curRoom.Add(2);
        curRoom.Add(3);
        curRoom.Add(4);
        curChar = (GameObject)Instantiate(charDB.GetComponent<CharacterDB>().charDB[curRoom[curInd]], Vector3.zero, Quaternion.LookRotation(new Vector3(0.0f, 0.0f, 1.0f), new Vector3(0.0f, 1.0f, 0.0f)));
        curChar.layer = 11 + playerNum;
        for (int i = 0; i < curChar.transform.childCount; i++)
        {
            curChar.transform.GetChild(i).gameObject.layer = 11 + playerNum;
            for (int j = 0; j < curChar.transform.GetChild(i).childCount; j++)
            {
                curChar.transform.GetChild(i).GetChild(j).gameObject.layer = 11 + playerNum;
            }
        }

        rb = GetComponent<Rigidbody>();
        tf = GetComponent<Transform>();

        camDist = 0.0f;
        setNum(playerNum);
        if(tf.childCount > 0)
            child = tf.GetChild(0).GetComponent<character>();

        charSwitchTime = 0;
    }

    void Update()
    {
        if (tf.childCount == 0)
        {
            child = null;
            powerupTime = 0.0f;
        }
        else
        {
            if (powerup == powerUps.NUKE)
            {
                doNuke();
            }
            if (powerup == powerUps.PROT)
            {
                gameObject.layer = 15;
                ImmuneAura[] tmp = child.transform.GetComponentsInChildren<ImmuneAura>();
                if (tmp.Length < 1)
                {
                    GameObject tmp2 = (GameObject)Instantiate(child.auraPref, child.transform.position, child.transform.rotation);
                    tmp2.transform.parent = child.transform;
                }
            }
            powerupTime -= Time.deltaTime;
            
        }
        
        if(powerupTime <= 0.0f)
        {
            powerupTime = 0.0f;
            if(powerup == powerUps.PROT)
            {
                Destroy(child.transform.GetComponentInChildren<ImmuneAura>().gameObject);
                gameObject.layer = 8;
            }
            powerup = powerUps.NONE;
        }
        horizMove = Input.GetAxis("moveHoriz_P" + playerNum);
        horizFace = Input.GetAxis("faceHoriz_P" + playerNum);
        vertMove = Input.GetAxis("moveVert_P" + playerNum);
        vertFace = Input.GetAxis("faceVert_P" + playerNum);
        primaryIn = Input.GetAxis("primaryAttack_P" + playerNum);
        secondaryIn = Input.GetAxis("secondaryAttack_P" + playerNum);
        dodgeIn = Input.GetAxis("dodge_P" + playerNum);
        charSel = (int)Input.GetAxisRaw("charSel_P" + playerNum);


        if (charSel != 0)
        {
            if(!charSelUsed)
            {
                charSelUsed = true;
            }
            else
            {
                charSel = 0;
            }
        }
        else
        {
            if(!charSwitching)
               charChoose = Input.GetButtonDown("Accept_P" + playerNum);
            charSelUsed = false;
        }
    }

    void FixedUpdate()
    {
        if (tf.childCount > 0)
        {
            movePlayer();
            GetComponent<Collider>().enabled = true;
            GetComponent<Rigidbody>().useGravity = true;
            child.processInput(primaryIn, secondaryIn, dodgeIn);
        }
        else
        {
            if (main.GetComponent<MainCameraController>() == null)
            {
                main = GameObject.FindObjectOfType<MainCameraController>().gameObject;
            }
            Vector3 respawnPoint = main.GetComponent<MainCameraController>().target.GetComponent<RoomSpawn>().relRespawnLoc + main.GetComponent<MainCameraController>().target.transform.position;
            Vector3 tmp = transform.position - respawnPoint;
            if(tmp.magnitude > 0.1f)
            {
                transform.position = respawnPoint;
            }
            GetComponent<Collider>().enabled = false;
            GetComponent<Rigidbody>().useGravity = false;
            moveCam();
            if (curRoom.Count > 0)
            {
                if (charSwitching)
                {
                    switchChar();
                    charSwitchTime++;
                    charChoose = false;
                }
                else
                    selectCharacter();
                if (charSwitchTime > 30)
                {
                    charSwitching = false;
                    Destroy(curChar);
                    curChar = nextChar;
                    charSwitchTime = 0;
                }

            }
            else
            {
                Destroy(curChar);
            }
        }

    }

    public void setNum(int id)
    {
        playerNum = id;
        createCam();
    }

    void createCam()
    {
        csTarg = curChar.GetComponent<character>().camTarget;
        GameObject orig = GameObject.FindGameObjectWithTag("MainCamera");
        cs = ((GameObject)Instantiate(orig, csTarg + new Vector3(0.0f,0.0f,4.0f), Quaternion.Euler(0.0f, 180.0f, 0.0f))).GetComponent<Camera>();
        cs.name = "Player " + playerNum + " Camera";
        cs.tag = "CharSelectCam";
        cs.backgroundColor = Color.gray;
        cs.depth = playerNum;
        cs.cullingMask = 1 << (11 + playerNum);
        cs.orthographic = false;

        Destroy(cs.GetComponent<MainCameraController>());

        cs.pixelRect = new Rect((0.013f + (float)(playerNum - 1) * 0.7815f) * cs.pixelRect.width, 0.785f * cs.pixelRect.height, 0.1925f * cs.pixelRect.width, 0.195f * cs.pixelRect.height);
        ((AudioListener)cs.GetComponent(typeof(AudioListener))).enabled = false;
        ((GUILayer)cs.GetComponent(typeof(GUILayer))).enabled = false;

        cs.fieldOfView = 20.0f;

    }

    void moveCam()
    {
        if (Mathf.Abs(camDist) <= Mathf.Epsilon)
        {
            Vector3 csPos = cs.GetComponent<Transform>().position;
            camDist = Vector3.Distance(csPos, csTarg);
        }
        cs.GetComponent<Transform>().Rotate(new Vector3(vertMove , -horizMove , 0.0f) * panSpeed);
        cs.GetComponent<Transform>().position = csTarg + -(camDist * cs.GetComponent<Transform>().forward);
        cs.GetComponent<Transform>().rotation = Quaternion.Euler(new Vector3(cs.GetComponent<Transform>().rotation.eulerAngles.x, cs.GetComponent<Transform>().rotation.eulerAngles.y, 0.0f));

    }
    void movePlayer()
    {
        //reset cam when playing
        if (cs.GetComponent<Transform>().rotation != Quaternion.Euler(new Vector3(0.0f, 180.0f, 0.0f)))
        {
            cs.GetComponent<Transform>().rotation = Quaternion.Euler(new Vector3(0.0f, 180.0f, 0.0f));
            cs.GetComponent<Transform>().position = csTarg + -(camDist * cs.GetComponent<Transform>().forward);
        }
        if (Mathf.Abs(horizFace) > Mathf.Epsilon || Mathf.Abs(vertFace) > Mathf.Epsilon)
            tf.forward = new Vector3(horizFace, 0.0f, vertFace);
        float tmp = rb.velocity.y;
        rb.velocity = Vector3.Normalize(new Vector3(horizMove, -0.0f, vertMove)) * (GetComponent<PlayerController>().speed + child.speed) * (powerup == powerUps.EVADE ? 1.5f : 1.0f);
        rb.velocity = new Vector3(rb.velocity.x, tmp, rb.velocity.z);
    }

    void selectCharacter()
    {
        prevInd = curInd;
        if (curInd > 0 && charSel != 0)
        {
            curInd = (curInd + charSel) % curRoom.Count;
        }
        else
        {
            if (charSel > 0)
                curInd = 1;
            else if (charSel < 0)
                curInd = curRoom.Count - 1;
        }

        if (prevInd != curInd)
        {
            cycleChar(curInd);
        }
        if (charChoose)
            createChar();
    }

    void cycleChar(int targetIndex)
    {
            charSwitching = true;
            if (cs.GetComponent<Transform>().rotation != Quaternion.Euler(new Vector3(0.0f, 180.0f, 0.0f)))
            {
                cs.GetComponent<Transform>().rotation = Quaternion.Euler(new Vector3(0.0f, 180.0f, 0.0f));
                cs.GetComponent<Transform>().position = csTarg + -(camDist * cs.GetComponent<Transform>().forward);
            }
            nextChar = (GameObject)Instantiate(charDB.GetComponent<CharacterDB>().charDB[curRoom[targetIndex]], new Vector3(-6.2f, 0.0f, 0.0f), Quaternion.LookRotation(new Vector3(0.0f, 0.0f, 1.0f), new Vector3(0.0f, 1.0f, 0.0f)));
            nextChar.layer = 11 + playerNum;
            for(int i = 0;i < nextChar.transform.childCount;i++)
            {
                nextChar.transform.GetChild(i).gameObject.layer = 11 + playerNum;
                for (int j = 0; j < nextChar.transform.GetChild(i).childCount; j++)
                {
                    nextChar.transform.GetChild(i).GetChild(j).gameObject.layer = 11 + playerNum;
                }
            }

    }

    void switchChar()
    {
        curChar.transform.position = curChar.transform.position + new Vector3(0.2f, 0.0f, 0.0f);
        nextChar.transform.position = nextChar.transform.position + new Vector3(0.2f, 0.0f, 0.0f);
        Vector3 tmp = Vector3.zero;
        csTarg = Vector3.SmoothDamp(csTarg, nextChar.GetComponent<character>().camTarget, ref tmp, 0.08f);
    }

    void createChar()
    {
        GameObject tmp = (GameObject)Instantiate(charDB.GetComponent<CharacterDB>().charDB[curRoom[curInd]], transform.position, transform.rotation);
        tmp.transform.SetParent(transform);
        tmp.GetComponent<character>().owner = playerNum;
        child = tmp.GetComponent<character>();
        curRoom.RemoveAt(curInd);
        if (curInd > 0)
            curInd--;
        GetComponent<Collider>().enabled = true;
        rb.useGravity = true;
        powerupTime = 0.0f;
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.layer == 16)
        {
            nextRoom.Add(collision.gameObject.GetComponent<character>().arrayIndex);
            scorer.addScore(playerNum, collision.gameObject.GetComponent<character>().rescueScore);
            Destroy(collision.gameObject);
        }
        if(collision.gameObject.layer == 11)
        {
            if(collision.gameObject.GetComponent<Bullet>() != null)
            {
                child.getDamage(collision.gameObject.GetComponent<Bullet>().damage);
            }
            if (child.health <= 0)
            {
                GameObject tmp = (GameObject)GameObject.Instantiate(child.GetComponent<character>().deathPref, transform.position + new Vector3(0.0f, 1.0f, 0.0f), Quaternion.Euler(Vector3.zero));
                tmp.GetComponent<Renderer>().material.SetColor("_EmissionColor", child.GetComponent<character>().immuneAura);
                GetComponent<Collider>().enabled = false;
                rb.useGravity = false;
                rb.velocity = Vector3.zero;
                curInd = 0;
                if (curRoom.Count > 0)
                {
                    cycleChar(curInd);

                }
                powerup = powerUps.NONE;
            }
        }
    }

    void nextRoomCharacterPrep()
    {
        for (int i = 0; i < curRoom.Count; i++)
        {
            nextRoom.Add(curRoom[i]);
        }
        nextRoom.Sort();
        curInd = 0;
        curRoom = nextRoom;
        cycleChar(curInd);
        nextRoom = new List<int>();
    }

    void doNuke()
    {
        GameObject[] toKill = GameObject.FindGameObjectsWithTag("Active");
        for (int i = 0; i < toKill.Length; i++)
        {
            
            if (toKill[i].GetComponent<EnemyHealth>() != null)
                toKill[i].GetComponent<EnemyHealth>().lastHitBy = -1;
            toKill[i].GetComponent<EnemyDeath>().DoDestroy();
        }

    }
}
