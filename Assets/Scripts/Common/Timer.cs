using System;
using System.Collections;
using UnityEngine;

namespace Common
{
    public class Timer
    {
        public event Action StartEvent;
        public event Action<float> UpdatedEvent;
        public event Action<float> TimeChangedEvent;
        public event Action EndEvent;

        public bool Stopped { get; private set; }
        public float Normalized => RemainingTime / Time;

        public float Time{ get; private set; }
        public float RemainingTime{ get; private set; }
        private IEnumerator countdown;
        private readonly MonoBehaviour context;

        public Timer(MonoBehaviour mono,Timer timer) : this(mono, timer.Time)
        {
            TimeChangedEvent = timer.TimeChangedEvent;
            StartEvent = timer.StartEvent;
            EndEvent = timer.EndEvent;
            UpdatedEvent = timer.UpdatedEvent;
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
            TimeChangedEvent?.Invoke(newValue);
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
            StartEvent?.Invoke();

            while(RemainingTime >= 0){
                RemainingTime -= UnityEngine.Time.deltaTime;
                UpdatedEvent?.Invoke(RemainingTime / Time);
                yield return null;
            }

            Stopped = true;
            EndEvent?.Invoke();
        }
    }
}