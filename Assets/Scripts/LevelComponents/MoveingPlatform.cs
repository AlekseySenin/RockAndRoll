using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class MoveingPlatform : TriggerListener
{
    [SerializeField] private Platform platform;
    [SerializeField] private List<Transform> movepoints;
    private int selectedPointIndex;
    [SerializeField] private bool singleMove;
    [SerializeField] private bool avakeOnStart;
    [SerializeField] private float speed;
    [SerializeField] private float waitTime;

    private bool isMoveing = false;

    void Start()
    {
        if (movepoints.Count > 0)
        {
            selectedPointIndex = 0;
            if (avakeOnStart)
            {
                StartMoveing();
            }
        }
    }

    private void StartMoveing()
    {
        if (!isMoveing)
        {
            StartCoroutine(Move());
        }
    }

    IEnumerator Move()
    {
        Vector3 moveVector = (movepoints[selectedPointIndex].position - platform.transform.position).normalized * speed * Time.deltaTime;
        yield return null;
        if((movepoints[selectedPointIndex].position - platform.transform.position).magnitude > moveVector.magnitude)
        {
            platform.transform.position += moveVector;
            platform.OnMove?.Invoke(moveVector);
            isMoveing = true;
            StartCoroutine(Move());
        }
        else
        {
            platform.OnMove?.Invoke(movepoints[selectedPointIndex].position - platform.transform.position);
            platform.transform.position = movepoints[selectedPointIndex].position;
            SelectNextPoint();
            isMoveing = false;
            if (!singleMove)
            {
                StartCoroutine(Wait());
            }
        }
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(waitTime);
        StartCoroutine(Move());
    }

    void SelectNextPoint()
    {
        if(selectedPointIndex == movepoints.Count - 1)
        {
            selectedPointIndex = 0;
        }
        else
        {
            selectedPointIndex++;
        }
    }

    protected override void TriggerOn()
    {
        base.TriggerOn();
        StartMoveing();
    }

    protected override void TriggerOff()
    {
        isMoveing = false;
        base.TriggerOff();
        StopAllCoroutines();
    }
}
