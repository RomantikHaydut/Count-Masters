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
        PlayerController playerController = FindObjectOfType<PlayerController>();
        if (sum)
        {
            playerController.CreateRunner2(myValue);
        }
        else if (multiplier)
        {
            playerController.CreateRunner2(myValue * playerController.activePlayerlist.Count);
        }
        else if (minus)
        {
            playerController.DestroyRunners(myValue);
        }
        else if (divide)
        {
            playerController.DestroyRunners(playerController.activePlayerlist.Count - (playerController.activePlayerlist.Count / myValue));
        }
       
    }
    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.gameObject.CompareTag("Stickman"))
    //    {
    //        MyFunction();
    //        //Component[] options = transform.parent.gameObject.GetComponentsInChildren(typeof(OptionController));
    //        //foreach (OptionController option in options)
    //        //{
    //        //    if (option != this)
    //        //    {
    //        //        option.enabled = false;
    //        //    }
    //        //}

    //        transform.parent.GetComponent<Options>().optionController1.enabled = false;
    //        transform.parent.GetComponent<Options>().optionController2.enabled = false;
    //        gameObject.SetActive(false);

    //    }
    //}

  
}
