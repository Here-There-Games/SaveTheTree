using System;
using System.Collections;
using UnityEngine;

namespace Common
{
    public class Timer
    {
        public event Action OnStartEvent;
        public event Action<float> OnUpdatedNormalizedEvent;
        public event Action<float> OnUpdatedEvent;
        public event Action<float> OnTimeChangedEvent;
        public event Action OnEndEvent;

        public bool Stopped { get; private set; }
        public float Normalized => RemainingTime / Time;

        public float Time{ get; private set; }
        public float RemainingTime{ get; private set; }
        private IEnumerator countdown;
        private readonly MonoBehaviour context;

        public Timer(MonoBehaviour mono,Timer timer) : this(mono, timer.Time)
        {
            OnTimeChangedEvent = timer.OnTimeChangedEvent;
            OnStartEvent = timer.OnStartEvent;
            OnEndEvent = timer.OnEndEvent;
            OnUpdatedNormalizedEvent = timer.OnUpdatedNormalizedEvent;
        }
        public Timer(MonoBehaviour context, float time) : this(context)
            => ChangeTime(time);

        public Timer(MonoBehaviour context)
        {
            Stopped = true;
            this.context = context;
        }

        public void ChangeTime(float newValue)
        {
            Time = newValue;
            RemainingTime = newValue;
            OnTimeChangedEvent?.Invoke(newValue);
        }

        public void Start()
        {
            RemainingTime = Time;
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
            OnStartEvent?.Invoke();

            while(RemainingTime >= 0){
                RemainingTime -= UnityEngine.Time.deltaTime;
                OnUpdatedNormalizedEvent?.Invoke(RemainingTime / Time);
                OnUpdatedEvent?.Invoke(RemainingTime);
                yield return null;
            }

            Stopped = true;
            OnEndEvent?.Invoke();
        }
    }
}