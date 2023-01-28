using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float followSpeed = 1f;

    private Transform target;

    [SerializeField] private Vector3 offset;


    private void Awake()
    {
        target = GameObject.FindGameObjectWithTag("FocalPoint").transform;
        offset = target.position - transform.position;
    }

    void LateUpdate()
    {
        FollowTarget();
    }

    private void FollowTarget()
    {
        transform.position = Vector3.Lerp(transform.position, target.position - offset, Time.deltaTime * followSpeed);
    }

    public void ChangeTarget(Transform newTarget) => target = newTarget;

    public void Rotate()
    {
        StartCoroutine(Rotate_Coroutine());
    }

    private IEnumerator Rotate_Coroutine()
    {
        Vector3 startOffset = target.position - transform.position;
        Vector3 targetOffset = new Vector3(-startOffset.z * 1.3f, startOffset.y / 1.3f, startOffset.z / 1.5f);

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
            transform.LookAt(target);
            timer += Time.deltaTime * rotateSpeed;
        }
    }
}
