using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class SongSampler : MonoBehaviour
{
    private float timer, maxTime, startTime;
    private bool isSampling, isWaiting;
    private AudioSource source;

    // Use this for initialization
    void Start()
    {
        timer = maxTime = startTime = 0.0F;
        isSampling = isWaiting = false;
        source = GetComponent<AudioSource>();
    }
	
    // Fixed update is called once per framerate frame
    void FixedUpdate()
    {
        // start the clip
        if (isWaiting && source.clip.loadState == AudioDataLoadState.Loaded)
        {
            isWaiting = false;
            isSampling = true;
            timer = 0.0F;
            source.time = startTime;
            source.Play();
        }

        // check clip timer
        if (isSampling)
        {
            timer += Time.fixedDeltaTime;
            if (timer >= maxTime)
            {
                source.Stop();
                isSampling = false;
            }
        }
    }


    public void SampleSong(SongInfo song)
    {
        isWaiting = false; // cancel previous opertation        

        var songPath = song.Path + '\\' + song.Music;
        if (!File.Exists(songPath)) // does the song exist
            return;

        // load the song
        float _sampleStart, _sampleLength;
        var success = float.TryParse(song.SampleStart, out _sampleStart);
        success = success & float.TryParse(song.SampleLength, out _sampleLength);
        if (success)
        {
            var audioloader = new WWW(Directory.GetCurrentDirectory() + songPath.Trim('.'));
            // streaming the clip is faster, but throws empty errors (just ignore)
            // the errors are supposedly caused by by failure to read a file
            var clip = audioloader.GetAudioClip(false, true, AudioType.OGGVORBIS);
            startTime = _sampleStart;
            maxTime = _sampleLength;
            source.clip = clip;
            isWaiting = true;
        }
    }
}