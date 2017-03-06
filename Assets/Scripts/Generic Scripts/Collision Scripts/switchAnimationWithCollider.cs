using UnityEngine;
using AssemblyCSharp;

namespace AssemblyCSharp
{
    public class switchAnimationWithCollider : MonoBehaviour
    {

        public string parameterString;
        private Animator ani;
        public GameObject objectWithCollider;
        private CollisionSwitch coll;


        // Use this for initialization
        void Start()
        {
            if (objectWithCollider == null)
                objectWithCollider = this.gameObject;
            ani = GetComponent<Animator>();
            coll = objectWithCollider.GetComponent<CollisionSwitch>();
        }
	
        // Update is called once per frame
        void Update()
        {
            ani.SetBool(parameterString, coll.IsActivated);
        }
    }
}