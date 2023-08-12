using Interfaces;
using UnityEngine;
using UnityEngine.Events;

namespace Mechanics
{
    
    [System.Serializable]
    public class EntityLevel
    {
        public event UnityAction<int> LevelUpEvent;
        public event UnityAction<float,float> ChangeExperienceEvent;

        [field: SerializeField] public int Level { get; private set; }
        [field: SerializeField, Tooltip("If 0, that infinity")] public int MaxLevel { get; private set; }
        [field: SerializeField] public float CurrentExperience { get; private set; }
        public float ExperienceNormalized => CurrentExperience / MaxExperience;
        public float MaxExperience => GetMaxExperience();

        public EntityLevel(int maxLevel)
        {
            Level = 1;
            CurrentExperience = 0;
            MaxLevel = maxLevel;
        }

        public void AddExperience(IFloat iExperience)
        {
            if(MaxLevel != 0 && Level >= MaxLevel)
                return;
            
            CurrentExperience += iExperience.Value;

            while(CurrentExperience >= MaxExperience){
                Level++;
                LevelUpEvent?.Invoke(Level);
            }
            ChangeExperienceEvent?.Invoke(CurrentExperience, MaxExperience);
        }

        private float GetMaxExperience() 
            => Level * 5;
    }
}