using UnityEngine;

namespace Assets.Scripts.Ability
{
    public class DodgeAbility : Ability
    {
        public float distance = 30f;
     
        public new float Cooldown;

        public override void Upgrade()
        {
            this.Cooldown -= 0.5f;
        }

        public void Dodge(CharacterController controller, DodgeDirection direction)
        {
            Vector3 horizontalDir = controller.transform.right * (int)direction * distance;
            controller.SimpleMove(horizontalDir);
        }
    }

    public enum DodgeDirection
    {
        Right = 1,
        Left = -1
    }
}
