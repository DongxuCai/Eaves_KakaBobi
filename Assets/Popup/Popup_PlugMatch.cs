using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Popup_PlugMatch : MonoBehaviour
{
    public TextMeshProUGUI socketName;
    public TextMeshProUGUI energy;
    public void Initialize(string ID)
    {
        SocketInfo socketInfo = ChunkLibrary.socketInfoDic[ID];
        socketName.text = socketInfo.name_CN;
        energy.text = socketInfo.energy.ToString();
    }
}
