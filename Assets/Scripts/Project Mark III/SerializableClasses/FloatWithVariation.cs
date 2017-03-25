using UnityEngine;

public class FloatWithVariation : MonoBehaviour
{
    public float value;
    public float variation;

    const bool absolute = false;
    readonly bool varyOnGet;

    public float Value
    {
        get
        {
            return varyOnGet ? value.Variation(variation, absolute) : value;
        }
    }

    public FloatWithVariation(float value, bool varyOnGet = false)
    {
        this.value = value;
        this.varyOnGet = varyOnGet;
    }

    void OnStart()
    {
        value = value.Variation(variation);
    }
}
