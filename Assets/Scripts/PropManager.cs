using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropManager : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject, 0.0f);
    }
}
