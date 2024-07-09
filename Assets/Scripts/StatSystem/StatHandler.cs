using Core.StatSystem.Interfaces;
using Core.Utilities.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Core.StatSystem
{
    public class StatHandler : MonoBehaviour, IBuffable
    {
        public event Action<ResultToAddStat> StatAddedEvent;
        public event Action<IBuff> AddBuffEvent;
        public event Action<IBuff> RemoveBuffEvent;

        public IReadOnlyList<IBuff> Buffs => buffs;

        [SerializeReference, PickerDataBase(typeof(DataBase))] private IStat[] statTemplate;
        private readonly List<IBuff> buffs = new();
        public Stats BaseStats { get; private set; }
        public Stats CurrentStats { get; private set; }

        private void Awake()
        {
            if(statTemplate == null)
                throw new ArgumentNullException($"Stat Template in {name} is null");
            BaseStats = new Stats(statTemplate);
            CurrentStats = new Stats(BaseStats.GetStats());
        }

        public void AddStat(IStat statToAdd, bool inBase = false)
            => AddStat(statToAdd.ID, statToAdd.Value, inBase);

        public void AddStat(string nameStat, float valueToAdd, bool inBase = false)
        {
            ResultToAddStat result = inBase
                                         ? BaseStats.TryAddStat(nameStat, valueToAdd)
                                         : CurrentStats.TryAddStat(nameStat, valueToAdd);

            StatAddedEvent?.Invoke(result);
        }

        public void AddBuff(IBuff buff)
        {
            buffs.Add(buff);
            ApplyBuffs();
            AddBuffEvent?.Invoke(buff);
        }

        public void RemoveBuff(IBuff buff)
        {
            if(!buffs.Contains(buff)){
                Debug.LogWarning(buff.GetType() + $" is not found in {name}.");
                return;
            }

            buffs.Remove(buff);
            ApplyBuffs();
            RemoveBuffEvent?.Invoke(buff);
        }

        private void ApplyBuffs()
        {
            CurrentStats = BaseStats.GetStatsNew();
            CurrentStats = buffs.Aggregate(CurrentStats, (current, buff) => buff.ApplyStats(current));
        }
    }
}