using UnityEngine;

namespace Assets.Scripts.Ability
{
    public class DodgeAbility : Ability
    {
        public float distance = 0.3f;
         
        public override void Upgrade()
        {
            this.Cooldown -= 0.5f;
        }

        public void Dodge(Transform transform, DodgeDirection direction)
        {
            Vector3 horizontalDir = transform.right * (int)direction * distance;
            transform.position += horizontalDir;
        }
    }

    public enum DodgeDirection
    {
        Right = 1,
        Left = -1
    }
}
