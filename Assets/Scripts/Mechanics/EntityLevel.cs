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

        public void AddExperience(IFloat experienceI)
        {
            while(CurrentExperience + experienceI.Value >= MaxExperience){
                if(MaxLevel != 0 && Level >= MaxLevel)
                    break;
                CurrentExperience++;
                OnChangeExperience?.Invoke(CurrentExperience);

                if(CurrentExperience >= MaxExperience){
                    CurrentExperience -= MaxExperience;
                    Level++;
                    OnLevelUp?.Invoke(Level);
                }
            }
        }

        private float GetMaxExperience()
        {
            return Level * 15;
        }
    }
}