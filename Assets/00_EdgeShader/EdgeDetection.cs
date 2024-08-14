using UnityEngine;

public class EdgeDetection : MonoBehaviour
{
    private Material edgeDetection_Material;
    public PostProcessFilter PossProcessFilterAsset;
    [HideInInspector] public PostProcessFilter ppf;
    [HideInInspector] public float ChangeMode = 1f;

    Vector2 cursorPos;
    [HideInInspector] public float ScreenWaveAlpha = 1;
    [HideInInspector] public Vector3 cursorWPos;
    [HideInInspector] public float ScreenWaveTime = 0;
    [HideInInspector] public float renderTexValue = 1;

    [HideInInspector] public int randomInt01;
    [HideInInspector] public int randomInt02;
    [HideInInspector] public float Render01Size;
    [HideInInspector] public float RenderColRange;

    VertexColorCamera VVC;


    private void Start()
    {
        edgeDetection_Material = new Material(Shader.Find("PostProcess/EdgeDetection"));
        ppf = PossProcessFilterAsset;
        GetComponent<Camera>().depthTextureMode |= DepthTextureMode.Depth;
        VVC = VertexColorCamera.Instance;
    }

    [ImageEffectOpaque]
    private void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        if (edgeDetection_Material != null)
        {
            edgeDetection_Material.SetFloat("_ChangeMode", ChangeMode);
            edgeDetection_Material.SetFloat("_BuildColLerp", ppf.BuildColLerp);
            edgeDetection_Material.SetColor("_BuildCol", ppf.BuildCol);
            edgeDetection_Material.SetTexture("_BuildTex", ppf.BuildTex);
            edgeDetection_Material.SetFloat("_NoisePower", ppf.NoisePower * (1 - ChangeMode));
            edgeDetection_Material.SetVector("_NoiseRange", ppf.NoiseRange);
            edgeDetection_Material.SetFloat("_NoiseFactor", ppf.NoiseFactor);
            edgeDetection_Material.SetVector("_BuildSaturation", ppf.BuildSaturation);
            edgeDetection_Material.SetVector("_BuildContrast", ppf.BuildContrast);
            edgeDetection_Material.SetFloat("_RoamSaturation", ppf.RoamSaturation);
            edgeDetection_Material.SetFloat("_RoamContrast", ppf.RoamContrast);
            edgeDetection_Material.SetVector("_PureMaskRange", ppf.PureMaskRange);



            edgeDetection_Material.SetFloat("_useObjcetColor", Mathf.Lerp(ppf.useObjcetColor_Build, ppf.useObjcetColor_Roam, ChangeMode));

            Color edgeColor = Color.Lerp(ppf.buildEdgeColor, ppf.roamEdgeColor, ChangeMode);
            edgeDetection_Material.SetColor("_EdgeColor", edgeColor);
            edgeDetection_Material.SetColor("_FogEdgeColor", ppf.FogEdgeColor);
            edgeDetection_Material.SetFloat("_fogUseObjcetColor", ppf.fogUseObjcetColor);
            edgeDetection_Material.SetFloat("_FogEdgeColorFactor", ppf.FogColorDistanceFactor);
            edgeDetection_Material.SetFloat("_FogEdgeWidthFactor", ppf.FogWidthDistanceFactor);
            edgeDetection_Material.SetFloat("_SampleDistance", ppf.DepthNormalEdgeWidth);
            edgeDetection_Material.SetVector("_Sensitivity", new Vector4(Mathf.Lerp(ppf.SensitivityNormals_Build, ppf.SensitivityNormals_Roam, ChangeMode), ppf.SensitivityDepth, 0.0f, 0.0f));
            edgeDetection_Material.SetFloat("_ColorWidth", ppf.ColorEdgeWidth * Screen.width / 1080f);
            edgeDetection_Material.SetFloat("_ColorStrength", ppf.ColorEdgeStrength);
            edgeDetection_Material.SetFloat("_Threshold", ppf.ColorEdgeThreshold);
            edgeDetection_Material.SetTexture("_VertexColorMask", VVC.cam.targetTexture);
            edgeDetection_Material.SetTexture("_ScreenNoise", ppf.ScreenNoise);

            cursorPos = Camera.main.WorldToScreenPoint(cursorWPos);

            edgeDetection_Material.SetTexture("_RenderTex01", ppf.RenderTex01[randomInt01]);
            edgeDetection_Material.SetTexture("_RenderTex02", ppf.RenderTex02[randomInt02]);
            edgeDetection_Material.SetFloat("_RenderColLerp", Mathf.Lerp(1, ppf.RenderColLerp + ppf.RandomColRange * RenderColRange, renderTexValue));
            edgeDetection_Material.SetFloat("_Render01Size", Render01Size);
            edgeDetection_Material.SetVector("_ScreenPos", cursorPos);
            edgeDetection_Material.SetFloat("_ScreenWaveAlpha", ScreenWaveAlpha);
            edgeDetection_Material.SetFloat("_ScreenWaveTime", ScreenWaveTime);
            edgeDetection_Material.SetFloat("_ScreenWaveSharp", ppf.ScreenWaveSharp);
            edgeDetection_Material.SetFloat("_ScreenWaveWidth", ppf.ScreenWaveWidth);
            edgeDetection_Material.SetFloat("_ScreenWaveSpeed", ppf.ScreenWaveSpeed);
            edgeDetection_Material.SetFloat("_ScreenWavePower", ppf.ScreenWavePower);

            Graphics.Blit(src, dest, edgeDetection_Material);
        }
        else
        {
            Graphics.Blit(src, dest);
        }
    }


}
