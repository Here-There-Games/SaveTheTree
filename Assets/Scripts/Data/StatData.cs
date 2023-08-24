using System;
using System.Collections.Generic;
using System.Linq;
using Characters.Entity;
using Mechanics;
using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "Stat", menuName = "Game/new Stat", order = 0)]
    public class StatData : ScriptableObject
    {
        public IReadOnlyList<EntityStat> Stats => stats;
        public IReadOnlyList<EntityStat> Attributes => attributes;

        [SerializeField] private List<EntityStat> stats;
        [SerializeField] private List<EntityAttribute> attributes;

        public bool TryGetAttribute(string attributeName, out EntityAttribute attribute)
        {
            attribute = null;
            if(!attributes.Any(s => string.Equals(s.Name, attributeName, StringComparison.CurrentCultureIgnoreCase)))
                return false;

            attribute = FindAttribute(attributeName);
            return true;
        }

        public bool TryGetStat(string statName, out EntityStat attribute)
        {
            attribute = null;
            if(!attributes.Any(s => string.Equals(s.Name, statName, StringComparison.CurrentCultureIgnoreCase)))
                return false;

            attribute = FindAttribute(statName);
            return true;
        }

        public bool TryCreateAttribute(EntityAttribute attribute)
        {
            if(attributes.Contains(attribute) || attributes.Any(s => s.Name == attribute.Name))
                return false;

            attributes.Add(attribute);
            return true;
        }

        public bool TryCreateStat(EntityStat stat)
        {
            if(stats.Contains(stat) || stats.Any(s => s.Name == stat.Name))
                return false;

            stats.Add(stat);
            return true;
        }

        public EntityAttribute FindAttribute(string attributeName)
            => attributes.Find(a => string.Equals(a.Name, attributeName, StringComparison.CurrentCultureIgnoreCase));

        public EntityStat FindStat(string statName)
            => stats.Find(a => string.Equals(a.Name, statName, StringComparison.CurrentCultureIgnoreCase));
    }
}