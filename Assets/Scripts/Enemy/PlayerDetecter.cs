using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetecter : MonoBehaviour
{
    public List<GameObject> enemyControllersList;
    [SerializeField] private GameObject area;
    private PlayerMovement playerMovement;
    private UIManager uiManager;

    private void Start()
    {
        playerMovement = FindObjectOfType<PlayerMovement>();
        uiManager = FindObjectOfType<UIManager>();
        for (int i = 0; i < transform.childCount; i++)
        {
            enemyControllersList.Add(transform.GetChild(i).transform.gameObject);
        }
        DisplayEnemyCount();
    }

    public void DisplayEnemyCount()
    {
        uiManager.DisplayText(transform.parent.gameObject, enemyControllersList.Count.ToString());
    }

    private IEnumerator StartFight()
    {
        float areaGrowSpeed = 1f;
        float startScaleX = area.transform.localScale.x;
        float maxScaleX = startScaleX * 2;
        Vector3 areGrowScale = new Vector3(1, 0, 1);
        while (true)
        {
            yield return null;
            if (area.transform.localScale.x < maxScaleX)
                area.transform.localScale += areGrowScale * Time.deltaTime * areaGrowSpeed;

            if (CheckFightIsOver())
            {
                StartCoroutine(FinishFight());
                yield break;
            }
        }
    }

    private bool CheckFightIsOver()
    {
        if (enemyControllersList.Count == 0)
            return true;
        else
            return false;
    }

    private IEnumerator FinishFight()
    {
        float timer = 0;
        float finishSpeed = 4f;
        Vector3 startScale = area.transform.localScale;
        while (true)
        {
            if (timer >= 1)
            {
                transform.parent.gameObject.SetActive(false);
                PlayerMagnet playerMagnet = FindObjectOfType<PlayerMagnet>();
                playerMagnet.OpenMagnet();
                playerMovement.StopFight();
                yield break;
            }

            yield return null;
            timer += Time.deltaTime * finishSpeed;
            area.transform.localScale = Vector3.Lerp(startScale, Vector3.zero, timer);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            for (int i = 0; i < enemyControllersList.Count; i++)
            {
                enemyControllersList[i].GetComponent<EnemyController>().attack = true; // for enemy action
                enemyControllersList[i].tag = "Enemy";
            }

            playerMovement.RunToEnemy(transform);
            StartCoroutine(StartFight());
        }
    }
}
