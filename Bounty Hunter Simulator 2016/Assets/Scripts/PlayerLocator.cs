using UnityEngine;
using System.Collections;

public class PlayerLocator : MonoBehaviour
{
    public GameObject [] players;
	void Update ()
    {
        GameObject [] tmp = GameObject.FindGameObjectsWithTag("Player");
        int playersToCount = 0;
        int[] ids = new int[tmp.Length];
        for (int i = 0;i < tmp.Length;i++)
        {
            if(tmp[i].transform.childCount > 0 /*|| tmp[i].GetComponent<PlayerController>().powerup != PlayerController.powerUps.INVIS*/)
            {
                playersToCount++;
                ids[i] = i;
            }
            else
            {
                ids[i] = -1;
            }
        }
        players = new GameObject[playersToCount];
        for(int i = 0;i <ids.Length;i++)
        {
            if(ids[i] >= 0)
            {
                players[ids[i]] = tmp[ids[i]];
            }
        }
    }
}

//Jacob wrote