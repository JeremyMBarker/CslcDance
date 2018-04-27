using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SongView : MonoBehaviour
{
    private Text title;
    private Text artist;

    // Use this for initialization
    void Start()
    {
        var texts = GetComponentsInChildren<Text>();
        foreach (var text in texts)
        {
            if (text.name == "Title")
                title = text;
            else if (text.name == "Artist")
                artist = text;
        }
    }

    public void SetInfo(string _title, string _artist)
    {
        title.text = _title;
        artist.text = _artist;
    }
}