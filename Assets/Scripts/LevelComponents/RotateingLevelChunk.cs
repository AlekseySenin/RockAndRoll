using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateingLevelChunk : MonoBehaviour
{
    [SerializeField] private GameObject content;

    public void MoveToPosition(Vector3 position)
    {
        Vector3 relativePosition = transform.position - position;
        transform.position -= relativePosition;
        content.transform.position += relativePosition;
    }
}
