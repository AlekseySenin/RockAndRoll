using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeFrizer : MonoBehaviour
{
    [SerializeField] private float timeScale = 0.2f;
    [SerializeField] private float speedScale = 1.5f;
    [SerializeField] Trigger trigger;
    [SerializeField] float delayTime;
    [SerializeField] private GameObject particleSystem;
    public static bool TimeIsFrozen { get; private set; }
    private void Awake()
    {
        TimeIsFrozen = false;
        Time.timeScale = 1;
        Time.fixedDeltaTime = 0.02f * Time.timeScale;
        if (timeScale == 0)
        {
            timeScale = 0.2f;
        }
        trigger.OnActivate += FrizeTime;
        trigger.OnDeactivate += UnfrizeTime;
    }

    private void OnDestroy()
    {
        Debug.Log(Time.fixedDeltaTime);
        TimeIsFrozen = false;
        Time.timeScale = 1;
        Time.fixedDeltaTime = 0.02f * Time.timeScale;
        trigger.OnActivate -= FrizeTime;
        trigger.OnDeactivate -= UnfrizeTime;
    }

    private void FrizeTime()
    {
        if (!TimeIsFrozen)
        {
            TimeIsFrozen = true;
            StartCoroutine(OnTimeFriz());
        }
    }

    IEnumerator OnTimeFriz()
    {
        GameObject particles = Instantiate(particleSystem);
        particles.transform.position = transform.position;
        yield return new WaitForSeconds(delayTime);
        Player.TurnSpeed *= speedScale;
        Time.timeScale = timeScale;
        Time.fixedDeltaTime = 0.02f * Time.timeScale;
    }

    private void UnfrizeTime()
    {
        TimeIsFrozen = false;
        Player.TurnSpeed /= speedScale;
        Time.timeScale = 1;
        Time.fixedDeltaTime = 0.02f * Time.timeScale;
    }
}
