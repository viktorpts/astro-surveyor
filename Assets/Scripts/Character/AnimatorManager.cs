using System.Collections.Generic;
using UnityEngine;


namespace AstroSurveyor
{
    public class AnimatorManager
    {
        Animator animator;
        Transform transform;
        string state;
        public Direction direction;
        private Dictionary<string, int[]> states;

        public AnimatorManager(Animator animator, Transform transform)
        {
            this.animator = animator;
            this.transform = transform;
            state = "idle";
            direction = Direction.DOWN;

            states = new Dictionary<string, int[]>()
            {
                ["idle"] = new int[] {
                    Animator.StringToHash("idle_up"),
                    Animator.StringToHash("idle_side"),
                    Animator.StringToHash("idle_down")
                },
                ["walk"] = new int[] {
                    Animator.StringToHash("walk_up"),
                    Animator.StringToHash("walk_side"),
                    Animator.StringToHash("walk_down")
                },
                ["hold"] = new int[] {
                    Animator.StringToHash("hold_up"),
                    Animator.StringToHash("hold_side"),
                    Animator.StringToHash("hold_down")
                },
                ["carry"] = new int[] {
                    Animator.StringToHash("carry_up"),
                    Animator.StringToHash("carry_side"),
                    Animator.StringToHash("carry_down")
                },
                ["throw"] = new int[] {
                    Animator.StringToHash("throw_up"),
                    Animator.StringToHash("throw_side"),
                    Animator.StringToHash("throw_down")
                },
                ["examine"] = new int[] {
                    Animator.StringToHash("duck_up"),
                    Animator.StringToHash("duck_side"),
                    Animator.StringToHash("duck_down")
                }
            };
        }

        public void SetState(string newState)
        {
            if (state != newState)
            {
                state = newState;
                animator.Play(states[state][DirectionToIndex(direction)]);
            }
        }

        public void SetDirection(Direction newDirection)
        {
            if (direction != newDirection)
            {
                direction = newDirection;
                animator.Play(states[state][DirectionToIndex(direction)]);
                UpdateTransformDir(direction);
            }
        }

        private void UpdateTransformDir(Direction dir)
        {
            switch (dir)
            {
                case Direction.LEFT:
                    transform.localScale = new Vector3(1, 1);
                    break;
                case Direction.RIGHT:
                    transform.localScale = new Vector3(-1, 1);
                    break;
            }
        }

        private int DirectionToIndex(Direction dir)
        {
            switch (dir)
            {
                case Direction.UP:
                    return 0;
                case Direction.DOWN:
                    return 2;
                case Direction.LEFT:
                case Direction.RIGHT:
                    return 1;
                default:
                    return 2;
            }
        }
    }
}