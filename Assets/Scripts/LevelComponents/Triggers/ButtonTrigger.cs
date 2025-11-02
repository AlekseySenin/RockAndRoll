using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonTrigger : Trigger
{
    protected override void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerChar>())
        {
            collisionCount--;
        }
    }

    protected override IEnumerator RessetEnter()
    {
        yield return new WaitForSeconds(enterCooldownTime);
        PlayerExit();
    }

    protected override IEnumerator RessetExit()
    {
        yield return new WaitForSeconds(exitCooldownTime);
        canExit = true;
        canEnter = true;

    }
}
