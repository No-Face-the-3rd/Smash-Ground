using UnityEngine;
using System.Collections;

public class BossPlayMusic : MonoBehaviour
{
    private AudioSource musicObject;
    public AudioClip bossMusic;
    private bool justSpawned;

	void Start ()
    {
        musicObject = GameObject.FindGameObjectWithTag("Music").GetComponent<AudioSource>();
        justSpawned = true;
	}
	

	void Update ()
    {
	    if(justSpawned)
        {
            musicObject.clip = bossMusic;
            musicObject.timeSamples = 0;
            musicObject.Play();
            musicObject.loop = true;
            justSpawned = false;
        }
	}
}
