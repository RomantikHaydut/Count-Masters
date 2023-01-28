using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossController : MonoBehaviour
{

    [SerializeField] private float rotateSpeed = 2f;

    [SerializeField] private GameObject goldChest;

    private Animator anim;

    private bool dead = false;

    private bool sleeping = true;

    private bool attacking = false;

    private bool waitingForFight = false;

    private PlayerMagnet playerMagnet;


    [SerializeField] private Image healthBar;

    [SerializeField] private List<BoxCollider> weaponColliders = new List<BoxCollider>();

    [SerializeField] private float attackRange = 0.3f;

    public float maxHealth;

    public float health;

    public float damage = 1f;


    private void Awake()
    {
        playerMagnet = FindObjectOfType<PlayerMagnet>();
        anim = GetComponent<Animator>();
        health = maxHealth;
    }

    private void Update()
    {
        if (!sleeping)
            if (waitingForFight)
                CheckPlayersForStartAttack();
    }


    public void GetDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            health = 0;
            healthBar.fillAmount = 0;
            Die();
            enabled = false;
            return;
        }
        healthBar.fillAmount = (float)health / maxHealth;
    }

    private void StartFight()
    {
        if (sleeping)
        {
            sleeping = false;
            FindObjectOfType<CameraController>().ChangeTarget(transform);
            FindObjectOfType<PlayerMovement>().canMove = false;
            FindObjectOfType<PlayerMagnet>().StartBossFight(transform);
            StartCoroutine(LookClosestPlayer_Coroutine());
        }
    }

    private Transform FindTheClosestPlayer()
    {
        float distanceToClosestPlayer = Mathf.Infinity;
        Transform closestPlayer = null;
        GameObject[] playersArray = GameObject.FindGameObjectsWithTag("Stickman");
        if (playersArray.Length >= 1)
        {
            for (int i = 0; i < playersArray.Length; i++)
            {
                float distanceToPlayer = (playersArray[i].transform.position - transform.position).sqrMagnitude;
                if (distanceToPlayer < distanceToClosestPlayer)
                {
                    distanceToClosestPlayer = distanceToPlayer;
                    closestPlayer = playersArray[i].transform;
                }
            }

            return closestPlayer;
        }
        else
            return null;
    }

    private float DistanceToClosestPlayer()
    {
        Transform closestPlayer = FindTheClosestPlayer();
        float distance = Vector3.Distance(new Vector3(transform.position.x, 0, transform.position.z), new Vector3(closestPlayer.transform.position.x, 0, closestPlayer.transform.position.z));
        return distance;
    }

    private bool NearThePlayer()
    {
        if (DistanceToClosestPlayer() <= attackRange)
            return true;
        else
            return false;
    }
    private IEnumerator LookClosestPlayer_Coroutine()
    {
        while (true)
        {
            yield return null;
            if (dead || FindTheClosestPlayer() == null)
                yield break;

            Vector3 targetDirection = (FindTheClosestPlayer().position - transform.position).normalized;
            Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, Time.deltaTime * rotateSpeed);
        }
    }

    private void CheckPlayersForStartAttack()
    {
        if (!attacking)
        {
            if (NearThePlayer())
            {
                StartAttack();
            }
        }

    }
    public void BossWin()
    {
        anim.SetTrigger("jump");
        this.enabled = false;
    }
    private void WaitPlayers()
    {
        if (!waitingForFight)
        {
            anim.SetTrigger("wait");
            waitingForFight = true;
        }
    }
    private void StartAttack()
    {
        if (!attacking)
        {
            attacking = true;
            anim.SetTrigger("attack_01");
            waitingForFight = false;
            playerMagnet.canMagnet = false;
        }
    }


    private void Die()
    {
        if (!anim.GetBool("dead"))
        {
            dead = true;
            anim.SetBool("dead", true);
            anim.SetTrigger("die");
            CloseWeaponsColliders();
            playerMagnet.FinishBossFight();
            OpenGoldChest();
        }
    }

    private void OpenGoldChest()
    {
        goldChest.GetComponent<Animator>().SetTrigger("open");
    }

    #region  Animation events
    private void OpenWeaponsColliders() // Calling this every attack animation.
    {
        transform.LookAt(FindTheClosestPlayer());
        for (int i = 0; i < weaponColliders.Count; i++)
        {
            weaponColliders[i].enabled = true;
        }

    }

    private void CloseWeaponsColliders() // Calling this every attack animation.
    {
        for (int i = 0; i < weaponColliders.Count; i++)
            weaponColliders[i].enabled = false;
    }

    private void Death() // calling in die animation
    {
        FindObjectOfType<PlayerMovement>().canMove = false;
        ScoreManager.Instance.CalculateScore(1);
        GameManager.Instance.WinGame();
    }



    #endregion

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            StartFight();
            WaitPlayers();
        }
    }


}
