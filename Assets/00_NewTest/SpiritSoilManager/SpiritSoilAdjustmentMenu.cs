using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiritSoilAdjustmentMenu : MonoBehaviour
{
    public GameObject shiftButton;
    public GameObject rotateButton;
    public GameObject tripodButton;

    public void Open(bool shiftable, bool rotatable, string ID)
    {
        shiftButton.SetActive(shiftable);
        rotateButton.SetActive(rotatable);
        tripodButton.SetActive(ID == "Tripod");
        gameObject.SetActive(true);
    }
    public void Close()
    {
        gameObject.SetActive(false);
    }
}
