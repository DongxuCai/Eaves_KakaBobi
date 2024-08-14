using UnityEngine;
using System.Collections;
using System;


/// <summary>
/// �����л�
/// </summary>
[Serializable]
public class PostProcessFilter : ScriptableObject
{
    [Header("Mode Switch")]
    [Range(0, 1)]
    public float BuildColLerp = 0.55f; //Խ�ӽ�0 Խʹ��ԭɫ�ʣ�Խ�ӽ�1 Խʹ��BuildCol
    public Color BuildCol = Color.white; // ����ģʽ��ɫ
    public Texture2D BuildTex;
    [Range(0, 1)]
    public float NoisePower = 0.1f; //Խ�ӽ�0 ƽ����Խ�ӽ�1 Խë��
    public Vector2 NoiseRange = new Vector2(0.94f, 0.99f);
    public float NoiseFactor = 10f;

    [Header("Saturation&Contrast")]
    public Vector4 BuildSaturation = new Vector4 (2f,1f,1f,1f); //����ģʽ�µĽ������Ͷ�
    public Vector4 BuildContrast = new Vector4(2f, 1f, 1f,1f); //����ģʽ�µĽ����Աȶ�
    public Vector4 PureMaskRange = new Vector4(0.2f, 0.2f, 0.2f,1f); //��ɫ��Χ
    public float RoamSaturation = 1f; //����ģʽ�µĽ������Ͷ�
    public float RoamContrast = 1f; //����ģʽ�µĽ����Աȶ�
    



    [Header("Outline Color")]
    [Space(20)]
    [Range(0, 1)]
    public float useObjcetColor_Build = 0.5f; //Խ�ӽ�0 Խʹ��EdgeColor��Խ�ӽ�1 Խʹ�����屾����ɫ
    public Color buildEdgeColor = Color.black; // ��Ե��ɫ
    [Range(0, 1)]
    public float useObjcetColor_Roam= 0.5f; //Խ�ӽ�0 Խʹ��EdgeColor��Խ�ӽ�1 Խʹ�����屾����ɫ
    public Color roamEdgeColor = Color.black; // ��Ե��ɫ
    public Texture2D ScreenNoise;


    [Header("Fog")]
    [Space(20)]
    [Range(0, 1)]
    public float fogUseObjcetColor = 0.5f; //Խ�ӽ�0 Խʹ��FogEdgeColor��Խ�ӽ�1 Խʹ�����屾����ɫ
    public Color FogEdgeColor = Color.white; // ����Ե��ɫ
    public float FogColorDistanceFactor = 5f; // �������� ��ֵԽ�������ɫ����Ӱ��Խ��
    public float FogWidthDistanceFactor = 5f; // �������� ��ֵԽ����ߴ�ϸ����Ӱ��Խ��


    [Header("Depth & Normal Outline")]
    [Space(20)]
    public float DepthNormalEdgeWidth = 0.5f; // ���ƶ���ȣ�������������ʱ ��ʹ�õĲ������롣ֵԽ�����Խ��
    public float SensitivityDepth = 5f; // ������жȣ�Ӱ�쵱��������ֵ������ʱ���ᱻ��Ϊ����һ���߽�
    public float SensitivityNormals_Build = 1.0f; // �������жȣ�Ӱ�쵱����ķ���ֵ������ʱ���ᱻ��Ϊ����һ���߽�
    public float SensitivityNormals_Roam = 1.0f; // �������жȣ�Ӱ�쵱����ķ���ֵ������ʱ���ᱻ��Ϊ����һ���߽�


    [Header("Color Outline")]
    [Space(20)]
    [Range(0.0f, 5.0f)]
    public float ColorEdgeWidth = 1.0f; // ������ɫ����߿��ȣ�ֵԽ�����Խ��
    [Range(0.0f, 5.0f)]
    public float ColorEdgeStrength = 1.0f;// ������ɫ��������أ�ֵԽ�����Խ��
    [Range(0.0f, 1.0f)]
    public float ColorEdgeThreshold = 1.0f;// ������ɫ��������жȣ�ֵԽС�����Խ����



    [Header("Screen Wave")]
    [Space(20)]
    [Range(0, 1)]
    public float RenderColLerp = 0.55f; //ˮ����ɫ
    [Range(0, 1)]
    public float RandomColRange = 0.1f; //ˮ����ɫ��ǳ��Χ
    public Texture2D[] RenderTex01;
    public Texture2D[] RenderTex02;
    [Range(0.0f, 10f)]
    public float ScreenWaveSharp = 2f;
    [Range(0.0f, 1f)]
    public float ScreenWaveWidth = 0.1f;
    [Range(0.0f, 10f)]
    public float ScreenWavePower = 0.1f;
    public float ScreenWaveSpeed = 5;
}


