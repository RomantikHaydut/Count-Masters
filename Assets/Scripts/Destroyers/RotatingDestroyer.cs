using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingDestroyer : DestroyerBase
{
    [SerializeField] private float rotateSpeed;

    private Transform parent;
    private void Start()
    {
        parent = transform.parent;
    }

    void Update()
    {
        Movement();
    }

    private void Movement()
    {
        transform.RotateAround(parent.position, Vector3.up, 360 * Time.deltaTime *rotateSpeed);
    }
}
