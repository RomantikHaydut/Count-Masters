using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishStair : MonoBehaviour
{
    private GameObject focalPoint;
    private PlayerController playerController;
   private void Awake()
    {
        playerController = FindObjectOfType<PlayerController>();
        focalPoint = GameObject.FindGameObjectWithTag("FocalPoint");
        RandomColor();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Stickman"))
        {
            focalPoint.transform.position = new Vector3(focalPoint.transform.position.x, transform.position.y, focalPoint.transform.position.z);
            other.gameObject.transform.parent = null;
            //other.gameObject.transform.position = transform.position;
            playerController.finishedPlayerlist.Add(other.gameObject);
            if (playerController.finishedPlayerlist.Count >= playerController.activePlayerlist.Count)
            {
                Camera.main.GetComponent<CameraController>().enabled = false;
            }
        }
    }


    private void RandomColor()
    {
        Color randomColor = new Color(Random.value, Random.value, Random.value);
        GetComponent<MeshRenderer>().material.color = randomColor;
    }
}
