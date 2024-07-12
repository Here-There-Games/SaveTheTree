using Cysharp.Threading.Tasks;
using System;

namespace Entity
{
    /// <summary>
    /// Attack for Entity
    /// </summary>
    public class Attack
    {
        private readonly Action behaviour;
        private readonly float preparing;
        private readonly float cooldown;
        // private IStat preparing;
        // private IStat cooldown;

        private AttackState State { get; set; }

        /// <summary>
        /// Attack Entity
        /// <list type="behaviour">An event initiated by an entity during an attack</list>
        /// <list type="preparing">An event initiated by an entity during an attack</list>
        /// </summary>
        public Attack(Action behaviour, float preparing = 0, float cooldown = 1)
        {
            this.behaviour = behaviour;
            this.preparing = preparing;
            this.cooldown = cooldown;
            State = AttackState.Stopped;
        }

        /// <summary>
        /// Start Attack
        /// </summary>
        public bool TryStart()
        {
            if(State != AttackState.Stopped){
                return false;
            }

            Proceed().Forget();
            return true;
        }

        private async UniTask Proceed()
        {
            State = AttackState.Preparing;
            await UniTask.Delay(TimeSpan.FromSeconds(preparing));

            behaviour?.Invoke();

            State = AttackState.Cooldown;
            await UniTask.Delay(TimeSpan.FromSeconds(cooldown));

            State = AttackState.Stopped;
        }
    }
}