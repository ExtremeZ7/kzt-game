namespace System.Collections{
	using UnityEngine;

	public class SaveAnimatorBooleanParameters : MonoBehaviour {

		public Animator animator;
		public string[] parameterNames;

		[Header("Private Variables")]
		[SerializeField]
		public bool[] parameterStates;

		void OnValidate(){
			Array.Resize<bool>(ref parameterStates,parameterNames.Length);
		}

		void Start(){
			if(animator == null)
				animator = GetComponent<Animator>();
		}

		void OnEnable() {
			for(int i = 0; animator != null && i < parameterNames.Length; i++){
				animator.SetBool(parameterNames[i], parameterStates[i]);
			}
		}

		void Update () {
			for(int i = 0; animator != null && i < parameterNames.Length; i++){
				parameterStates[i] = animator.GetBool(parameterNames[i]);
			}
		}
	}
}
