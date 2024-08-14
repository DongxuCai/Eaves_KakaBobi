using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VertexColorCamera : Singleton<VertexColorCamera>
{
    public Camera cam;

    protected override void Awake()
    {
        base.Awake();
        cam.SetReplacementShader(Shader.Find("PostProcess/VertexColor"), "ReplaceVertexColor");
        CreateRenderTexture();
    }

    private void CreateRenderTexture()
    {
        cam.targetTexture = new RenderTexture(Screen.width, Screen.height, 0) { name = "RT_VertexColor" };
    }
}
