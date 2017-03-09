using UnityEngine;
using AssemblyCSharp;
using System;

public class travelAcrossARail : MonoBehaviour
{

    public enum LoopType
    {
Normal,
Circular,
BackAndForth,
DestroyAtLastPoint}

    ;

    public enum Direction
    {
Forward = 1,
Backward = -1}

    ;

    public enum MovementStyle
    {
ConstantSpeed,
Accelerate,
TimedSpeed}

    ;

    [Space(10)]
    public Transform transformOfTraveler;
    public Transform rail;

    [Space(10)]
    public float defaultSpeed;
    public float startDelay;

    [Space(10)]
    public Direction startDirection = Direction.Forward;
    public int startPointIndex;
    public bool teleportToStartPoint;
    public bool stateReset;

    [Space(10)]
    public float[] speeds;
    public float[] pauseTimes;

    [Space(10)]
    public LoopType loopType;
    public MovementStyle movementStyle;
	
    private float speed;
    private float accelerationRatio;
    private int direction;
    private float timer;
    private Transform[] points;
    private int targetIndex;
    private bool destroyed;

    private bool isMoving;

    [ContextMenu("Revalidate")]
    void OnValidate()
    {
        if (!name.Contains("[RAIL]"))
            name += " [RAIL]";

        int childCount = rail.transform.childCount;

        if (defaultSpeed < 0f)
            defaultSpeed = 0f;

        if (startPointIndex < 0)
            startPointIndex = 0;

        if (startPointIndex > childCount - 1)
            startPointIndex = childCount - 1;

        if (rail != null && childCount > 1)
        {
            Array.Resize<float>(ref speeds, childCount);
            Array.Resize<float>(ref pauseTimes, childCount);
        }
        else
        {
            Array.Resize<float>(ref speeds, 0);
            Array.Resize<float>(ref pauseTimes, 0);
            startPointIndex = 0;
        }
    }

    [ContextMenu("Automatically Get Transform And Rail")]
    void AutomaticallyGetTransformAndRail()
    {
        transformOfTraveler = transform.GetChild(0);
        rail = transform.GetChild(1);
        OnValidate();
    }

