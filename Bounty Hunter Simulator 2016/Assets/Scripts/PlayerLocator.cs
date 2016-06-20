using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerLocator : MonoBehaviour
{
    public GameObject[] players;
    public GameObject[] targetable;
    public bool[] hasSpawned;
    public bool shouldRestart;
    private RoomChangeManager manager;
    private bool checkRestart;

    void Start()
    {
        shouldRestart = checkRestart = false;
        manager = RoomChangeManager.FindObjectOfType<RoomChangeManager>();
        hasSpawned = new bool[0];
    }
    void Update()
    {
        if(checkRestart)
        {
            shouldRestart = true;
        }
        players = GameObject.FindGameObjectsWithTag("Player");
        int targetableToCount = 0;
        int[] ids = new int[players.Length];
        if (hasSpawned.Length < players.Length)
        {
            bool[] tmp = new bool[players.Length];
            for (int i = 0; i < hasSpawned.Length; i++)
            {
                tmp[i] = hasSpawned[i];
            }
            hasSpawned = tmp;

        }
        for (int i = 0; i < players.Length; i++)
        {
            int childCount = players[i].transform.childCount;
            if (childCount > 0)
            {
                if (hasSpawned[i] == false)
                {
                    hasSpawned[i] = true;
                    if (checkRestart == false)
                    {
                        checkRestart = true;
                    }
                }
                shouldRestart = false;
            }
            else
            {
                PlayerController player = players[i].GetComponent<PlayerController>();
                if (hasSpawned[i] != false)
                {
                    if (player.curRoom.Count <= 0 && player.nextRoom.Count <= 0)
                    {
                        shouldRestart = true;
                    }
                    else
                    {
                        if (player.curRoom.Count <= 0)
                            shouldRestart = true;
                        else
                            shouldRestart = false;
                    }
                }
            }
            if (childCount > 0
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
        int temp = 0;
        targetable = new GameObject[targetableToCount];
        for (int i = 0; i < ids.Length; i++)
        {
            if (ids[i] >= 0)
            {
                targetable[temp] = players[ids[i]];
                temp++;
            }
        }




        if (shouldRestart && checkRestart)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

    }
}