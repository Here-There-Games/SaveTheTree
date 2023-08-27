using Cinemachine;
using Common;
using UnityEngine;

namespace Mechanics
{
    public class CameraShake : MonoBehaviour
    {
        [SerializeField] private float duration = 0.4f;

        private float defaultAmplitude;
        private float defaultFrequency;
        private CinemachineVirtualCamera virtualCamera;
        private CinemachineBasicMultiChannelPerlin perlin;
        private Timer timer;

        private void Awake()
        {
            virtualCamera = GetComponent<CinemachineVirtualCamera>();
            perlin = virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

            timer = new Timer(this, duration);
            
        }

        public void Init(float amplitude = 5, float frequency = 5)
        {
            timer.OnStartEvent += () =>
                                      {
                                          defaultAmplitude = perlin.m_AmplitudeGain;
                                          defaultFrequency = perlin.m_FrequencyGain;
                                          perlin.m_AmplitudeGain = amplitude;
                                          perlin.m_FrequencyGain = frequency;
                                      };
            timer.OnEndEvent += () =>
                                    {
                                        perlin.m_AmplitudeGain = defaultAmplitude;
                                        perlin.m_FrequencyGain = defaultFrequency;
                                    };
        }
        
        public void StartShake()
        {
            if(timer.Stopped)
                timer.Start();
        }

        public void StartShake(float newTime)
        {
            if(timer.Stopped){
                timer.ChangeTime(newTime);
                StartShake();
            }
        }
    }
}