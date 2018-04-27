using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingBackground : MonoBehaviour
{
    public float scrollSpeed;
    public float tileSize;

    private Vector2 startPosition;

    void Start ()
    {
        startPosition = transform.position;
    }

    void Update ()
    {
        float newPosition = Mathf.Repeat (Time.time * scrollSpeed, tileSize);
        transform.position = startPosition + Vector2.down * newPosition;
    }
}