using UnityEngine;
using System.Collections;

public class ExitSwitchRooms : MonoBehaviour
{
    public GameObject nextRoom;
    public Vector3 nextRoomRelSpawn;



	void Update ()
    {
	    
	}

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 8)
        {
            RoomChangeManager.roomChanger.nextRoom = this.nextRoom;
            if (nextRoomRelSpawn != Vector3.zero)
                nextRoom.GetComponent<RoomSpawn>().relRespawnLoc = this.nextRoomRelSpawn;
            RoomChangeManager.roomChanger.switchRoom = true;
        }
    }
}
