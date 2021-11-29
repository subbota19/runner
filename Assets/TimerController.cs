using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class TimerController : MonoBehaviour
{
    public event Action TimerEnded;
    public event Action UpdateTimer;

    [SerializeField]
    private TMP_Text timer;
    [SerializeField]
    private int countOfMinute;

    private DateTime timerEnd;
    private bool isStartTimer;
    private Coroutine coroutine;

    public void SetupTimer()
    {
        UpdateTimer?.Invoke();
    }

    public void ResetTimer()
    {
        timerEnd = DateTime.Now;
        timer.text = "";
    }

    private void Awake()
    {
        UpdateTimer += SetTimer;
        TimerEnded += DeleteCoroutine;
    }

    private void OnDisable()
    {
        UpdateTimer -= SetTimer;
        TimerEnded -= DeleteCoroutine;
    }

    private void SetTimer()
    {
        timerEnd = DateTime.Now.AddSeconds(countOfMinute * 60);
        isStartTimer = true;
        coroutine = StartCoroutine(nameof(TimerLogic));
    }

    private IEnumerator TimerLogic()
    {
        while (isStartTimer)
        {
            TimeSpan delta = timerEnd - DateTime.Now;

            if (delta.TotalSeconds <= 0)
            {
                TimerEnded?.Invoke();
                isStartTimer = false;
                break;
            }

            timer.text = delta.Minutes.ToString("00") + ":" + delta.Seconds.ToString("00");

            yield return new WaitForSeconds(1f);
        }
    }

    private void DeleteCoroutine()
    {
        StopCoroutine(coroutine);
    }
}
