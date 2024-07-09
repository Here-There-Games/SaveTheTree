using Core.StatSystem.Interfaces;

namespace Core.StatSystem
{
    public struct ResultToAddStat
    {
        public bool AddToNew { get; }
        public bool AddToFounded { get; }
        public float ValueToAdded { get; }
        public float StatValue { get; }
        public string StatID { get; }

        public ResultToAddStat(string statID, float valueToAdded, float statValue, bool addToNew)
        {
            AddToNew = addToNew;
            AddToFounded = !addToNew;
            ValueToAdded = valueToAdded;
            StatValue = statValue;
            StatID = statID;
        }

        public ResultToAddStat(IStat stat, float valueToAdded, bool addToNew)
            : this(stat.ID, valueToAdded, stat.Value, addToNew) { }
    }
}