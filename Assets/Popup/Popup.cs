using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Popup : MonoBehaviour
{
    public Animator animator;
    public void SelfDestroy() { Destroy(gameObject); }
}
