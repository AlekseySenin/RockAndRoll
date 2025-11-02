using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Trigger : MonoBehaviour
{
    public Action OnActivate;
    public Action OnDeactivate;
    [SerializeField] protected float enterCooldownTime;
    [SerializeField] protected float reliaseDelay;
    [SerializeField] protected float exitCooldownTime;
    [SerializeField] protected bool realTimeDelay;
    protected bool canEnter = true;
    protected bool canExit = true;
    public bool isPressed { get; private set; }
    protected int collisionCount = 0;

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerChar>() && canEnter)
        {
            if (collisionCount <= 0)
            {
                collisionCount = 0;
                PlayerEnter();
            }
            collisionCount++;
        }
    }

    protected virtual void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerChar>() && canExit)
        {
            collisionCount--;
            if (collisionCount == 0)
            {
                StartCoroutine(ExitDelay());
            }
        }
    }

    private IEnumerator ExitDelay()
    {
        if (!realTimeDelay)
        {
            yield return new WaitForSeconds(reliaseDelay);
        }
        else
        {
            yield return new WaitForSecondsRealtime(reliaseDelay);
        }
        PlayerExit();
    }

    protected virtual void PlayerEnter()
    {
        isPressed = true;
        OnActivate?.Invoke();
        canEnter = false;
        StartCoroutine(RessetEnter());
    }

    protected virtual void PlayerExit()
    {
        isPressed = true;
        OnDeactivate?.Invoke();
        canExit = false;
        StartCoroutine(RessetExit());
    }

    protected virtual IEnumerator RessetEnter()
    {
        if (!realTimeDelay)
        {
            yield return new WaitForSeconds(reliaseDelay);
        }
        else
        {
            yield return new WaitForSecondsRealtime(reliaseDelay);
        }
        canEnter = true;
    }

    protected virtual IEnumerator RessetExit()
    {
        if (!realTimeDelay)
        {
            yield return new WaitForSeconds(reliaseDelay);
        }
        else
        {
            yield return new WaitForSecondsRealtime(reliaseDelay);
        }
        canExit = true;
    }
}
