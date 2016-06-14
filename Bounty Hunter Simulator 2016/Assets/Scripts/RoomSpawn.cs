using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class enemyToSpawn
{
    public int ind;
    public float spawnTime;
    public Vector3 relSpawnPos;
    public Vector3 rotation;
    public Vector3 startDriveTo;
}

public class RoomSpawn : MonoBehaviour
{
    public List<enemyToSpawn> enemiesToSpawn;
    public GameObject[] doors;
    private EnemyDB enemyData;

    private float startTime;

	void Start ()
    {
        enemyData = FindObjectOfType<EnemyDB>();
        startTime = Time.time;
	}
	
	void Update ()
    {
        List<int> indToDel = new List<int>();
	    for(int i = 0;i < enemiesToSpawn.Count;i++)
        {
            if (Time.time - startTime >= enemiesToSpawn[i].spawnTime)
            {
                if (enemiesToSpawn[i].ind < enemyData.enemyDB.Length)
                {
                    GameObject tmp = (GameObject)Instantiate(enemyData.enemyDB[enemiesToSpawn[i].ind], transform.position + enemiesToSpawn[i].relSpawnPos, Quaternion.Euler(enemiesToSpawn[i].rotation));
                    if (tmp.gameObject.layer != 10)
                        tmp.gameObject.layer = 10;
                    tmp.tag = "Active";
                    tmp.GetComponent<driveToTarget>().targetLoc = enemiesToSpawn[i].startDriveTo;
                    indToDel.Add(i);
                }
            }
        }
        for(int i = indToDel.Count - 1;i >= 0;i--)
        {
            enemiesToSpawn.RemoveAt(indToDel[i]);
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
        
	}
}
