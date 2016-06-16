using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RoomChangeManager : MonoBehaviour
{

    private ScoreManager scorer;
    private PlayerLocator locator;
    private MainCameraController mainCam;
    public GameObject nextRoom;
    public bool switchRoom;

	void Start ()
    {
        scorer = ScoreManager.FindObjectOfType<ScoreManager>();
        locator = PlayerLocator.FindObjectOfType<PlayerLocator>();
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
                for(int i = 0;i < locator.players.Length;i++)
                {
                    PlayerController player = locator.players[i].GetComponent<PlayerController>();
                    if (player.child != null)
                    {
                        player.nextRoom.Add(player.child.arrayIndex);
                        scorer.addScore(player.playerNum, player.child.rescueScore * 2);
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
                    player.cycleChar(player.curInd);
                    player.nextRoom = new List<int>();
                }
            }
        }
    }
}
