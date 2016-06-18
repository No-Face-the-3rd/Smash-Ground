using UnityEngine;
using System.Collections;

public class ImmuneAura : MonoBehaviour {

    private bool setParent;

	void Start ()
    {
        setParent = false;
	}



    void Update ()
    {
        if (transform.parent != null && setParent == false)
        {
            GetComponent<Renderer>().material.SetColor("_EmissionColor", transform.parent.GetComponent<character>().immuneAura);
            setParent = true;
        }
	}
}
