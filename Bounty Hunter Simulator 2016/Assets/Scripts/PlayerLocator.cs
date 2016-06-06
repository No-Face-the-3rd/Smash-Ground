using UnityEngine;
using System.Collections;

public class PlayerLocator : MonoBehaviour
{
    public GameObject [] players;
    public GameObject [] targetable;
	void Update ()
    {
        players = GameObject.FindGameObjectsWithTag("Player");
        int targetableToCount = 0;
        int[] ids = new int[players.Length];
        for (int i = 0;i < players.Length;i++)
        {
            if(players[i].transform.childCount > 0 
                && players[i].GetComponent<PlayerController>().powerup != PlayerController.powerUps.INVIS 
                && players[i].layer != 15)
            {
                targetableToCount++;
                ids[i] = i;
            }
            else
            {
                ids[i] = -1;
            }
        }
        targetable = new GameObject[targetableToCount];
        for(int i = 0;i <ids.Length;i++)
        {
            if(ids[i] >= 0)
            {
                targetable[ids[i]] = players[ids[i]];
            }
        }
    }
}