using UnityEngine;

public class OscillatorObject : MonoBehaviour
{
    // fields
    public Vector2Oscillator mainOscillators;

    //methods
    protected void OnEnable()
    {
        OscillatorObjectManager.Instance.Register(this);
    }

    protected void OnDisable()
    {
        OscillatorObjectManager.Instance.Unregister(this);
    }

    public virtual void ManagedUpdate()
    {
    }
}
