using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UILifeManagerScript : MonoBehaviour
{
    public Text[] liveTexts;
    private bool curRoom;
    private float swapRoom;

	void Start ()
    {
        swapRoom = 0.0f;
        curRoom = true;
	}
	
	void Update ()
    {
        for(int i =0;i < PlayerLocator.locator.players.Length;i++)
        {
            int idTmp = PlayerLocator.locator.players[i].GetComponent<PlayerController>().playerNum - 1;
            if (idTmp < liveTexts.Length)
            {
                if (curRoom)
                {
                    liveTexts[idTmp].text = "Current: " + PlayerLocator.locator.players[i].GetComponent<PlayerController>().curRoom.Count;
                }
                else
                {
                    liveTexts[idTmp].text = "Next: " + PlayerLocator.locator.players[i].GetComponent<PlayerController>().nextRoom.Count;
                }
            }
        }
        if(swapRoom >= 2.0f)
        {
            curRoom = !curRoom;
            swapRoom = 0.0f;
        }

        swapRoom += Time.deltaTime;
    }
}
