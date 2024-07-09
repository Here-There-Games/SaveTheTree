using Core.StatSystem.Interfaces;
using Core.Utilities.Attributes;
using System.Collections.Generic;
using UnityEngine;

namespace Core.StatSystem
{
    [CreateAssetMenu(fileName = "DataBase", menuName = "Game/Database")]
    public class DataBase : ScriptableObject
    {
        public List<IStat> Stats => stats;
        [field: SerializeReference, PickInterface]
        private List<IStat> stats;
    }
}