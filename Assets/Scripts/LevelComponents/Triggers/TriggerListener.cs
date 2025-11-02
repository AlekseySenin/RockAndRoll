using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerListener : MonoBehaviour
{
    [SerializeField] List <Trigger> triggers;

    protected virtual void Awake()
    {
        foreach (var item in triggers)
        {
            item.OnActivate += TriggerOn;
            item.OnDeactivate += TriggerOff;
        }
    }

    protected virtual void TriggerOn()
    {
    }

    protected virtual void TriggerOff()
    {
    }
}
