using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelRotator : TriggerListener
{
    [SerializeField] private float rotationValue;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private RotateingLevelChunk levelChunk;
    [SerializeField] private bool selfCentered = true;
    private float desiredRotation;

    public Vector3 RelativePosition(Vector3 toPosition)
    {
        return transform.position - toPosition;
    }

    private void RotateChunk()
    {
        if (selfCentered)
        {
            levelChunk.MoveToPosition(transform.position);
        }
        desiredRotation = levelChunk.transform.rotation.eulerAngles.z + rotationValue;
        if (desiredRotation < 0)
        {
            desiredRotation += 360;
        }
        StartCoroutine(Rotateing());
    }

    IEnumerator Rotateing()
    {
        yield return null;
        int val = rotationValue > 0 ? 1 : -1;
        float relativeRotationSpeed = rotationSpeed * Time.deltaTime * Time.timeScale * val;
        Debug.Log((Mathf.Abs(levelChunk.transform.rotation.eulerAngles.z - desiredRotation)));
        

        if (!(Mathf.Abs(levelChunk.transform.rotation.eulerAngles.z - desiredRotation) < 10))
        {
            levelChunk.transform.Rotate(0, 0, relativeRotationSpeed);
            StartCoroutine(Rotateing());
        }
        else
        {
            levelChunk.transform.rotation = Quaternion.Euler(0, 0, desiredRotation);
        }

        //    if (levelChunk.transform.rotation.eulerAngles.z < desiredRotation)
        //{
        //    levelChunk.transform.Rotate(new Vector3(0, 0, rotationSpeed * Time.deltaTime * Time.timeScale));
        //    if (levelChunk.transform.rotation.eulerAngles.z < desiredRotation)
        //    {
        //        StartCoroutine(Rotateing());
        //    }
        //    else
        //    {
        //        levelChunk.transform.rotation = Quaternion.Euler(0, 0, desiredRotation);
        //    }
        //}
        //else if (levelChunk.transform.rotation.eulerAngles.z > desiredRotation || (levelChunk.transform.rotation.eulerAngles.z < desiredRotation))
        //{
        //    levelChunk.transform.Rotate(new Vector3(0, 0, -rotationSpeed * Time.deltaTime * Time.timeScale));
        //    if (levelChunk.transform.rotation.eulerAngles.z > desiredRotation)
        //    {
        //        StartCoroutine(Rotateing());
        //    }
        //    else
        //    {
        //        levelChunk.transform.rotation = Quaternion.Euler(0, 0, desiredRotation);
        //    }
        //}
    }

    protected override void TriggerOn()
    {
        RotateChunk();
        base.TriggerOn();
    }
}
