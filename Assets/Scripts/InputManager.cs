using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    // Use this for initialization
    void Start()
    {
        var joyNames = Input.GetJoystickNames();
        for (var i = 0; i < joyNames.Length; i++)
            Debug.Log(i + ": " + joyNames[i]);
    }
	
    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("P1_Left"))
            Debug.Log("P1_Left");
        if (Input.GetButtonDown("P2_Left"))
            Debug.Log("P2_Left");
        if (Input.GetButtonDown("P1_Down"))
            Debug.Log("P1_Down");
        if (Input.GetButtonDown("P2_Down"))
            Debug.Log("P2_Down");
        if (Input.GetButtonDown("P1_Up"))
            Debug.Log("P1_Up");
        if (Input.GetButtonDown("P2_Up"))
            Debug.Log("P2_Up");
        if (Input.GetButtonDown("P1_Right"))
            Debug.Log("P1_Right");
        if (Input.GetButtonDown("P2_Right"))
            Debug.Log("P2_Right");
        if (Input.GetButtonDown("P1_Cross"))
            Debug.Log("P1_Cross");
        if (Input.GetButtonDown("P1_Circle"))
            Debug.Log("P1_Circle");
        if (Input.GetButtonDown("P1_Square"))
            Debug.Log("P1_Square");
        if (Input.GetButtonDown("P1_Triangle"))
            Debug.Log("P1_Triangle");
        if (Input.GetButtonDown("P2_Cross"))
            Debug.Log("P2_Cross");
        if (Input.GetButtonDown("P2_Circle"))
            Debug.Log("P2_Circle");
        if (Input.GetButtonDown("P2_Square"))
            Debug.Log("P2_Square");
        if (Input.GetButtonDown("P2_Triangle"))
            Debug.Log("P2_Triangle");
        if (Input.GetButtonDown("P1_Select"))
            Debug.Log("P1_Select");
        if (Input.GetButtonDown("P1_Start"))
            Debug.Log("P1_Start");
        if (Input.GetButtonDown("P2_Start"))
            Debug.Log("P2_Start");
        if (Input.GetButtonDown("P2_Select"))
            Debug.Log("P2_Select");
    }
}
