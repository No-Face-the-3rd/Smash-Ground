using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIPowerupManagerScript : MonoBehaviour
{
    public Image[] powerupIcons;
    public RawImage[] powerupDisplay;
    public Texture[] textures;
    //private PlayerLocator locator;
    private GameObject[] players;
    private bool front;
    private float flipTime;

    void Start ()
    {
        front = true;
        flipTime = 0.0f;
        //locator = FindObjectOfType<PlayerLocator>();
	}


    void Update()
    {
        players = GameObject.FindGameObjectsWithTag("Player");
        for (int i = 0; i < /*locator.*/players.Length; i++)
        {
            int idTmp = /*locator.*/players[i].GetComponent<PlayerController>().playerNum - 1;
            if (idTmp < powerupIcons.Length)
            {
                int textureID = ((int)/*locator.*/players[i].GetComponent<PlayerController>().powerup * 2 - (front ? 1 : 0));
                textureID = textureID < 0 ? 0 : textureID;
                powerupDisplay[idTmp].texture = textures[textureID];
            }
        }

        if(flipTime >= 0.5f)
        {
            front = !front;
            flipTime = 0.0f;
        }

        flipTime += Time.deltaTime;
    }
}
