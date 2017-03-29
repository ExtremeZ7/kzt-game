using System;
using Common.Extensions;

[Serializable]
public class FloatWithVariation
{
    public float value;
    public float variation;

    const bool absolute = false;

    // Analysis disable once ConvertToAutoProperty
    public float Value { get { return value; } set { this.value = value; } }

    public float VariedValue
    {
        get
        {
            return value.Variation(variation, absolute);
        }
    }

    public FloatWithVariation(float value = 0f)
    {
        Value = value;
    }

    public void VaryValue()
    {
        Value = Value.Variation(variation);
    }
}
