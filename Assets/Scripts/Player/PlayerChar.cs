using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerChar : MonoBehaviour
{
    [SerializeField] private SideTrigger topTrigger;
    [SerializeField] private SideTrigger bottomTrigger;

    public bool canGrow { get { return topTrigger.objectsTouched == 0; } }
    public bool canShrink { get { return bottomTrigger.objectsTouched == 0; } }

    [SerializeField] private float yAdj = 0.05f;
    private List<Collision2D> collision2Ds = new List<Collision2D>(); 

    public Action OnPressed;
    public Vector3 RelativePosition (Vector3 toPosition)
    {
        return transform.position - toPosition;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.contacts[0].point.y<transform.position.y + yAdj)
        {
            collision2Ds.Add(collision);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        collision2Ds.Remove(collision);

    }


    public bool IsStanding()
    {
        return (collision2Ds.Count > 0);      
    }
}
