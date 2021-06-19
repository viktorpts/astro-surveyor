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
        public bool inLZ;
        float throwTime = 0.66f;
        Container inHands;
        public GameObject selectorUI;
        bool uiMode = false;

        void Start()
        {
            character = gameObject.GetComponent<Character>();
            animator = gameObject.transform.GetChild(0).GetComponent<Animator>();
            state = new AnimatorManager(animator, transform);
            carry = GetComponent<Carry>();
        }

        public Direction GetDirection()
        {
            return state.direction;
        }

        void Update()
        {
            if (character.isThrowing)
            {
                return;
            }
            else if (uiMode)
            {
                HandleUI();
            }
            else
            {
                HandleMovement();
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
            if (value.performed && !uiMode)
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
                    Invoke("ThrowComplete", throwTime);
                }
            }
        }

        public void OnInteract(InputAction.CallbackContext value)
        {
            if (value.performed && inLZ && !character.isHolding && !character.isThrowing)
            {
                if (!uiMode)
                {
                    uiMode = true;
                    selectorUI.SetActive(true);
                }
                else
                {
                    uiMode = false; ;
                    selectorUI.SetActive(false);
                }
            }
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
