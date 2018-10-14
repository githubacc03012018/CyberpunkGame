using UnityEngine;

namespace Assets.Scripts.Ability
{
    public class DodgeAbility : Ability
    {
        public float distance;
        public new float Cooldown;
        public override void Upgrade()
        {
            this.Cooldown -= 0.5f;
        }

        public void Dodge(Transform transform, DodgeDirection direction)
        {
            Vector3 dodge = transform.right * (int)direction * distance;
            transform.position += dodge;
        }
    }

    public enum DodgeDirection
    {
        Right = 1,
        Left = -1
    }
}
