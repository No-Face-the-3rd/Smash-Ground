using UnityEngine;
using System.Collections;

public class CharacterDB : MonoBehaviour {


    public GameObject[] charDB;

    void Start()
    {
        for(int i = 0;i < charDB.Length;i++)
        {
            charDB[i].GetComponent<character>().arrayIndex = i;
        }
    }
}
