using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SizeChangeTrigger : Trigger
{
    [SerializeField] private bool grow;
    protected override void PlayerEnter()
    {
        base.PlayerEnter();

    }
}
