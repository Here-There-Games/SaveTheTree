using Common;
using System;
using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Demo.Scripts
{
    public class TimerTesting : MonoBehaviour
    {
        [SerializeField] private TMP_Text TimeText;
        [SerializeField] private TMP_Text RemainingTimeText;
        [SerializeField] private TMP_Text StateText;
        [SerializeField] private int timerTime;
        [SerializeField] private Button startTimer;
        [SerializeField] private Button stopTimer;
        [SerializeField] private Button changeTimer;
        [SerializeField] private TMP_InputField changeTimerField;

        private Timer timer;

        private void Start()
        {
            timer = new Timer(timerTime);
            startTimer.onClick.AddListener(timer.Start);
            stopTimer.onClick.AddListener(timer.Stop);
            changeTimer.onClick.AddListener(ChangeTimerTest);
            timer.UpdateEvent += UpdateUI;
            timer.StartedEvent += () => UpdateUI(timer.RemainingTime, timer.Time);
            timer.StoppedEvent += () => UpdateUI(timer.RemainingTime, timer.Time);
            UpdateUI(timerTime, timerTime);
        }

        private void UpdateUI(float remainingTime, float time)
        {
            TimeText.text = time.ToString(CultureInfo.InvariantCulture);
            RemainingTimeText.text = remainingTime.ToString(CultureInfo.InvariantCulture);
            StateText.text = timer.State.ToString();
        }

        private void ChangeTimerTest()
        {
            timer.ChangeTime(int.Parse(changeTimerField.text));
        }
    }
}