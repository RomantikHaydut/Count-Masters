using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunnerController : MonoBehaviour
{
    public bool attack = false;

    [SerializeField] private float runSpeed = 1f;
    public void StartAttack(Transform boss) => StartCoroutine(Attack_Coroutine(boss));

    public void StopAttack()
    {
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        attack = false;
    }
    private IEnumerator Attack_Coroutine(Transform boss)
    {
        attack = true;
        while (true)
        {
            if (attack == false)
                yield break;

            yield return null;
            transform.LookAt(boss);
            Vector3 dir = new Vector3(boss.position.x - transform.position.x, 0, boss.position.z - transform.position.z).normalized;
            transform.position += dir * Time.deltaTime * runSpeed;

        }
    }
}
