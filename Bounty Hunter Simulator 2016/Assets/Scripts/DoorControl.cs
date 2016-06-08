using UnityEngine;
using System.Collections;

public class DoorControl : MonoBehaviour {

    public bool switchDoor;
    public Animator anim;
    public bool doorOpen;


	void Start ()
    {
        switchDoor = false;
        anim = GetComponent<Animator>();
        doorOpen = false;
	}

    void Update()
    {

        if (switchDoor)
        {
            doorOpen = !doorOpen;
            int tmp2 = (doorOpen ? 1 : 0);
            anim.Play("Take 001" + tmp2);
            switchDoor = false;
        }
        if (doorOpen)
            GetComponent<Collider>().enabled = false;
        else
            GetComponent<Collider>().enabled = true;
    }
}
