using UnityEngine;
using System.Collections;

public class EnemyDeath : MonoBehaviour
{
    public void DoDestroy()
    {
        Destroy(this.gameObject);
    }
}
