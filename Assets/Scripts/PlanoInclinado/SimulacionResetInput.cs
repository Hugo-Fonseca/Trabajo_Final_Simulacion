using UnityEngine;

public class SimulacionResetInput : MonoBehaviour
{
    public PlanoInclinado2DManager manager;

    void OnEnable()
    {
        if (InputManager.Instance != null)
            InputManager.Instance.OnReset += ResetSim;
    }

    void OnDisable()
    {
        if (InputManager.Instance != null)
            InputManager.Instance.OnReset -= ResetSim;
    }

    private void ResetSim()
    {
        manager.ResetSimulation();
    }
}
