using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RoomChangeManager : MonoBehaviour
{
    public static RoomChangeManager roomChanger;

    private MainCameraController mainCam;
    public GameObject nextRoom;
    public bool switchRoom;

    void Awake ()
    {
        if(roomChanger == null)
        {
            DontDestroyOnLoad(gameObject);
            roomChanger = this;
        }
        else if(roomChanger != this)
        {
            Destroy(gameObject);
        }
    }

	void Start ()
    {
        mainCam = MainCameraController.FindObjectOfType<MainCameraController>();
        nextRoom = null;
        switchRoom = false;
	}

    void Update()
    {
        if (mainCam == null)
            mainCam = MainCameraController.FindObjectOfType<MainCameraController>();
        if (switchRoom)
        {
            if (nextRoom != null)
            {
                switchRoom = false;
                mainCam.target.GetComponent<RoomSpawn>().enabled = false;
                mainCam.target = nextRoom;
                nextRoom.GetComponent<RoomSpawn>().enabled = true;
                nextRoom = null;
                for(int i = 0;i < PlayerLocator.locator.players.Length;i++)
                {
                    PlayerController player = PlayerLocator.locator.players[i].GetComponent<PlayerController>();
                    if (player.child != null)
                    {
                        player.nextRoom.Add(player.child.arrayIndex);
                        ScoreManager.scorer.addScore(player.playerNum, player.child.rescueScore * 2);
                        Destroy(player.child.gameObject);
                    }
                    for(int j = 0;j < player.curRoom.Count;j++)
                    {
                        player.nextRoom.Add(player.curRoom[j]);
                    }
                    player.curRoom.Clear();
                    player.nextRoom.Sort();
                    player.curRoom = player.nextRoom;
                    player.curInd = 0;
                    if(player.curRoom.Count > 0)
                        player.cycleChar(player.curInd);
                    player.nextRoom = new List<int>();
                }
            }
        }
    }
}
