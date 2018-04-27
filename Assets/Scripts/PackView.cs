using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PackView : MonoBehaviour
{
    private Text packName;

    // Use this for initialization
    void Start()
    {
        packName = GetComponent<Text>();
    }

    public void SetInfo(string _packName)
    {
        packName.text = _packName;
    }
}
