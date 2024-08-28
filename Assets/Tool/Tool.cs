using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tool : Singleton<Tool>
{
    protected override void Awake() { base.Awake(); DontDestroyOnLoad(this); }
    public static Vector3Int RotateCoord(Vector3Int coord) { return new Vector3Int(coord.z, coord.y, -coord.x); }
    public static int GetRotation(int rotation)
    {
        if (rotation > 2) { rotation -= 4; }
        else if (rotation < -1) { rotation += 4; }
        return rotation;
    }
    public static Vector3Int RotateDirection(Vector3Int direction, int rotation)
    {
        Vector3Int[] directions = new Vector3Int[4] { Vector3Int.forward, Vector3Int.right, Vector3Int.back, Vector3Int.left };
        int result = Array.IndexOf(directions, direction) + rotation;
        if (result > 3) { result -= 4; }
        else if (result < 0) { result += 4; }
        return directions[result];
    }
    public static Vector3Int Vector3ToVector3Int(Vector3 inputVector)
    {
        int x = (int)inputVector.x;
        int y = (int)inputVector.y;
        int z = (int)inputVector.z;
        return new Vector3Int(x, y, z);
    }
    public static Vector3 StringToVector3(string coord, char split)
    {
        int n = coord.Length;
        string[] info = coord.Substring(1, n - 2).Split(split);
        float x = float.Parse(info[0]);
        float y = float.Parse(info[1]);
        float z = float.Parse(info[2]);
        return new Vector3(x, y, z);
    }
    public static Vector3Int StringToVector3Int(string coord, char split)
    {
        int n = coord.Length;
        string[] info = coord.Substring(1, n - 2).Split(split);
        int x = int.Parse(info[0]);
        int y = int.Parse(info[1]);
        int z = int.Parse(info[2]);
        return new Vector3Int(x, y, z);
    }
    public static void ProgessBarTrigger(ref bool tryTrigger, Image progressBar, float duration, Action Method)
    {
        if (tryTrigger)
        {
            progressBar.fillAmount += duration * Time.deltaTime;
            if (progressBar.fillAmount == 1f) { Method(); tryTrigger = false; }
        }
        else if (progressBar.fillAmount > 0f) { progressBar.fillAmount = 0f; }
    }
    public static float CalculateSunlightDegree(float period)
    {
        float degree = (period - 1) / 12 - 0.25f;
        if (degree <= 0) { degree += 1; }
        return degree;
    }
    public static void RestartAnimator(Animator animator)
    {
        animator.Rebind();
        animator.Update(0f);
    }
    public static T ParseEnum<T>(string value)
    {
        return (T)Enum.Parse(typeof(T), value, true);
    }
    public static void SetImageAlpha(Image image, float alpha)
    {
        image.color = new Color(image.color.r, image.color.g, image.color.b, alpha);
    }
    public static void SetRawImageAlpha(RawImage image, float alpha)
    {
        image.color = new Color(image.color.r, image.color.g, image.color.b, alpha);
    }
    public static int GetNextIndex(int index, int count)
    {
        if (index == count - 1) return 0;
        else return index + 1;
    }
    public static int GetPreviousIndex(int index, int count)
    {
        if (index == 0) return count - 1;
        else return index - 1;
    }
    public static void DestoryAllChild(Transform parent) { foreach (Transform child in parent) { Destroy(child.gameObject); } }

    public static void SetUIFacingScreen(Transform UI)
    {
        Camera camera = Camera.main;
        UI.LookAt(UI.position - camera.transform.rotation * Vector3.back, camera.transform.rotation * Vector3.up);
    }
    public static void SynchronizeCameraWithMain(GameObject camera)
    {
        camera.transform.position = Camera.main.transform.position;
        camera.transform.rotation = Camera.main.transform.rotation;
    }

    public static bool StringArrayEqual(string[] a, string[] b)
    {
        if (a.Length != b.Length) { return false; }
        for (int i = 0; i < a.Length; i++) { if (a[i] != b[i]) { return false; } }
        return true;
    }
    public static List<T> DeepCopyList<T>(List<T> list)
    {
        List<T> newlist = new List<T>();
        foreach (var item in list) { newlist.Add(item); }
        return newlist;
    }
    public static Vector3 AttachToGrid(Vector3 coord, Vector3 scale)
    {
        return new Vector3(Mathf.RoundToInt(coord.x / scale.x) * scale.x, Mathf.RoundToInt(coord.y / scale.y) * scale.y, Mathf.RoundToInt(coord.z / scale.z) * scale.z);
    }
    public static Vector3 AttachToGrid2D(Vector3 coord, Vector3 scale)
    {
        return new Vector3(Mathf.RoundToInt(coord.x / scale.x) * scale.x, coord.y, Mathf.RoundToInt(coord.z / scale.z) * scale.z);
    }

}