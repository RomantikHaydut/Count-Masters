using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishStair : MonoBehaviour
{
    public int myScoreFactor;

    private GameObject focalPoint;

    private PlayerMagnet playerMagnet;

    private UIManager uiManager;
    private void Awake()
    {
        playerMagnet = FindObjectOfType<PlayerMagnet>();
        uiManager = FindObjectOfType<UIManager>();
        focalPoint = GameObject.FindGameObjectWithTag("FocalPoint");
    }

    private void Start()
    {
        uiManager.DisplayText(gameObject, myScoreFactor.ToString() + "X");
        RandomColor();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Stickman"))
        {
            focalPoint.transform.position = new Vector3(focalPoint.transform.position.x, transform.position.y, focalPoint.transform.position.z);
            other.gameObject.transform.parent = null;
            playerMagnet.finishedPlayerlist.Add(other.gameObject);

            if (playerMagnet.finishedPlayerlist.Count >= playerMagnet.activePlayerlist.Count) // Last runner triggered / Finish Game.
            {
                ScoreManager.Instance.CalculateScore(myScoreFactor);
                GameManager.Instance.WinGame();
            }
        }
    }


    private void RandomColor()
    {
        Color randomColor = new Color(Random.value, Random.value, Random.value);
        GetComponent<MeshRenderer>().material.color = randomColor;
    }
}
