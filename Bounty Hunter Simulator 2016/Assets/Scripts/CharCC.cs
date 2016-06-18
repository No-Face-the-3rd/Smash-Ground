using UnityEngine;
using System.Collections;

public class CharCC : character {




    public override void doSecondary()
    {
        GameObject tmp = (GameObject)Instantiate(secondaryPref, tf.position + tf.right * secondaryPref.GetComponent<Bullet>().spawnOffsetLength + transform.parent.rotation * secondaryPref.GetComponent<Bullet>().spawnOffsetHeight, Quaternion.LookRotation(transform.right));
        tmp.layer = 9;
        tmp.GetComponent<Bullet>().owner = transform.parent.GetComponent<PlayerController>().playerNum;
        if (transform.parent.GetComponent<PlayerController>().powerup == PlayerController.powerUps.SPREAD)
        {
            GameObject tmp2 = (GameObject)Instantiate(secondaryPref, tf.position + tf.right * secondaryPref.GetComponent<Bullet>().spawnOffsetLength + (Quaternion.Euler(0.0f, secondaryPref.GetComponent<Bullet>().spreadAngle, 0.0f) * transform.parent.rotation) * secondaryPref.GetComponent<Bullet>().spawnOffsetHeight, Quaternion.LookRotation(transform.right) * Quaternion.Euler(0.0f, secondaryPref.GetComponent<Bullet>().spreadAngle, 0.0f));
            GameObject tmp3 = (GameObject)Instantiate(secondaryPref, tf.position + tf.right * secondaryPref.GetComponent<Bullet>().spawnOffsetLength + (Quaternion.Euler(0.0f, -secondaryPref.GetComponent<Bullet>().spreadAngle, 0.0f) * transform.parent.rotation) * secondaryPref.GetComponent<Bullet>().spawnOffsetHeight, Quaternion.LookRotation(transform.right) * Quaternion.Euler(0.0f, -secondaryPref.GetComponent<Bullet>().spreadAngle, 0.0f));
            tmp2.layer = tmp3.layer = 9;
            tmp2.GetComponent<Bullet>().owner = tmp3.GetComponent<Bullet>().owner = transform.parent.GetComponent<PlayerController>().playerNum;
        }
        GameObject tmp4 = (GameObject)Instantiate(secondaryPref, tf.position + -tf.right * secondaryPref.GetComponent<Bullet>().spawnOffsetLength + transform.parent.rotation * secondaryPref.GetComponent<Bullet>().spawnOffsetHeight, Quaternion.LookRotation(-transform.right));
        tmp4.layer = 9;
        tmp4.GetComponent<Bullet>().owner = transform.parent.GetComponent<PlayerController>().playerNum;
        if (transform.parent.GetComponent<PlayerController>().powerup == PlayerController.powerUps.SPREAD)
        {
            GameObject tmp2 = (GameObject)Instantiate(secondaryPref, tf.position + -tf.right * secondaryPref.GetComponent<Bullet>().spawnOffsetLength + (Quaternion.Euler(0.0f, secondaryPref.GetComponent<Bullet>().spreadAngle, 0.0f) * transform.parent.rotation) * secondaryPref.GetComponent<Bullet>().spawnOffsetHeight, Quaternion.LookRotation(-transform.right) * Quaternion.Euler(0.0f, secondaryPref.GetComponent<Bullet>().spreadAngle, 0.0f));
            GameObject tmp3 = (GameObject)Instantiate(secondaryPref, tf.position + -tf.right * secondaryPref.GetComponent<Bullet>().spawnOffsetLength + (Quaternion.Euler(0.0f, -secondaryPref.GetComponent<Bullet>().spreadAngle, 0.0f) * transform.parent.rotation) * secondaryPref.GetComponent<Bullet>().spawnOffsetHeight, Quaternion.LookRotation(-transform.right) * Quaternion.Euler(0.0f, -secondaryPref.GetComponent<Bullet>().spreadAngle, 0.0f));
            tmp2.layer = tmp3.layer = 9;
            tmp2.GetComponent<Bullet>().owner = tmp3.GetComponent<Bullet>().owner = transform.parent.GetComponent<PlayerController>().playerNum;
        }
    }
}
