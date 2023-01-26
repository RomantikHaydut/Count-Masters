using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetecter : MonoBehaviour
{
    public List<GameObject> enemyControllersList;
    [SerializeField] private GameObject area;
    private PlayerController playerController;
    private UIManager uiManager;

    private void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
        uiManager = FindObjectOfType<UIManager>();
        for (int i = 0; i < transform.childCount; i++)
        {
            enemyControllersList.Add(transform.GetChild(i).transform.gameObject);
        }
        DisplayEnemyCount();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerController.rush = false;
            for (int i = 0; i < enemyControllersList.Count; i++)
            {
                enemyControllersList[i].GetComponent<EnemyController>().attack = true; // for enemy action
                enemyControllersList[i].tag = "Enemy";
            }
            StartCoroutine(StartFight());
        }
    }

    public void DisplayEnemyCount()
    {
        uiManager.DisplayText(transform.parent.gameObject, enemyControllersList.Count.ToString());
    }

    public void DestroyEnemy(GameObject enemy)
    {
        enemyControllersList.Remove(enemy);
        DisplayEnemyCount();
        if (enemyControllersList.Count == 0)
        {
            // Finish
        }
    }

    private IEnumerator StartFight()
    {
        float areaGrowSpeed = 1f;
        float startScaleX = area.transform.localScale.x;
        float maxScaleX = startScaleX * 2;
        while (true)
        {
            yield return null;
            if (area.transform.localScale.x < maxScaleX)
                area.transform.localScale += Vector3.one * Time.deltaTime * areaGrowSpeed;
            else
            {
                StartCoroutine(FinishFight());
                yield break;
            }

            if (enemyControllersList.Count == 0)
            {
                StartCoroutine(FinishFight());
                yield break;
            }
        }
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
                playerController.rush = true;
                yield break;
            }

            yield return null;
            timer += Time.deltaTime * finishSpeed;
            area.transform.localScale = Vector3.Lerp(startScale, Vector3.zero, timer);
        }
    }
}
