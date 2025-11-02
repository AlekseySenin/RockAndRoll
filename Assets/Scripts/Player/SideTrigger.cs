using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideTrigger : MonoBehaviour
{
    public float objectsTouched { get; private set; }

    private void OnTriggerEnter2D(Collider2D other)
    {
        objectsTouched++;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.contacts[0].point.y < transform.position.y)
        {
            objectsTouched++;

        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
       objectsTouched--;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        objectsTouched--;
    }
}
