using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] PlayerType playerType;

    GameObject[] playerList;
    GameObject closestPlayer;
    float distanceToClosestPlayer;
    float distanceToPlayer;

    public bool attack;

    Animator myAnimator;
    PlayerController playerController;

    void Start()
    {
        closestPlayer = null;
        attack = false;
        myAnimator = GetComponent<Animator>();
        myAnimator.enabled = false;
        playerController = FindObjectOfType<PlayerController>();
    }


    void Update()
    {
        if (attack)
        {
            myAnimator.enabled = true;
            FindTheClosestPlayer();
            transform.LookAt(closestPlayer.transform.position);
            transform.position += transform.forward * Time.deltaTime * playerType.runSpeed / 3;
        }
    }

    void FindTheClosestPlayer()
    {
        distanceToClosestPlayer = Mathf.Infinity;
        closestPlayer = null;
        playerList = GameObject.FindGameObjectsWithTag("Stickman");
        for (int i = 0; i < playerList.Length; i++)
        {
            distanceToPlayer = (playerList[i].transform.position - transform.position).sqrMagnitude;
            if (distanceToPlayer < distanceToClosestPlayer)
            {
                distanceToClosestPlayer = distanceToPlayer;
                closestPlayer = playerList[i];
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Stickman"))
        {
            transform.gameObject.SetActive(false);
            playerController.DestroyRunner(other.gameObject);
            transform.parent.GetComponent<PlayerDetecter>().enemyControllersList.Remove(gameObject);
            transform.parent.GetComponent<PlayerDetecter>().DisplayEnemyCount();
        }
    }
}
