using UnityEngine;
using System.Collections;

public class DoorControl : MonoBehaviour {

    public bool switchDoor;
    public Animator anim;
    public bool doorOpen;
    public GameObject exitSign;
    private AudioSource audio;

	void Start ()
    {
        switchDoor = false;
        anim = GetComponent<Animator>();
        doorOpen = false;
        audio = GetComponent<AudioSource>();
	}

    void Update()
    {

        if (switchDoor)
        {
            doorOpen = !doorOpen;
            int tmp2 = (doorOpen ? 1 : 0);
            anim.Play("Take 001" + tmp2);
            if(audio !=null)
            {
                audio.timeSamples = doorOpen ? audio.clip.samples - 1 : 0;
                audio.pitch = doorOpen ? -1.0f : 1.0f;
                audio.Play();
            }
            switchDoor = false;
        }
        
        if (doorOpen)
        {
            if(exitSign != null)
                exitSign.SetActive(true);
        }
        else
        {
            if(exitSign != null)
                exitSign.SetActive(false);
        }
    }
}
