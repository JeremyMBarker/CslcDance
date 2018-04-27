using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectionWheel : MonoBehaviour
{
    private Text[][] SongText;
    private Text[] PackText;
    private List<PackInfo> PackList;
    private int startingIndex;

    // Use this for initialization
    void Start()
    {
        startingIndex = 4; // index of center item in wheel
        // Get references to all the text components
        SongText = new Text[9][]; // [#][0] = title, [#][1] = artist
        PackText = new Text[9];
        foreach (var item in GetComponentsInChildren<WheelItem>())
        {
            foreach (var text in item.GetComponentsInChildren<Text>())
            {
                if (SongText[item.Index] == null)
                    SongText[item.Index] = new Text[2];
                // This isn't really maintainable, so DON'T EVER change the names of the GameoObjects
                if (text.name == "Title")
                    SongText[item.Index][0] = text;
                else if (text.name == "Artist")
                    SongText[item.Index][1] = text;
                else if (text.name == "Pack")
                    PackText[item.Index] = text;
            }
        }

        // Initially, show the song packs
        foreach (var texts in SongText)
            foreach (var text in texts)
                text.gameObject.SetActive(false);
        PackList = new StepmaniaParser().LoadSongs();
        for (var i = startingIndex; i < PackText.Length; i++)
            PackText[i].text = PackList[(i - startingIndex) % PackList.Count].Name;
        for (var i = 0; i < startingIndex; i++)
            PackText[startingIndex - 1 - i].text = PackList[Mathf.Abs(PackList.Count - 1 - i) % PackList.Count].Name;
    }
	
    // Update is called once per frame
    void Update()
    {
    }
}
