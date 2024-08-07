using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Singleton<PlayerController>
{
    public Ray ray;
    private void Start() { Application.targetFrameRate = 60; }
    private void Update() { Raycast(); }
    private void Raycast() { ray = Camera.main.ScreenPointToRay(Input.mousePosition); }
}
