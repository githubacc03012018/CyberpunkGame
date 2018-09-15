using Assets.Scripts.Contracts;

namespace Assets.Scripts.Ability
{
    public abstract class Ability : IUpgradeable
    {
        protected float Cooldown { get; set; }

        public virtual void Upgrade()
        {
        }
    }
}
