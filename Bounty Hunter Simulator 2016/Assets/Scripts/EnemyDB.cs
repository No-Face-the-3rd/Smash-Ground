using UnityEngine;
using System.Collections;

public class EnemyDB : MonoBehaviour
{
    public static EnemyDB enemyData;

    [SerializeField]
    private GameObject[] enemyDB;

    void Awake()
    {
        if(enemyData == null)
        {
            DontDestroyOnLoad(gameObject);
            enemyData = this;
        }
        else if(enemyData != this)
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

    public GameObject getEnemy(int index)
    {
        return enemyDB[index];
    }

    public int getNumEnemies()
    {
        return enemyDB.Length;
    }
}
