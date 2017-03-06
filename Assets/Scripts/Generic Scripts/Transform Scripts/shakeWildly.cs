using UnityEngine;
using AssemblyCSharp;

public class shakeWildly : MonoBehaviour
{

    public Vector2 shakeIntensity;
    public Vector2 shakeSize;

    [Space(10)]
    [Range(0f, 1f)]
    public float shakeProbability;
    [Range(0f, 1f)]
    public float pauseProbability;

    private Vector3 originalPosition;
    private Vector2 direction;

    void Awake()
    {
        originalPosition = transform.localPosition;
        direction = Vector2.one;
    }

    void Update()
    {
        for (int i = 0; i < 2; i++)
        {
            if (Random.Range(0f, 1f) <= shakeProbability)
                direction[i] *= -1;
        }

        if (Random.Range(0f, 1f) <= pauseProbability)
            goto Finish;

        transform.localPosition = new Vector3(transform.localPosition.x + (shakeIntensity.x * direction.x),
            transform.localPosition.y + (shakeIntensity.y * direction.y),
            0f);

        for (int i = 0; i < 2; i++)
        {
            if (transform.localPosition[i] < originalPosition[i] - shakeSize[i] || transform.localPosition[i] > originalPosition[i] + shakeSize[i])
                direction[i] *= -1;
        }

        transform.localPosition = new Vector3(Helper.forceRange(transform.localPosition.x, originalPosition.x - shakeSize.x, originalPosition.x + shakeSize.x),
            Helper.forceRange(transform.localPosition.y, originalPosition.y - shakeSize.y, originalPosition.y + shakeSize.y),
            0f);

        Finish:
        ;
    }
}
