using UnityEngine;
using AssemblyCSharp;

namespace AssemblyCSharp
{
    public class CollisionSwitch : CollisionHandler
    {
        void OnCollisionEnter2D(Collision2D coll)
        {
            Trigger(coll.gameObject);
        }

        void OnCollisionStay2D(Collision2D coll)
        {
            if (activateOnStay)
            {
                Trigger(coll.gameObject);
            }
        }

        void OnCollisionExit2D(Collision2D coll)
        {
            ExitTrigger(coll.gameObject);
        }
    }
}
