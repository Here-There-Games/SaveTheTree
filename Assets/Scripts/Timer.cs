using System;
using System.Collections;
using UnityEngine;

public class Timer
{
    public event Action OnStart;
    public event Action<float> OnUpdated;
    public event Action OnTimeChanged;
    public event Action OnEnd;

    public bool Stopped { get; private set; }

    private float time;
    private float remainingTime;
    private IEnumerator countdown;
    private readonly MonoBehaviour context;

    public Timer(MonoBehaviour context, float time) : this(context)
        => ChangeTime(time);

    public Timer(MonoBehaviour context)
    {
        Stopped = true;
        this.context = context;
    }

    public void ChangeTime(float newValue)
    {
        time = newValue;
        remainingTime = newValue;
        OnTimeChanged?.Invoke();
    }

    public void Start()
    {
        remainingTime = time;
        countdown = Countdown();
        context.StartCoroutine(countdown);
    }

    public void Stop()
    {
        if(countdown != null)
            context.StopCoroutine(countdown);
    }

    private IEnumerator Countdown()
    {
        Stopped = false;
        OnStart?.Invoke();

        while(remainingTime >= 0){
            remainingTime -= Time.deltaTime;
            OnUpdated?.Invoke(remainingTime / time);
            yield return null;
        }

        Stopped = true;
        OnEnd?.Invoke();
    }
}