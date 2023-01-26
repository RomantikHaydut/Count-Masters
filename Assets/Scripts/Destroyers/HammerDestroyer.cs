using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HammerDestroyer : DestroyerBase
{
    [SerializeField] private float speedStartDistance = 5f;
    [SerializeField] private float speedMultiplier = 1f;
    [SerializeField] private float maxSpeed = 5f;
    private Animator anim;
    void Start()
    {
        anim = transform.parent.GetComponent<Animator>();
    }

    void Update()
    {
        GiveSpeed();
    }

    private void GiveSpeed()
    {
        if (IsPlayerClose())
            StartCoroutine(GiveSpeed_Coroutine());
    }

    private bool IsPlayerClose()
    {
        float distance = Mathf.Abs(playerController.gameObject.transform.position.z - transform.position.z);
        if (distance < speedStartDistance)
            return true;
        else
            return false;
    }

    private IEnumerator GiveSpeed_Coroutine()
    {
        this.enabled = false;
        while (true)
        {
            if (anim.speed > maxSpeed)
                yield break;

            yield return null;
            anim.speed += speedMultiplier * Time.deltaTime;
        }
    }
}
