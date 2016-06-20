using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class toSpawn
{
    public int ind;
    public float spawnTime;
    public Vector3 relSpawnPos;
}
[System.Serializable]
public class enemyToSpawn : toSpawn
{
    public Vector3 rotation;
    public Vector3 startDriveTo;
}

public class RoomSpawn : MonoBehaviour
{
    public List<enemyToSpawn> enemiesToSpawn;
    public List<toSpawn> charToRescue;
    public GameObject[] doors;
    private EnemyDB enemyData;
    private CharacterDB charData;

    private float startTime;

    public Vector3 relRespawnLoc;

	void Start ()
    {
        enemyData = FindObjectOfType<EnemyDB>();
        charData = FindObjectOfType<CharacterDB>();
        startTime = Time.time;
	}
	
	void Update ()
    {

        for(int i = enemiesToSpawn.Count - 1;i >= 0;i--)
        {
            if(Time.time - startTime >= enemiesToSpawn[i].spawnTime)
            {
                if(enemiesToSpawn[i].ind < enemyData.enemyDB.Length)
                {
                    GameObject tmp = (GameObject)Instantiate(enemyData.enemyDB[enemiesToSpawn[i].ind], transform.position + enemiesToSpawn[i].relSpawnPos, Quaternion.Euler(enemiesToSpawn[i].rotation));
                    if(tmp.gameObject.layer != 10)
                        tmp.gameObject.layer = 10;
                    tmp.tag = "Spawning";
                    if (tmp.GetComponent<driveToTarget>() != null)
                        tmp.GetComponent<driveToTarget>().targetLoc = transform.position + enemiesToSpawn[i].startDriveTo;
                    if (tmp.GetComponent<AimAt>() != null)
                        tmp.GetComponent<AimAt>().aimAtLoc = transform.position + enemiesToSpawn[i].startDriveTo + Quaternion.Euler(enemiesToSpawn[i].rotation) * Vector3.forward * 5.0f;
                    enemiesToSpawn.RemoveAt(i);
                }
            }
        }
        for (int i = charToRescue.Count - 1; i >= 0; i--)
        {
            if(Time.time - startTime >= charToRescue[i].spawnTime)
            {
                if(charToRescue[i].ind < charData.charDB.Length)
                {
                    GameObject tmp = (GameObject)Instantiate(charData.charDB[charToRescue[i].ind], transform.position + charToRescue[i].relSpawnPos, Quaternion.Euler(Vector3.zero));
                    tmp.GetComponent<character>().owner = -i;
                    tmp.GetComponent<character>().health = 0;
                    tmp.transform.parent = transform;
                    charToRescue.RemoveAt(i);
                }
            }
        }
    

        GameObject[] testActive = GameObject.FindGameObjectsWithTag("Active");
        GameObject[] testSpawning = GameObject.FindGameObjectsWithTag("Spawning");

        if(testActive.Length == 0 && enemiesToSpawn.Count == 0 && testSpawning.Length == 0)
        {
            for(int i = 0;i < doors.Length;i++)
            {
                DoorControl door = doors[i].GetComponent<DoorControl>();
                if (!door.doorOpen)
                    door.switchDoor = true;
            }
        }
        else
        {
            for (int i = 0; i < doors.Length; i++)
            {
                DoorControl door = doors[i].GetComponent<DoorControl>();
                if (door.doorOpen)
                    door.switchDoor = true;
            }
        }
        
	}
}
