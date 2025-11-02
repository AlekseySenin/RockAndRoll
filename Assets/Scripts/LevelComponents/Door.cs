using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : TriggerListener
{
    [SerializeField] Animator animator;


    protected override void Awake()
    {
        base.Awake();
        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }
    }

    protected override void TriggerOn()
    {
        base.TriggerOn();
        if (animator != null)
        {
            animator.Play("TurningOn");
        }
    }
    protected override void TriggerOff()
    {
        base.TriggerOff();
        if (animator != null)
        {
            animator.Play("TurningOff");
        }
    }
}
