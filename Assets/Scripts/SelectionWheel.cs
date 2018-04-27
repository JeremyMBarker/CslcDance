using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectionWheel : MonoBehaviour
{
    // Text references
    private Text[][] SongText;
    private Text[] PackText;

    // Complete song library
    private List<PackInfo> PackList;

    // selection wheel variables
    private PackInfo selectedPack;
    private int centerIndex;
    private int itemIndex;

    // Use this for initialization
    void Start()
    {
        centerIndex = 4;    // index of center item in wheel
        itemIndex = 0;      // index of item that is in the center
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
        UpdateItemViews();
        PackList = new StepmaniaParser().LoadSongs();
        UpdateItemTexts(0);
    }

    // Update is called once per frame
    void Update()
    {
        // Selecting Something
        if (Input.GetButtonDown("Submit"))
        {
            // select song
            if (selectedPack != null)
            {

            }
            // select pack
            else
            {
                selectedPack = PackList[centerIndex];
                UpdateItemViews();
            }
        }

        // Canceling or going back
        if (Input.GetButtonDown("Cancel"))
        {
        }

        // Scroll Up
        if (Input.GetButtonDown("Up"))
            UpdateItemTexts(1);

        // Scroll Down
        if (Input.GetButtonDown("Down"))
            UpdateItemTexts(-1);
    }

    /// <summary>
    /// Updates the item views by hiding/showing song/pack texts
    /// </summary>
    private void UpdateItemViews()
    {
        foreach (var texts in SongText)
            foreach (var text in texts)
                text.gameObject.SetActive(selectedPack != null);   
        foreach (var text in PackText)
            text.gameObject.SetActive(selectedPack == null);
    }

    /// <summary>
    /// Updates the text within item views.
    /// </summary>
    /// <param name="amount">Amount.</param>
    private void UpdateItemTexts(int amount)
    {
        // Update pack names
        if (selectedPack == null)
        {
            // update item index
            if (itemIndex + amount < 0)
                itemIndex = PackList.Count - itemIndex + amount;
            else
                itemIndex = (itemIndex + amount) % PackList.Count;
            for (var i = 0; i < PackText.Length; i++)
                PackText[i].text = PackList[(int)Mathf.Repeat(PackList.Count - centerIndex + i + itemIndex, PackList.Count)].Name;
        }
        // Update song titles, artist
        else
        {
        }
    }
}