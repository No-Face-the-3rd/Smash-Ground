using UnityEngine;
using System.Collections;

public class ExitSwitchRooms : MonoBehaviour
{
    public GameObject nextRoom;
    private RoomChangeManager manager;
    public Vector3 nextRoomRelSpawn;

	void Start ()
    {
        manager = RoomChangeManager.FindObjectOfType<RoomChangeManager>();
	}
	

	void Update ()
    {
	    
	}

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 8)
        {
            manager.nextRoom = this.nextRoom;
            if (nextRoomRelSpawn != Vector3.zero)
                nextRoom.GetComponent<RoomSpawn>().relRespawnLoc = this.nextRoomRelSpawn;
            manager.switchRoom = true;
        }
    }
}
