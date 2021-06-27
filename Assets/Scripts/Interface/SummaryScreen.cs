using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.LowLevel;


namespace AstroSurveyor
{
    public class SummaryScreen : MonoBehaviour
    {
        public Text specimensLabel;
        public Text uniqueLabel;
        public Text scoreLabel;

        bool m_ButtonPressed = false;
        float rollIn = 0f;
        int specimens;
        int unique;
        int score;


        void Start()
        {
            InputSystem.onEvent += (eventPtr, device) =>
                        {
                            if (!eventPtr.IsA<StateEvent>() && !eventPtr.IsA<DeltaStateEvent>())
                                return;
                            var controls = device.allControls;
                            var buttonPressPoint = InputSystem.settings.defaultButtonPressPoint;
                            for (var i = 0; i < controls.Count; ++i)
                            {
                                var control = controls[i] as ButtonControl;
                                if (control == null || control.synthetic || control.noisy)
                                    continue;
                                if (control.ReadValueFromEvent(eventPtr, out var value) && value >= buttonPressPoint)
                                {
                                    m_ButtonPressed = true;
                                    break;
                                }
                            }
                        };
        }

        void Update()
        {
            if (m_ButtonPressed)
            {
                SceneManager.LoadScene("Menu");
            }



            if (rollIn < 0.5)
            {
                this.specimensLabel.text = Mathf.RoundToInt(Mathf.Lerp(0, specimens, rollIn * 2)).ToString();
            }
            else if (rollIn < 1)
            {
                this.specimensLabel.text = specimens.ToString();
                this.uniqueLabel.text = Mathf.RoundToInt(Mathf.Lerp(0, unique, (rollIn - 0.5f) * 2)).ToString();
            }
            else
            {
                this.uniqueLabel.text = unique.ToString();
                this.scoreLabel.text = Mathf.RoundToInt(Mathf.Lerp(0, score, (rollIn - 1f) / 2f)).ToString();
            }

            if (rollIn < 3)
            {
                rollIn += Time.deltaTime;
            }
            else
            {
                this.specimensLabel.text = specimens.ToString();
                this.uniqueLabel.text = unique.ToString();
                this.scoreLabel.text = score.ToString();
            }
        }

        public void Display(int specimens, int unique, int score)
        {
            this.specimens = specimens;
            this.unique = unique;
            this.score = score;
        }
    }
}