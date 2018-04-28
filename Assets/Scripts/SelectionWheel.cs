using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Threading;

public class SelectionWheel : MonoBehaviour
{
    public RawImage Banner;

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
        UpdateItemInfo();
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
                selectedPack = PackList[itemIndex];
                UpdateItemViews();
                itemIndex = 0;
                UpdateItemInfo();
            }
        }

        // Canceling or going back
        if (Input.GetButtonDown("Cancel"))
        {
            // select song
            if (selectedPack != null)
            {
                itemIndex = PackList.IndexOf(selectedPack); // set to the previous index
                selectedPack = null;
                UpdateItemViews();
                UpdateItemInfo();
            }
            // select pack
            else
            {
            }
        }

        // Scroll Up
        if (Input.GetButtonDown("Up"))
            UpdateItemInfo(1);

        // Scroll Down
        if (Input.GetButtonDown("Down"))
            UpdateItemInfo(-1);

        // Sample song music
        if (Input.GetButtonDown("Sample"))
        {
            if (selectedPack != null)
            {
                var s = selectedPack.Songs[itemIndex];
                float _sampleStart, _sampleLength;
                var success = float.TryParse(s.SampleStart, out _sampleStart);
                success = success & float.TryParse(s.SampleLength, out _sampleLength);
                if (success)
                    SampleSong(s.Path + '\\' + s.Music, _sampleStart, _sampleLength);
            }            
        }
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
    /// Updates the item texts and banner image.
    /// Can specify how much to increment the item index.
    /// </summary>
    /// <param name="indexInc">Index increment amount.</param>
    private void UpdateItemInfo(int indexInc = 0)
    {
        // Update pack names
        if (selectedPack == null)
        {
            // update item index
            if (itemIndex + indexInc < 0)
                itemIndex = PackList.Count - itemIndex + indexInc;
            else
                itemIndex = (itemIndex + indexInc) % PackList.Count;
            for (var i = 0; i < PackText.Length; i++)
                PackText[i].text = PackList[(int)Mathf.Repeat(PackList.Count - centerIndex + i + itemIndex, PackList.Count)].Name;
        }
        // Update song titles, artist
        else
        {
            // update item index
            if (itemIndex + indexInc < 0)
                itemIndex = selectedPack.Songs.Count - itemIndex + indexInc;
            else
                itemIndex = (itemIndex + indexInc) % selectedPack.Songs.Count;
            for (var i = 0; i < SongText.Length; i++)
            {
                var song = selectedPack.Songs[(int)Mathf.Repeat(selectedPack.Songs.Count - centerIndex + i + itemIndex, selectedPack.Songs.Count)];
                SongText[i][0].text = song.Title;
                SongText[i][1].text = song.Artist;
            }
            var bannerPath = selectedPack.Songs[itemIndex].Path + '\\' + selectedPack.Songs[itemIndex].Banner;
            Texture2D tex = null;
            if (File.Exists(bannerPath))
            {
                tex = new Texture2D(2, 2);
                tex.LoadImage(File.ReadAllBytes(bannerPath));
            }
            Banner.texture = tex;
        }
    }

    /// <summary>
    /// Samples the song.
    /// </summary>
    /// <param name="songPath">Song path.</param>
    /// <param name="startPoint">Start point.</param>
    /// <param name="duration">Duration.</param>
    private void SampleSong(string songPath, float startPoint, float duration)
    {

        var path = Directory.GetCurrentDirectory() + songPath.Trim('.');
        if (File.Exists(songPath))
        {
            var source = GetComponent<AudioSource>();
            var audioloader = new WWW(path);
            var clip = audioloader.GetAudioClip(false);
            // Busy waiting is bad. This should definitely be fixed
            while (clip.loadState != AudioDataLoadState.Loaded)
                ;
            source.clip = clip;
            source.Play();
        }
    }
}