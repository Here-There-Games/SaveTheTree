using Interfaces;
using UnityEngine;
using UnityEngine.Events;

namespace Mechanics
{
    [System.Serializable]
    public class EntityLevel
    {
        public event UnityAction<int> OnLevelUp;
        public event UnityAction<float> OnChangeExperience;
        
        [field:SerializeField] public int Level { get; private set; }
        [field:SerializeField] public float CurrentExperience { get; private set; }
        public float ExperienceNormalized => CurrentExperience / MaxExperience;
        public float MaxExperience => GetMaxExperience();

        public EntityLevel()
        {
            Level = 0;
            CurrentExperience = 0;
        }

        public void AddExperience(IExperience experienceI)
        {
            CurrentExperience += experienceI.Experience;
            OnChangeExperience?.Invoke(CurrentExperience);

            while(CurrentExperience >= MaxExperience){
                CurrentExperience -= MaxExperience;
                OnLevelUp?.Invoke(Level);
            }
        }

        private float GetMaxExperience()
        {
            return Level * 10 + 50;
        }
    }
}