using UnityEngine;
using AssemblyCSharp;

public class SendChildrenOnOrbit : MonoBehaviour
{

    public enum Rotation
    {
        Clockwise,
        CounterClockwise,
        Randomize}

    ;

    public enum PauseStyle
    {
        None,
        EveryCompleteRotation,
        Custom}

    ;

    public enum LoopType
    {
        Normal,
        HalfSine,
        NegativeHalfSine}

    ;

    public enum OrbitArrangement
    {
        EqualDistances,
        Randomize,
        Custom}

    ;

    public bool sendAllChidrenOnOrbit = true;
    [Tooltip("How many of the gameObject's children will be sent to orbit starting from the first child")]
    public int numberOfOrbitingChildren;

    [Space(10)]
    public float RPM;
    [Tooltip("in seconds")]
    [SerializeField]
    private float timePerRotation;
    public Rotation rotation;

    [Space(10)]
    public float startDelay;
    public PauseStyle pauseStyle = PauseStyle.None;
    public float[] pauseDegrees;
    public float[] pauseTimes;
    private float pauseTimer;

    [Space(10)]
    public OrbitArrangement orbitArrangement;
    [Range(0f, 360f)]
    public float globalOffset;
    [Range(0f, 360f)]
    public float[] degreeOffsets;

    [Space(10)]
    public Vector2 orbitSize;

    [Space(10)]
    public LoopType loopTypeX;
    public LoopType loopTypeY;

    [Space(10)]
    public Vector2[] transformOffsets;

    private Transform[] orbitingChildren;
    private float xDistance;
    private float yDistance;
    private float degreeValue = 0f;
    private float gizmoDegreeValue = 0f;

    [Header("Randomizers")]
    public float rotationSpeedRandomizer;

    [Header("Performance Tweaks")]
    public bool stopWhenNotVisible;
    private bool isVisible;

    [ContextMenu("Validate")]
    void OnValidate()
    {
        if (!name.Contains("[ORBITER]"))
            name += " [ORBITER]";

        gizmoDegreeValue = 0f;

        if (sendAllChidrenOnOrbit)
            numberOfOrbitingChildren = transform.childCount;
        else
            Help.forceRange(ref numberOfOrbitingChildren, 1, transform.childCount);

        orbitingChildren = new Transform[numberOfOrbitingChildren];

        for (int i = 0; i < numberOfOrbitingChildren; i++)
        {
            orbitingChildren[i] = transform.GetChild(i);
        }

        switch (pauseStyle)
        {
            case PauseStyle.None:
                System.Array.Resize(ref pauseDegrees, 0);
                System.Array.Resize(ref pauseTimes, 0);
                break;
            case PauseStyle.EveryCompleteRotation:
                System.Array.Resize(ref pauseDegrees, 1);
                pauseDegrees[0] = 0f;
                System.Array.Resize(ref pauseTimes, 1);
                break;
            case PauseStyle.Custom:
                System.Array.Resize(ref pauseTimes, pauseDegrees.Length);
                break;
        }

        for (int i = 0; i < pauseDegrees.Length; i++)
            pauseDegrees[i] = pauseDegrees[i] % 360f;

        Help.forceRange(ref RPM, 0f, float.MaxValue);

        System.Array.Resize(ref transformOffsets, numberOfOrbitingChildren);

        switch (orbitArrangement)
        {
            case OrbitArrangement.EqualDistances:
                degreeOffsets = new float[numberOfOrbitingChildren];
                for (int i = 0; i < numberOfOrbitingChildren; i++)
                    degreeOffsets[i] = (360f / numberOfOrbitingChildren) * i;
                break;
            case OrbitArrangement.Randomize:
                System.Array.Resize(ref degreeOffsets, 0);
                break;
            case OrbitArrangement.Custom:
                System.Array.Resize(ref degreeOffsets, numberOfOrbitingChildren);
                for (int i = 0; i < numberOfOrbitingChildren; i++)
                {
                    degreeOffsets[i] %= 360f;
                }
                break;
        }

        for (int i = 0; i < numberOfOrbitingChildren; i++)
        {
            float degreeOffset = 0f;

            switch (orbitArrangement)
            {
                case OrbitArrangement.EqualDistances:
                case OrbitArrangement.Custom:
                    degreeOffset = degreeOffsets[i];
                    break;
                case OrbitArrangement.Randomize:
                    degreeOffset = Random.Range(0f, float.MaxValue) % 360f;
                    break;
            }

            degreeOffset += globalOffset;

            xDistance = orbitSize.x * Mathf.Cos(degreeOffset * Mathf.Deg2Rad);
            yDistance = orbitSize.y * Mathf.Sin(degreeOffset * Mathf.Deg2Rad);

            switch (loopTypeX)
            {
                case LoopType.HalfSine:
                    xDistance = Mathf.Abs(xDistance);
                    break;
                case LoopType.NegativeHalfSine:
                    xDistance = -Mathf.Abs(xDistance);
                    break;
            }

            switch (loopTypeY)
            {
                case LoopType.HalfSine:
                    yDistance = Mathf.Abs(yDistance);
                    break;
                case LoopType.NegativeHalfSine:
                    yDistance = -Mathf.Abs(yDistance);
                    break;
            }

            orbitingChildren[i].transform.localPosition = new Vector3(xDistance + transformOffsets[i].x, yDistance + transformOffsets[i].y, transform.localPosition.z);
        }

        timePerRotation = 1f / (RPM / 60f);
    }

