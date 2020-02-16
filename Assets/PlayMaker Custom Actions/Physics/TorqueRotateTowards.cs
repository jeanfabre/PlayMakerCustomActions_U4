// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/
// equation source: https://answers.unity.com/questions/727254/use-rigidbodyaddtorque-with-quaternions-or-forward.html

using UnityEngine;
using System.Collections;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Physics)]
	[Tooltip("Make a game object rotate towards a rotation using Torque")]
	public class TorqueRotateTowards : FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(Rigidbody))]
	 	[Tooltip("The object to effect.")]
		public FsmOwnerDefault gameObject;

		[UIHint(UIHint.Variable)]
		[Tooltip("The target it should use as rotation target")]
		public FsmGameObject targetObject;

		[Tooltip("The multiplier of torque to apply.")]
		public FsmFloat forceMultiplier;

		[Tooltip("The torque forceMode")]
		[ObjectType(typeof(ForceMode))]
		public FsmEnum forceMode;

		public FsmFloat maxSpeed;

		public FsmEvent Done;

		bool _done;

		public override void Reset()
		{
			gameObject = null;
			targetObject = null;
			forceMultiplier = 1f;
			forceMode = ForceMode.Force;
			maxSpeed = 1000f;
		}

		public override void OnPreprocess()
		{
			#if PLAYMAKER_1_8_5_OR_NEWER
			Fsm.HandleFixedUpdate = true;
			#endif
		}

		public override void OnFixedUpdate()
		{
			DoRotateTowardsAt();
		}
		
		void DoRotateTowardsAt()
		{
			var go = Fsm.GetOwnerDefaultTarget(gameObject);
			var target = targetObject.Value;


			if (_done) {

				go.rigidbody.angularVelocity = Vector3.zero;
				_done = false;
				Fsm.Event(Done);
				return;
			}


			go.rigidbody.maxAngularVelocity = maxSpeed.Value;

			Quaternion rotation = target.transform.rotation * Quaternion.Inverse(go.rigidbody.rotation);
			go.rigidbody.AddTorque(rotation.x * forceMultiplier.Value / Time.fixedDeltaTime, 
			                       rotation.y * forceMultiplier.Value / Time.fixedDeltaTime, 
			                       rotation.z * forceMultiplier.Value / Time.fixedDeltaTime, 
			                       (ForceMode)this.forceMode.Value);

			go.rigidbody.angularVelocity = Vector3.zero;

			if (Quaternion.Dot (go.transform.rotation, target.transform.rotation) >= 0.9999f) {
				_done = true; // one frame delay, to reset angular velocity
			}

		}
	}
}
