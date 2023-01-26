using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float followSpeed = 1f;
    private Transform player;

    [SerializeField] private Vector3 offset;

    [SerializeField] private float finishAngle = 65f;
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("FocalPoint").transform;
        offset = player.position - transform.position;
    }

    void LateUpdate()
    {
        FollowTarget();
        if (Input.GetKeyDown(KeyCode.O))
        {
            Rotate();
        }

    }

    private void FollowTarget()
    {
        transform.position = Vector3.Lerp(transform.position, player.position - offset, Time.deltaTime * followSpeed);
    }

    public void Rotate()
    {
        StartCoroutine(Rotate_Coroutine2());
    }


    private IEnumerator Rotate_Coroutine()
    {
        float timer = 0;
        float rotateSpeed = 1f;
        while (true)
        {
            yield return new WaitForEndOfFrame();

            if (timer >= 1)
                yield break;

            transform.RotateAround(player.transform.position, Vector3.up, -finishAngle * Time.deltaTime);
            offset = player.position - transform.position;
            timer += Time.deltaTime * rotateSpeed;
        }
    } 

    private IEnumerator Rotate_Coroutine2()
    {
        Vector3 startOffset = player.position - transform.position;
        Vector3 targetOffset = new Vector3(-startOffset.z, startOffset.y, startOffset.z/2);
        Vector3 startEulerAngles = transform.eulerAngles;
        Vector3 targetEulerAngles = transform.eulerAngles;

        float timer = 0;
        float rotateSpeed = 1f;
        while (true)
        {
            yield return new WaitForEndOfFrame();

            if (timer >= 1)
            {
                offset = targetOffset;
                yield break;

            }

            offset = Vector3.Lerp(startOffset, targetOffset, timer);
            transform.LookAt(player);
            timer += Time.deltaTime * rotateSpeed;
        }
    }
}
