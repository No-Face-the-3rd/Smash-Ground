using UnityEngine;
using System.Collections;

public class EnemySkin : MonoBehaviour
{
    private SkinnedMeshRenderer skin;
    public Color color;
  

    void Start ()
    {
        skin = GetComponentInChildren<SkinnedMeshRenderer>();
    }
	
	public virtual void Update ()
    {
        if (skin.material.color != color)
        {
            skin.material.color = color;
        }
    }
}
