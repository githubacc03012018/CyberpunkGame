using UnityEngine;

namespace Assets.Scripts.Ability
{
    public class DodgeAbility : Ability
    {
        public float distance;
        public new float Cooldown;
        public float startTime = 1.0f;
        public float transitionSpeed;

        public override void Upgrade()
        {
            this.Cooldown -= 0.5f;
        }

        public void Dodge(Transform transform, DodgeDirection direction)
        {
            Vector3 targetPosition = transform.position + transform.right * (int)direction * distance;
            transform.position = Vector3.Lerp(transform.position, targetPosition, (Time.time - startTime) * transitionSpeed * Time.deltaTime);
        }
    }

    public enum DodgeDirection
    {
        Right = 1,
        Left = -1
    }
}