    void OnDrawGizmosSelected()
    {
        int numberOfSamples = 60;
        float degreeDistance = 360f / ((float)numberOfSamples);

        for (int i = 0; i < numberOfSamples; i++)
        {
            Vector3 currentPoint = Vector3.zero;
            Vector3 nextPoint = Vector3.zero;

            currentPoint.x = orbitSize.x * Mathf.Cos(degreeDistance * i * Mathf.Deg2Rad);
            currentPoint.y = orbitSize.y * Mathf.Sin(degreeDistance * i * Mathf.Deg2Rad);

            nextPoint.x = orbitSize.x * Mathf.Cos(degreeDistance * (i + 1) * Mathf.Deg2Rad);
            nextPoint.y = orbitSize.y * Mathf.Sin(degreeDistance * (i + 1) * Mathf.Deg2Rad);

            switch (loopTypeX)
            {
                case LoopType.HalfSine:
                    currentPoint.x = Mathf.Abs(currentPoint.x);
                    nextPoint.x = Mathf.Abs(nextPoint.x);
                    break;
                case LoopType.NegativeHalfSine:
                    currentPoint.x = -Mathf.Abs(currentPoint.x);
                    nextPoint.x = -Mathf.Abs(nextPoint.x);
                    break;
            }

            switch (loopTypeY)
            {
                case LoopType.HalfSine:
                    currentPoint.y = Mathf.Abs(currentPoint.y);
                    nextPoint.y = Mathf.Abs(nextPoint.y);
                    break;
                case LoopType.NegativeHalfSine:
                    currentPoint.y = -Mathf.Abs(currentPoint.y);
                    nextPoint.y = -Mathf.Abs(nextPoint.y);
                    break;
            }

            currentPoint = new Vector3(transform.position.x + currentPoint.x, transform.position.y + currentPoint.y, 0f);
            nextPoint = new Vector3(transform.position.x + nextPoint.x, transform.position.y + nextPoint.y, 0f);

            Gizmos.color = Color.white;
            Gizmos.DrawLine(currentPoint, nextPoint);
        }

        float sphereRadius = 0.125f;

        for (int i = 0; i < numberOfOrbitingChildren; i++)
        {
            if (orbitingChildren[i] == null)
                continue;

            float gizmoDegreeOffset = 0f;

            switch (orbitArrangement)
            {
                case OrbitArrangement.EqualDistances:
                case OrbitArrangement.Custom:
                    gizmoDegreeOffset = degreeOffsets[i];
                    break;
                case OrbitArrangement.Randomize:
                    gizmoDegreeOffset = Random.Range(0f, float.MaxValue) % 360f;
                    break;
            }

            gizmoDegreeOffset += globalOffset;

            xDistance = orbitSize.x * Mathf.Cos((gizmoDegreeValue + gizmoDegreeOffset) * Mathf.Deg2Rad);
            yDistance = orbitSize.y * Mathf.Sin((gizmoDegreeValue + gizmoDegreeOffset) * Mathf.Deg2Rad);

            switch (loopTypeX)
            {
                case LoopType.HalfSine:
                    xDistance = Mathf.Abs(xDistance);
                    break;
                case LoopType.NegativeHalfSine:
                    xDistance = -Mathf.Abs(xDistance);
                    break;
            }

            switch (loopTypeY)
            {
                case LoopType.HalfSine:
                    yDistance = Mathf.Abs(yDistance);
                    break;
                case LoopType.NegativeHalfSine:
                    yDistance = -Mathf.Abs(yDistance);
                    break;
            }

            Gizmos.color = Color.cyan;
            Gizmos.DrawSphere(new Vector3(transform.position.x + xDistance + transformOffsets[i].x, transform.position.y + yDistance + transformOffsets[i].y, 0f), sphereRadius);
        }

        switch (rotation)
        {
            case Rotation.Clockwise:
                gizmoDegreeValue = gizmoDegreeValue - (RPM * 0.1f);
                while (gizmoDegreeValue < 0f)
                    gizmoDegreeValue += 360f;
                break;
            case Rotation.CounterClockwise:
                gizmoDegreeValue = (gizmoDegreeValue + (RPM * 0.1f)) % 360.0f;
                break;
        }
    }

