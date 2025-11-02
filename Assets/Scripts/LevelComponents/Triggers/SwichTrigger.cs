using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwichTrigger : Trigger
{
    [SerializeField] private bool resseting;
    private bool isDown;
    protected override void PlayerExit()
    {
        
    }

    protected override void PlayerEnter()
    {
        if (!isDown)
        {
            OnActivate?.Invoke();
        }
        else
        {
            OnDeactivate?.Invoke();
        }
        canEnter = false;
        StartCoroutine(RessetEnter());
    }
}
