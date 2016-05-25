using UnityEngine;
using System.Collections;

public class PlayerLocator : MonoBehaviour
{
    public GameObject [] players;
	void Update ()
    {
        players = GameObject.FindGameObjectsWithTag("Player");
	}
}
