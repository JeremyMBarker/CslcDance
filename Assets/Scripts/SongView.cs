using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SongView : MonoBehaviour
{
    [HideInInspector]
    public bool isSelected;
    private Text title;
    private Text artist;

    // Use this for initialization
    void Start()
    {
        var texts = GetComponents<Text>();
        foreach (var text in texts)
        {
            if (text.name == "Title")
                title = text;
            else if (text.name == "Artist")
                artist = text;
        }
    }

    public void SetInfo()
    {
        title.text = "Yes!";
        artist.text = "No!";
    }
}
