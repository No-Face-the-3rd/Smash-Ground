using UnityEngine;
using System.Collections;

public class DestroyAfterTimer : MonoBehaviour
{
    public float timer;
    private EnemyDeath enmD;

    void Start()
    {
        enmD = GetComponent<EnemyDeath>();
    }

	void Update ()
    {
        timer -= Time.deltaTime;
        if(timer <= 0)
        {
            if(enmD == null)
            {
                Destroy(this.gameObject);
            }
        }
	}
}
