using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerAnimator : MonoBehaviour
{
    [SerializeField] Trigger trigger;
    [SerializeField] Animator animator;

    private void Awake()
    {
        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }
        if (trigger == null)
        {
            trigger = GetComponent<Trigger>();
        }
        if (trigger != null)
        {
            trigger.OnActivate += TurnOn;
            trigger.OnDeactivate += TurnOff;
        }
    }

    private void TurnOn()
    {
        if (animator != null)
        {
            animator.Play("TurningOn");
        }
    }

    private void TurnOff()
    {
        if (animator != null)
        {
            animator.Play("TurningOff");
        }
    }
}
