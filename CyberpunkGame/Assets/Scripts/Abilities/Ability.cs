using Assets.Scripts.Contracts;
using UnityEngine;

namespace Assets.Scripts.Ability
{
    public abstract class Ability : MonoBehaviour, IUpgradeable
    {
        protected float Cooldown { get; set; }

        public abstract void Upgrade();
    }
}
