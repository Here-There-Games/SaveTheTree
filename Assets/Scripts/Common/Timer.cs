using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using UnityEngine;

namespace Common
{
    public class Timer
    {
        public event Action StartedEvent;
        public event Action<float, float> UpdateEvent;
        public event Action StoppedEvent;
        public event Action<float> ChangedTimeEvent;

        private CancellationTokenSource cancellationTokenSource;
        public float Time { get; private set; }
        public float RemainingTime { get; private set; }
        public State State { get; private set; }

        public Timer(float newTime)
        {
            Time = newTime;
            State = State.Stopped;
        }

        public void Start()
        {
            Stop();

            RemainingTime = Time;
            cancellationTokenSource = new CancellationTokenSource();
            Process(cancellationTokenSource.Token).Forget();
        }

        public void Stop()
        {
            if (cancellationTokenSource is{ IsCancellationRequested: false })
            {
                cancellationTokenSource.Cancel();
                cancellationTokenSource.Dispose();
                cancellationTokenSource = null; // Додаємо цю строку для уникнення повторної утилізації
            }
            State = State.Stopped;
            StoppedEvent?.Invoke();
        }
        
        public void ChangeTime(float newTime)
        {
            Stop();
            Time = newTime;
            ChangedTimeEvent?.Invoke(newTime);
        }

        private async UniTask Process(CancellationToken cancellationToken)
        {
            StartedEvent?.Invoke();
            State = State.Executing;

            UpdateEvent?.Invoke(RemainingTime, Time);
            await UniTask.Delay(TimeSpan.FromSeconds(1), cancellationToken: cancellationToken);

            while(RemainingTime > 0){
                if(cancellationToken.IsCancellationRequested){
                    State = State.Stopped;

                    return;
                }

                RemainingTime = Mathf.Max(RemainingTime - 1, 0);
                UpdateEvent?.Invoke(RemainingTime, Time);
                await UniTask.Delay(TimeSpan.FromSeconds(1), cancellationToken: cancellationToken);
            }

            State = State.Stopped;
            StoppedEvent?.Invoke();
        }
    }
}