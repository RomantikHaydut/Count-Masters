using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionController : MonoBehaviour
{
    [HideInInspector] public int myValue;

    [HideInInspector] public bool sum;
    [HideInInspector] public bool minus;
    [HideInInspector] public bool divide;
    [HideInInspector] public bool multiplier;


    public void MyFunction()
    {
        PlayerMagnet playerMagnet = FindObjectOfType<PlayerMagnet>();
        if (sum)
        {
            playerMagnet.CreateRunner(myValue);
        }
        else if (multiplier)
        {
            playerMagnet.CreateRunner((myValue * playerMagnet.activePlayerlist.Count) - playerMagnet.activePlayerlist.Count);
        }
        else if (minus)
        {
            playerMagnet.DestroyRunners(myValue);
        }
        else if (divide)
        {
            playerMagnet.DestroyRunners(playerMagnet.activePlayerlist.Count - (playerMagnet.activePlayerlist.Count / myValue));
        }
       
    } 
}
