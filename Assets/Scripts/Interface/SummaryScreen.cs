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
        public Text specimens;
        public Text unique;
        public Text score;

        bool m_ButtonPressed = false;


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
        }

        public void Display(int specimens, int unique, int score)
        {
            this.specimens.text = specimens.ToString();
            this.unique.text = unique.ToString();
            this.score.text = score.ToString();
        }
    }
}