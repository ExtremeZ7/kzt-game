using System;

[Serializable]
public class TaggedFloat
{
    public float value;
    public UseValueAs useValueAs;

    public TaggedFloat(float value = 0f,
                       UseValueAs useValueAs = UseValueAs.Time)
    {
        this.value = value;
        this.useValueAs = useValueAs;
    }
}
