using UnityEngine;
using AssemblyCSharp;

namespace AssemblyCSharp
{
    public class ShiftDirectionWhenTriggered : MonoBehaviour
    {

        public TriggerSwitch trigger;
        public Rigidbody2D rb2d;

        [Space(10)]
        public string[] tags;

        void Start()
        {
            if (trigger == null)
                trigger = GetComponent<TriggerSwitch>();
            if (rb2d == null)
                rb2d = GetComponent<Rigidbody2D>();
        }

        void Update()
        {
            if (trigger.ActivatedOnCurrentFrame)
            {
                if (trigger.TriggeringObject != null)
                {
                    rb2d.transform.position = trigger.TriggeringObject.gameObject.transform.position;
                    rb2d.velocity = GetNewVector(rb2d.velocity, trigger.TriggeringObject.transform.localRotation.eulerAngles.z);
                }
                else
                    Object.Destroy(gameObject);
            }
        }

        Vector2 GetNewVector(Vector2 vector, float tileAngle)
        {
            while (tileAngle < 0)
                tileAngle += 360;
            tileAngle %= 360;

            int tileDir = (int)(tileAngle / 90);
            int vectorDir = -1;

            if (vector.x > 0f && vector.y.IsWithinRange(-0.5f, 0.5f))
                vectorDir = 0;
            else if (vector.x.IsWithinRange(-0.5f, 0.5f) && vector.y > 0f)
                vectorDir = 1;
            else if (vector.x < 0f && vector.y.IsWithinRange(-0.5f, 0.5f))
                vectorDir = 2;
            else if (vector.x.IsWithinRange(-0.5f, 0.5f) && vector.y < 0f)
                vectorDir = 3;
            else if (vector.x > 0f && vector.y > 0f)
                vectorDir = 4;
            else if (vector.x < 0f && vector.y > 0f)
                vectorDir = 5;
            else if (vector.x < 0f && vector.y < 0f)
                vectorDir = 6;
            else if (vector.x > 0f && vector.y < 0f)
                vectorDir = 7;

            switch (tileDir)
            {
                case 0:
                    if (vectorDir == 2)
                        return new Vector2(0f, -vector.x);
                    else if (vectorDir == 3)
                        return new Vector2(-vector.y, 0f);
                    else if (vectorDir == 7 || vectorDir == 6)
                        return new Vector2(vector.x, -vector.y);
                    return vector;
                case 1:
                    if (vectorDir == 0)
                        return new Vector2(0f, vector.x);
                    else if (vectorDir == 3)
                        return new Vector2(vector.y, 0f);
                    else if (vectorDir == 4 || vectorDir == 7)
                        return new Vector2(-vector.x, vector.y);
                    return vector;
                case 2:
                    if (vectorDir == 0)
                        return new Vector2(0f, -vector.x);
                    else if (vectorDir == 1)
                        return new Vector2(-vector.y, 0f);
                    else if (vectorDir == 4 || vectorDir == 5)
                        return new Vector2(vector.x, -vector.y);
                    return vector;
                case 3:
                    if (vectorDir == 2)
                        return new Vector2(0f, vector.x);
                    else if (vectorDir == 1)
                        return new Vector2(vector.y, 0);
                    else if (vectorDir == 6 || vectorDir == 5)
                        return new Vector2(-vector.x, vector.y);
                    return vector;
                default:
                    if (Debug.isDebugBuild)
                        Debug.Log("WTF? {ShiftDirectionWhenTriggered}");
                    return Vector2.zero;
            }
        }
    }
}