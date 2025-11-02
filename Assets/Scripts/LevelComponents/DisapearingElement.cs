using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisapearingElement : Trigger
{
    [SerializeField] GameObject obj;
    [SerializeField] bool showOnStart;
    [SerializeField] float hideTime;
    [SerializeField] float showTime;
    [SerializeField] float startDelay;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DelayedStart());
    }

    IEnumerator Show()
    {
        OnActivate?.Invoke();
        if (obj != null)
        {
            obj.SetActive(true);
        }
        yield return new WaitForSeconds(hideTime);
        StartCoroutine(Hide());
    }
    IEnumerator Hide()
    {
        OnDeactivate?.Invoke();
        if (obj != null)
        {
            obj.SetActive(false);
        }
        yield return new WaitForSeconds(showTime);
        StartCoroutine(Show());
    }

    IEnumerator DelayedStart()
    {
        yield return new WaitForSeconds(startDelay);

        if (showOnStart)
        {
            StartCoroutine(Show());
        }
        else
        {
            StartCoroutine(Hide());
        }
    }
}