    void OnDrawGizmos()
    {
        Vector3 origin = (transformOfTraveler == null ? transform.position : transformOfTraveler.position);

        Vector3 cubeDimension = new Vector3(0.25f, 0.25f, 0f);
        Vector3 originDimension = new Vector3(0.5f, 0.5f, 0f);

        if (rail.transform.childCount == 0)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(origin, rail.transform.position);

            Gizmos.color = Color.green;
            Gizmos.DrawCube(origin, originDimension);
            Gizmos.color = Color.magenta;
            Gizmos.DrawCube(rail.transform.position, cubeDimension);
        }
        else
        {
            for (int i = 0; i < rail.transform.childCount; i++)
            {
                int index;
                switch (startDirection)
                {
                    case Direction.Forward:
                        index = (i + startPointIndex) % rail.transform.childCount;

                        if (index == startPointIndex)
                        {
                            Gizmos.color = Color.yellow;
                            Gizmos.DrawLine(origin, rail.GetChild(index).position);
                            Gizmos.color = Color.green;
                            Gizmos.DrawCube(origin, originDimension);
                        }
                        else
                        {
                            Gizmos.color = Color.yellow;
                            Gizmos.DrawLine(rail.GetChild(index == 0 ? rail.transform.childCount - 1 : index - 1).position, rail.GetChild(index).position);
                        }

                        Gizmos.color = Color.magenta;
                        Gizmos.DrawCube(rail.GetChild(index).position, cubeDimension);
                        break;
                    case Direction.Backward:
                        index = startPointIndex - i;

                        if (index < 0)
                            index += rail.transform.childCount;

                        if (index == startPointIndex)
                        {
                            Gizmos.color = Color.yellow;
                            Gizmos.DrawLine(origin, rail.GetChild(index).position);
                            Gizmos.color = Color.green;
                            Gizmos.DrawCube(origin, originDimension);
                        }
                        else
                        {
                            Gizmos.color = Color.yellow;
                            Gizmos.DrawLine(rail.GetChild((index + 1) % rail.transform.childCount).position, rail.GetChild(index).position);
                        }
                        Gizmos.color = Color.magenta;
                        Gizmos.DrawCube(rail.GetChild(index).position, cubeDimension);
                        break;
                }
            }
        }
    }

    void Start()
    {
        if (transformOfTraveler == null)
            transformOfTraveler = transform;

        direction = (int)startDirection;

        if (rail.childCount > 0)
        {
            points = new Transform[rail.childCount];
            for (int i = 0; i < points.Length; i++)
                points[i] = rail.GetChild(i);
        }
        else
        {
            points = new Transform[1];
            points[0] = rail;
        }

        if (points.Length == 0)
            points[0] = rail;

        targetIndex = startPointIndex;

        if (teleportToStartPoint)
            transformOfTraveler.position = points[targetIndex].position;

        if (pauseTimes.Length > 0)
            timer = pauseTimes[targetIndex];
    }

    void Update()
    {
        if (transformOfTraveler != null && Help.UseAsTimer(ref startDelay))
        {
            if (Help.UseAsTimer(ref timer))
            {
                switch (movementStyle)
                {
                    case MovementStyle.Accelerate:
                        accelerationRatio = Mathf.MoveTowards(accelerationRatio, 1f, Time.deltaTime);
                        speed = (speeds.Length == 0 || speeds[targetIndex] == 0f ? defaultSpeed : speeds[targetIndex]) * accelerationRatio;
                        break;
                    case MovementStyle.ConstantSpeed:
                        speed = (speeds.Length == 0 || speeds[targetIndex] == 0f ? defaultSpeed : speeds[targetIndex]);
                        break;
                    case MovementStyle.TimedSpeed:
                        float distanceToNextPoint = Vector3.Distance(points[(targetIndex == 0 ? points.Length - 1 : targetIndex - 1)].position, points[targetIndex].position);
                        speed = (speeds.Length == 0 || speeds[targetIndex] == 0f ? distanceToNextPoint / defaultSpeed : distanceToNextPoint / speeds[targetIndex]);
                        break;
                }

                float distance2Travel = Time.deltaTime * speed;

                while (distance2Travel > Vector3.Distance(transformOfTraveler.position, points[targetIndex].position))
                {
                    distance2Travel -= Vector3.Distance(transformOfTraveler.position, points[targetIndex].position);
                    transformOfTraveler.position = points[targetIndex].position;
                    getNextTargetIndex();
                    accelerationRatio = 0f;

                    if (pauseTimes.Length > 0)
                        timer = pauseTimes[targetIndex];

                    if (timer > 0f || destroyed || Vector3.Distance(transformOfTraveler.position, points[targetIndex].position) == 0f)
                        break;
                }

                if (Vector3.Distance(transformOfTraveler.position, points[targetIndex].position) == 0f || timer > 0f)
                    isMoving = false;
                else
                    isMoving = true;

                if (timer == 0f && !destroyed)
                {
                    transformOfTraveler.position = Vector3.MoveTowards(transformOfTraveler.position, 
                        points[targetIndex].position, 
                        distance2Travel);
                }
            }
        }
    }

    void getNextTargetIndex()
    {
        switch (loopType)
        {
            case LoopType.Normal:
            case LoopType.Circular:
                targetIndex += direction;

                if (targetIndex > points.Length - 1)
                    targetIndex = (loopType == LoopType.Normal ? points.Length - 1 : 0);
                else if (targetIndex < 0)
                    targetIndex = (loopType == LoopType.Normal ? 0 : points.Length - 1);

                break;
            case LoopType.BackAndForth:
                targetIndex += direction;
                if (targetIndex == points.Length || targetIndex == -1)
                {
                    targetIndex += 2 * -direction;
                    direction *= -1;
                }

                if (targetIndex > points.Length - 1)
                    targetIndex = (loopType == LoopType.Normal ? points.Length - 1 : 0);
                else if (targetIndex < 0)
                    targetIndex = (loopType == LoopType.Normal ? 0 : points.Length - 1);

                break;
            case LoopType.DestroyAtLastPoint:
                targetIndex++;
                if (targetIndex == points.Length)
                {
                    for (int i = 0; i < points.Length; i++)
                        UnityEngine.Object.Destroy(points[i].gameObject);
                    UnityEngine.Object.Destroy(transformOfTraveler.gameObject);
                    destroyed = true;
                }
                break;
        }
    }

    public void setRailDirection(bool notReversed)
    {
        direction = (notReversed ? 1 : -1);

        targetIndex += direction;

        if (targetIndex < 0)
            targetIndex = 0;
        else if (targetIndex >= points.Length)
            targetIndex = points.Length - 1;
    }

    public void reverseRailDirection()
    {
        direction *= -1;
    }

    public int getDirection()
    {
        return direction;
    }

    public bool IsMoving()
    {
        return isMoving;
    }

    public void StateReset()
    {
        if (stateReset)
        {
            targetIndex = startPointIndex;
            transformOfTraveler.position = rail.GetChild(startPointIndex).position;
        }
    }
}