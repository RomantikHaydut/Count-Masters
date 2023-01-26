using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyerBase : MonoBehaviour
{
    protected PlayerController playerController;
    private void Awake()
    {
        playerController = FindObjectOfType<PlayerController>();
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Stickman"))
        {
            playerController.DestroyRunner(other.gameObject);
        }
    }
}
