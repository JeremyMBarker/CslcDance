using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivePlayerController : MonoBehaviour
{
    private static bool isPlayer1Active;
    public bool IsPlayer1Active { get { return isPlayer1Active; } set { isPlayer1Active = value; } }
    private static bool isPlayer2Active;
    public bool IsPlayer2Active { get { return isPlayer2Active; } set { isPlayer2Active = value; } }
}