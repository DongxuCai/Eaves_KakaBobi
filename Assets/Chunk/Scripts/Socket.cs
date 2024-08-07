using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Socket : MonoBehaviour
{
    public string ID;
    public string targetID;
    private void Awake() { targetID = null;  }
    private void OnTriggerEnter(Collider other)
    {
        targetID = other.GetComponent<Socket>().ID;
        
    }
    private void OnTriggerExit(Collider other)
    {
        targetID = null;
    }
}
