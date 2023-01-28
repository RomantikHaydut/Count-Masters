using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private GameObject[] playerList;
    private GameObject closestPlayer;
    private float distanceToClosestPlayer;
    private float distanceToPlayer;

    private float runSpeed;
    public bool attack;

    private Animator myAnimator;
    private PlayerMagnet playerMagnet;

    private void Awake()
    {
        myAnimator = GetComponent<Animator>();
        playerMagnet = FindObjectOfType<PlayerMagnet>();
    }

    void Start()
    {
        runSpeed = GameManager.Instance.runSpeed;
        closestPlayer = null;
        attack = false;
        myAnimator.enabled = false;
    }


    void Update()
    {
        if (attack)
        {
            myAnimator.enabled = true;
            FindTheClosestPlayer();
            if (closestPlayer != null)
            {
                transform.LookAt(closestPlayer.transform.position);
                transform.position += transform.forward * Time.deltaTime * runSpeed;
            }
        }
    }

    private void FindTheClosestPlayer()
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

    private void Destroy()
    {
        var playerDetecter = transform.parent.GetComponent<PlayerDetecter>();
        playerDetecter.enemyControllersList.Remove(gameObject);
        transform.gameObject.SetActive(false);
        playerDetecter.DisplayEnemyCount();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Stickman"))
        {
            Destroy();
            playerMagnet.DestroyRunner(other.gameObject);

        }
    }
}
