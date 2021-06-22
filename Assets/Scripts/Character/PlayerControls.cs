using UnityEngine;
using UnityEngine.InputSystem;


namespace AstroSurveyor
{
    public class PlayerControls : MonoBehaviour
    {
        Character character;
        Animator animator;
        AnimatorManager state;
        Carry carry;
        Interact interact;
        float throwTime = 0.5f;
        Container inHands;

        public Container InHands { get => inHands; }

        bool CanInteract
        {
            get
            {
                if (!character.isHolding && !character.isThrowing)
                {
                    return true;
                }
                else if (character.isHolding && inHands != null)
                {
                    var target = inHands.GetComponent<Interactive>();
                    return target != null && target.mustCarry;
                }
                return false;
            }
        }

        void Start()
        {
            character = gameObject.GetComponent<Character>();
            animator = gameObject.transform.GetChild(0).GetComponent<Animator>();
            state = new AnimatorManager(animator, transform);
            carry = GetComponent<Carry>();
            interact = GetComponent<Interact>();
        }

        public Direction GetDirection()
        {
            return state.direction;
        }

        void Update()
        {
            // This check makes sure objects that transform on activation do not occupy the hand slot
            if (character.isHolding && inHands == null)
            {
                character.isHolding = false;
            }
            if (!character.isThrowing && !character.isBusy)
            {
                HandleMovement();
                if (interact.hasTarget)
                {
                    GameManager.Instance.ShowTarget(interact.target);
                }
                else
                {
                    GameManager.Instance.HideTarget();
                }
            }
            else if (character.isBusy)
            {
                character.busyTime += Time.deltaTime;
                var target = interact.target.GetComponent<Interactive>();
                GameManager.Instance.UpdateProgressBar(gameObject, character.busyTime / target.delay);

                if (target.delay < character.busyTime)
                {
                    state.SetState("idle");
                    character.isBusy = false;
                    character.busyTime = 0;
                    CompleteInteraction(target);
                }
            }
        }

        void ThrowComplete()
        {
            character.isThrowing = false;
        }

        public void OnMove(InputAction.CallbackContext value)
        {
            Vector2 input = value.ReadValue<Vector2>();
            character.inputX = input.x;
            character.inputY = input.y;
        }

        public void OnAction(InputAction.CallbackContext value)
        {
            if (value.performed && !character.isThrowing && !character.isBusy)
            {
                character.inputX = 0;
                character.inputY = 0;
                if (!character.isHolding && carry.hasTarget)
                {
                    character.isHolding = true;
                    state.SetState("hold");
                    inHands = carry.target.GetComponent<Container>();
                    inHands.OnPickUp(gameObject);
                }
                else if (character.isHolding)
                {
                    character.isHolding = false;
                    character.isThrowing = true;
                    state.SetState("throw");
                    inHands.OnDrop();
                    inHands = null;
                    Invoke("ThrowComplete", throwTime);
                }
            }
        }

        public void OnInteract(InputAction.CallbackContext value)
        {
            if (CanInteract)
            {
                if (value.performed)
                {
                    character.inputX = 0;
                    character.inputY = 0;
                    if (interact.hasTarget)
                    {
                        var target = interact.target.GetComponent<Interactive>();
                        if (target.delay == 0)
                        {
                            CompleteInteraction(target);
                        }
                        else
                        {
                            state.SetState("examine");
                            character.isBusy = true;
                            character.busyTime = 0;
                        }
                    }
                }
                else
                {
                    if (character.isBusy)
                    {
                        state.SetState("idle");
                        character.isBusy = false;
                        character.busyTime = 0;
                        GameManager.Instance.HideProgressBar(gameObject);
                    }
                }
            }
        }

        void CompleteInteraction(Interactive target)
        {
            target.OnInteract();
        }

        void HandleUI()
        {
            // selectorUI.GetComponent<RingSelector>().Input(character.control.x, character.control.y);
        }

        void HandleMovement()
        {
            if (character.inputX == 0 && character.inputY == 0)
            {
                state.SetState(character.isHolding ? "hold" : "idle");
            }
            else
            {
                state.SetState(character.isHolding ? "carry" : "walk");

                if (Mathf.Abs(character.inputX) > Mathf.Abs(character.inputY))
                {
                    if (character.inputX > 0)
                    {
                        state.SetDirection(Direction.RIGHT);
                        character.UpdateHandsOffset(Direction.RIGHT);
                    }
                    else if (character.inputX < 0)
                    {
                        state.SetDirection(Direction.LEFT);
                        character.UpdateHandsOffset(Direction.LEFT);
                    }
                }
                else if (Mathf.Abs(character.inputX) < Mathf.Abs(character.inputY))
                {
                    if (character.inputY > 0)
                    {
                        state.SetDirection(Direction.UP);
                        character.UpdateHandsOffset(Direction.UP);
                    }
                    else
                    {
                        state.SetDirection(Direction.DOWN);
                        character.UpdateHandsOffset(Direction.DOWN);
                    }
                }
            }
        }
    }
}
