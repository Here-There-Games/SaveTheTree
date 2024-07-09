using Core.StatSystem.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Core.StatSystem
{
    [Serializable]
    public struct Stats
    {
        public event Action<IStat> AddStatEvent;
        [SerializeReference] private List<IStat> get;

        public Stats(params IStat[] newStats)
        {
            get = new List<IStat>();
            foreach(IStat newStat in newStats)
                get.Add(newStat.Clone());
            AddStatEvent = null;
        }

        public Stats(IReadOnlyList<IStat> newStats) : this(newStats.ToArray()) { }

        public IReadOnlyList<IStat> GetStats()
            => get;

        public Stats GetStatsNew()
            => new(get);

        public ResultToAddStat TryAddStat(IStat newStat)
        {
            ResultToAddStat result = TryAddStat(newStat.ID, newStat.Value);

            if(!result.AddToNew)
                return result;
            AddStatEvent?.Invoke(newStat);
            get.Add(newStat.Clone());

            TryGetStat(newStat.ID, out IStat stat);
            return new ResultToAddStat(stat, result.ValueToAdded, result.AddToNew);
        }

        public ResultToAddStat TryAddStat(string statID, float valueToAdd)
        {
            if(!TryGetStat(statID, out IStat instance))
                return new ResultToAddStat(statID, valueToAdd, valueToAdd, true);
            instance.ChangeValue(valueToAdd);
            return new ResultToAddStat(instance, valueToAdd, false);
        }

        public bool TryGetStat(string statName, out IStat stat)
        {
            stat = get.FirstOrDefault(s => string.Equals(s.ID, statName, StringComparison.CurrentCultureIgnoreCase));
            return stat != null && !string.IsNullOrEmpty(stat.ID);
        }
    }
}