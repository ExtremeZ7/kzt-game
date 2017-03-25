using UnityEngine;

public class OscillatorCalculator : MonoBehaviour
{
    //methods
    protected void OnEnable()
    {
        OscillatorCalculatorManager.Instance.Register(this);
    }

    protected void OnDisable()
    {
        OscillatorCalculatorManager.Instance.Unregister(this);
    }

    public virtual void ManagedUpdate()
    {
    }
}
