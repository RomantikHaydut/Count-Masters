using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunnerController : MonoBehaviour
{
    public CharacterController characterController;

    public bool comeFinish = false;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        characterController.enabled = false;
    }

    void Update()
    {
        if (comeFinish)
        {
            Movement();
        }
    }

    private void Movement()
    {
        characterController.Move(transform.forward * Time.deltaTime * 5f);
        if (characterController.collisionFlags == CollisionFlags.Sides)
        {
            this.enabled = false;
        }
    }
}
