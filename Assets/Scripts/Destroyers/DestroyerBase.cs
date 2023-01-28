using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyerBase : MonoBehaviour
{
    protected PlayerMagnet playerMagnet;
    private void Awake()
    {
        playerMagnet = FindObjectOfType<PlayerMagnet>();
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Stickman"))
        {
            playerMagnet.DestroyRunner(other.gameObject);
        }
    }
}
