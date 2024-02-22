using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameData : MonoBehaviour
{
    public static int distanceTraveled = 0;
    
    public static void ResetData()
    {
        distanceTraveled = 0;
    }
}