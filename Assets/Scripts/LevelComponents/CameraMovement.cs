using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    //[SerializeField] Transform target;
    [SerializeField] float movementSpeed;
    [SerializeField] float minSize;
    [SerializeField] float maxSize;
    [SerializeField] float distanceToPlayer;
    [SerializeField] float zoomSpeed;
    [SerializeField] List <Transform> targets;
    [SerializeField] Vector3 offset;
    private int targetIndex;
    public static string TargetName;



    private void Awake()
    {
        Player.OnMovementStart += StartFollowingPlayer;
    }

    private void OnDestroy()
    {
        Player.OnMovementStart -= StartFollowingPlayer;
    }

    public void SwichTarget()
    {
        if (targetIndex< targets.Count - 1)
        {
            targetIndex++;
        }
        else
        {
            targetIndex = 0;
        }
        TargetName = targets[targetIndex].name;
    }

    private void Start()
    {
        TargetName = targets[targetIndex].name;
        OptionsPopUp.OnCameraTarhetChsnged += SwichTarget;
    }


    private void StartFollowingPlayer()
    {
        StartCoroutine(FollowTarget());
    }

    private IEnumerator FollowTarget()
    {
        yield return null;
        if (transform.position != targets[targetIndex].position)
        {
            float z =0;
            Vector3 relativePosition = targets[targetIndex].position - transform.position;
            if (Mathf.Abs(distanceToPlayer - relativePosition.z) > 10.5f)
            {
                if (distanceToPlayer < relativePosition.z)
                {
                    z = zoomSpeed * Time.deltaTime;
                }
                else if (distanceToPlayer > relativePosition.z)
                {
                    z = -zoomSpeed * Time.deltaTime;
                }
            }
            transform.position += (new Vector3(relativePosition.x, relativePosition.y, z) + offset) * movementSpeed*Time.deltaTime;
        }
        StartCoroutine(FollowTarget());
    }

}
