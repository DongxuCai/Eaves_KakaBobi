using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Button : MonoBehaviour
{
    public Image button;
    public Sprite button_Normal;
    public Sprite button_Highlight;
    private void OnEnable() { button.sprite = button_Normal; }
    public void HoverOn() { button.sprite = button_Highlight; }
    public void HoverOff() { button.sprite = button_Normal; }
    public void Click() { }
}
