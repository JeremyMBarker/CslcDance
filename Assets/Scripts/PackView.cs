using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PackView : MonoBehaviour
{
    private Text text;
    private string packName;

    // Use this for initialization
    void Start()
    {
        text = GetComponent<Text>();
    }

    public void SetInfo(string _packName)
    {
        packName = _packName;
    }
}
