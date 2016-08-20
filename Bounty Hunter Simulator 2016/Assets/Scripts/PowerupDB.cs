using UnityEngine;
using System.Collections;

public class PowerupDB : MonoBehaviour
{
    public static PowerupDB powerUpData;

    [SerializeField]
    private GameObject[] powerups;

    void Awake()
    {
        if(powerUpData == null)
        {
            DontDestroyOnLoad(gameObject);
            powerUpData = this;
        }
        else if(powerUpData != this)
        {
            Destroy(gameObject);
        }
    }

	void Start ()
    {
	
	}
	

	void Update ()
    {
	
	}

    public GameObject getPowerUp(int index)
    {
        return powerups[index];
    }

    public int getNumPowerUps()
    {
        return powerups.Length;
    }
}
