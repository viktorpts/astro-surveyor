using UnityEngine;


namespace AstroSurveyor
{
    public class Character : MonoBehaviour
    {
        // Data
        Rigidbody2D body;
        public float acceleration = 100f;
        public float maxSpeed = 5f;
        public float handsOffsetX = 0.35f;
        public float handsOffsetY = 0.15f;
        float sqrMaxSpeed;

        // State
        public Vector2 handsOffset;
        public Vector2 Velocity { get; private set; }
        public float inputX, inputY;
        public bool isHolding;
        public bool isThrowing;

        void Start()
        {
            body = GetComponent<Rigidbody2D>();
            sqrMaxSpeed = maxSpeed * maxSpeed;
            Velocity = new Vector2(0, 0);

            handsOffset = new Vector2();
        }

        void FixedUpdate()
        {
            UpdateVelocity();
        }

        private void UpdateVelocity()
        {
            if (body.velocity.sqrMagnitude < sqrMaxSpeed)
            {
                var accX = acceleration * inputX;
                var accY = acceleration * inputY;
                body.AddForce(new Vector2(accX, accY));
            }
            Velocity = body.velocity;
        }

        public void UpdateHandsOffset(Direction dir)
        {
            switch (dir)
            {
                case Direction.UP:
                    handsOffset.x = 0f;
                    handsOffset.y = handsOffsetY;
                    break;
                case Direction.DOWN:
                    handsOffset.x = 0f;
                    handsOffset.y = -handsOffsetY;
                    break;
                case Direction.LEFT:
                    handsOffset.x = -handsOffsetX;
                    handsOffset.y = -0.05f;
                    break;
                case Direction.RIGHT:
                    handsOffset.x = handsOffsetX;
                    handsOffset.y = -0.05f;
                    break;
            }
        }
    }
}
