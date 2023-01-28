using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossWeapon : MonoBehaviour
{
    private PlayerMagnet playerMagnet;

    private BossController bossController;

    private void Awake()
    {
        bossController = FindObjectOfType<BossController>();
        playerMagnet = FindObjectOfType<PlayerMagnet>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Stickman"))
        {
            playerMagnet.DestroyRunner(other.gameObject);
            bossController.GetDamage(1);
        }
    }
}
