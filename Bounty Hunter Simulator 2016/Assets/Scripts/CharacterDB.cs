using UnityEngine;
using System.Collections;

public class CharacterDB : MonoBehaviour {

    public static CharacterDB charData;

    [SerializeField]
    private GameObject[] charDB;

    void Awake()
    {
        if(charData == null)
        {
            DontDestroyOnLoad(gameObject);
            charData = this;
        }
        else if(charData != this)
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        for(int i = 0;i < charDB.Length;i++)
        {
            charDB[i].GetComponent<character>().arrayIndex = i;
        }
    }

    public GameObject getCharacter(int index)
    {
        return charDB[index];
    }

    public int getNumCharacters()
    {
        return charDB.Length;
    }
}
