using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public bool isGameStarted = false;
    public bool canMove = true;
    public bool canHorizontalMove = true;
    public bool fighting = false;

    [SerializeField] private float forwardSpeed = 1f;
    [SerializeField] private float horizontalSpeed = 1f;
    public float boundryX;
    private void Awake()
    {
        boundryX = GameObject.FindGameObjectWithTag("Ground").GetComponent<BoxCollider>().bounds.size.x / 2;
    }
    void Update()
    {
        if (isGameStarted)
        {
            if (canMove)
            {
                Movement();
            }
        }
    }

    #region Movement
    private void Movement()
    {
        transform.position += transform.forward * Time.deltaTime * forwardSpeed;

        if (canHorizontalMove)
        {
            float horizontalInput = Input.GetAxis("Horizontal");

            transform.position += transform.right * Time.deltaTime * horizontalInput * horizontalSpeed;
            transform.position = new Vector3(Mathf.Clamp(transform.position.x, -boundryX, boundryX), transform.position.y, transform.position.z);
        }
    }

    public void IncreaseSpeed(float multiplier)
    {
        forwardSpeed *= multiplier;
    }
    #endregion

    #region EnemyFight
    public void StopFight() { canMove = true; fighting = false; }
    public void RunToEnemy(Transform enemy) => StartCoroutine(RunEnemy_Coroutine(enemy));
    private IEnumerator RunEnemy_Coroutine(Transform enemy)
    {
        canMove = false;
        fighting = true;
        float moveSpeed = 3f;
        Vector3 dir = new Vector3(enemy.position.x - transform.position.x, 0, enemy.position.z - transform.position.z).normalized;
        while (true)
        {
            if (!fighting)
                yield break;

            yield return null;
            transform.position += dir * Time.deltaTime * moveSpeed;
        }
    }
    #endregion
}
