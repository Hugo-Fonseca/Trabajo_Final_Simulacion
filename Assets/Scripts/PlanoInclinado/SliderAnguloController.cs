using UnityEngine;
using UnityEngine.UI;

public class SliderAnguloController : MonoBehaviour
{
    [SerializeField] private Slider sliderAngulo;
    [SerializeField] private float sensibilidad = 20f; // ajusta la velocidad del joystick

    private void OnEnable()
    {
        if (InputManager.Instance != null)
            InputManager.Instance.OnMove += HandleMoveInput; // joystick + WASD
    }

    private void OnDisable()
    {
        if (InputManager.Instance != null)
            InputManager.Instance.OnMove -= HandleMoveInput;
    }

    private void HandleMoveInput(Vector2 input)
    {
        float delta = input.x * sensibilidad * Time.deltaTime;
        sliderAngulo.value += delta;  // esto moverá la rampa automáticamente
    }
}
