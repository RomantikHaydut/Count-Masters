using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingDestroyer : DestroyerBase
{
    private float boundry;

    private bool isGoingLeft;

    [SerializeField] private float moveSpeed = 2;

    [SerializeField] private float rotateSpeed = 1.5f;

    private void Start()
    {
        boundry = GameObject.FindGameObjectWithTag("Ground").GetComponent<BoxCollider>().bounds.size.x / 2;
    }
    void Update()
    {
        ControlDirection();
        Movement();
        Rotation();
    }

    private void Movement()
    {
        if (isGoingLeft)
            transform.position -= Vector3.right * Time.deltaTime * moveSpeed;
        else
            transform.position += Vector3.right * Time.deltaTime * moveSpeed;
    }

    private void ControlDirection()
    {
        if (isGoingLeft)
        {
            if (transform.position.x < -boundry)
            {
                isGoingLeft = false;
            }
        }
        else
        {
            if (transform.position.x > boundry)
            {
                isGoingLeft = true;
            }
        }
    }

    private void Rotation()
    {
        int rotateWay = isGoingLeft ? -1 : 1;
        transform.Rotate(Vector3.up * 360 * Time.deltaTime * rotateSpeed * rotateWay);
    }
}
