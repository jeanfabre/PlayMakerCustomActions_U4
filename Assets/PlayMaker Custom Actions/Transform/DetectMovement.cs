// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory(ActionCategory.Transform)]
    [Tooltip("Transforms a Direction from a Game Object's local space to world space.")]
	public class DetectMovement : FsmStateAction
    {
        [RequiredField]
		[Tooltip("The GameObject to watch movement")]
        public FsmOwnerDefault gameObject;

		[Tooltip("The distance threshold to fire the movementDetected event")]
        public FsmFloat distanceThreshold;
  
		[Tooltip("Prefer SqrMagnitude over simple magnitude (real distance) for better performances")]
		public bool useSqrMagnitude;

		[Tooltip("Event Sent when the object moved passed the distance threshold")]
		public FsmEvent movementDetected;

		[UIHint(UIHint.Variable)]
		[Tooltip("The current distance being recorded")]
		public FsmFloat currentDistance;

		Vector3 _lastPosition;
		float _distance;

		GameObject go;

        public override void Reset()
        {
            gameObject = null;
			distanceThreshold = 1f;
			useSqrMagnitude = false;
			movementDetected = null;
			currentDistance = null;
        }

        public override void OnEnter()
        {
            ExecuteAction();
        }

        public override void OnUpdate()
        {
			ExecuteAction();
        }

		void ExecuteAction()
        {
			go = Fsm.GetOwnerDefaultTarget(gameObject);
            if (go == null) return;

			if (_lastPosition == null)
			{
				_lastPosition = go.transform.position;
				return;
			}

			if (useSqrMagnitude){
				_distance = (go.transform.position - _lastPosition).sqrMagnitude;
			} else {
				_distance = (go.transform.position - _lastPosition).magnitude;
			}

			if (!currentDistance.IsNone){
				currentDistance.Value = _distance;
			}

			if ( _distance >= distanceThreshold.Value ){
				Fsm.Event(movementDetected);
				_lastPosition = go.transform.position;
			}
        }
    }
}

