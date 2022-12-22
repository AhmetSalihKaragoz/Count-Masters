using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour
{
    public void TurnOffColliders()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).CompareTag("Door"))
            {
                transform.GetChild(i).GetComponent<Collider>().enabled = false;
            }
        }
    }
}
