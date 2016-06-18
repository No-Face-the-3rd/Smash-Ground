using UnityEngine;
using System.Collections;

public class ExitSwitchRooms : MonoBehaviour
{
    public GameObject nextRoom;
    private RoomChangeManager manager;

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
            manager.switchRoom = true;
        }
    }
}