    void Start()
    {
        RPM += Random.Range(0f, rotationSpeedRandomizer);

        if (sendAllChidrenOnOrbit)
            numberOfOrbitingChildren = transform.childCount;

        switch (rotation)
        {
            case Rotation.Randomize:
                rotation = Random.Range(float.MinValue, float.MaxValue) > 0 ? Rotation.CounterClockwise : Rotation.Clockwise;
                break;
        }

        switch (orbitArrangement)
        {
            case OrbitArrangement.EqualDistances:
                degreeOffsets = new float[numberOfOrbitingChildren];
                for (int i = 0; i < numberOfOrbitingChildren; i++)
                    degreeOffsets[i] = (360f / numberOfOrbitingChildren) * i;
                break;
            case OrbitArrangement.Randomize:
                System.Array.Resize(ref degreeOffsets, numberOfOrbitingChildren);
                for (int i = 0; i < numberOfOrbitingChildren; i++)
                    degreeOffsets[i] = Random.Range(0f, float.MaxValue) % 360f;
                break;
        }

        orbitingChildren = new Transform[numberOfOrbitingChildren];

        for (int i = 0; i < numberOfOrbitingChildren; i++)
        {
            orbitingChildren[i] = transform.GetChild(i);
        }

        pauseTimer = 0f;

        timePerRotation = timePerRotation + 0f;
    }

    void Update()
    {
        if (!(stopWhenNotVisible && !isVisible) && gameObject.activeInHierarchy && gameObject.activeSelf && Help.UseAsTimer(ref startDelay))
        {
            for (int i = 0; i < numberOfOrbitingChildren; i++)
            {
                if (orbitingChildren[i] == null)
                    continue;

                xDistance = orbitSize.x * Mathf.Cos((degreeValue + degreeOffsets[i] + globalOffset) * Mathf.Deg2Rad);
                yDistance = orbitSize.y * Mathf.Sin((degreeValue + degreeOffsets[i] + globalOffset) * Mathf.Deg2Rad);

                switch (loopTypeX)
                {
                    case LoopType.HalfSine:
                        xDistance = Mathf.Abs(xDistance);
                        break;
                    case LoopType.NegativeHalfSine:
                        xDistance = -Mathf.Abs(xDistance);
                        break;
                }

                switch (loopTypeY)
                {
                    case LoopType.HalfSine:
                        yDistance = Mathf.Abs(yDistance);
                        break;
                    case LoopType.NegativeHalfSine:
                        yDistance = -Mathf.Abs(yDistance);
                        break;
                }
						

                orbitingChildren[i].transform.localPosition = new Vector3(xDistance + transformOffsets[i].x, yDistance + transformOffsets[i].y, transform.localPosition.z);
            }

            if (Help.UseAsTimer(ref pauseTimer))
            {
                float previousDegree = degreeValue;

                switch (rotation)
                {
                    case Rotation.Clockwise:
                        degreeValue -= RPM * 0.1f * Time.deltaTime * 60f;

                        for (int i = 0; i < pauseDegrees.Length; i++)
                        {
                            if (degreeValue < 0f)
                            {
                                if (pauseDegrees[i] >= degreeValue + 360f || pauseDegrees[i] < previousDegree)
                                {
                                    degreeValue = pauseDegrees[i];
                                    pauseTimer = pauseTimes[i];
                                    break;
                                }
                            }
                            else
                            {
                                if (pauseDegrees[i] >= degreeValue && pauseDegrees[i] < previousDegree)
                                {
                                    degreeValue = pauseDegrees[i];
                                    pauseTimer = pauseTimes[i];
                                    break;
                                }
                            }
                        }

                        if (degreeValue < 0f)
                            degreeValue += 360f;

                        break;
                    case Rotation.CounterClockwise:
                        degreeValue += RPM * 0.1f * Time.deltaTime * 60f;

                        for (int i = 0; i < pauseDegrees.Length; i++)
                        {
                            if (degreeValue > 360f)
                            {
                                if (pauseDegrees[i] <= degreeValue - 360f || pauseDegrees[i] > previousDegree)
                                {
                                    degreeValue = pauseDegrees[i];
                                    pauseTimer = pauseTimes[i];
                                    break;
                                }
                            }
                            else
                            {
                                if (pauseDegrees[i] <= degreeValue && pauseDegrees[i] > previousDegree)
                                {
                                    degreeValue = pauseDegrees[i];
                                    pauseTimer = pauseTimes[i];
                                    break;
                                }
                            }
                        }

                        if (degreeValue > 360f)
                            degreeValue -= 360f;
                        break;
                }
            }
        }
    }

    void OnBecameInvisible()
    {
        isVisible = false;
    }

    void OnBecameVisible()
    {
        isVisible = true;
    }
}
