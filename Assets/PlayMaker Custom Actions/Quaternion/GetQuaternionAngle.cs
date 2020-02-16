// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Quaternion)]
	[Tooltip("Gets the angle between two quaternions")]
	public class GetQuaternionAngle : QuaternionBaseAction
	{
		[Tooltip("First GameObject rotation")]
		public FsmOwnerDefault gameObject1;

		[Tooltip("Or First Quaternion")]
		public FsmQuaternion orQuaternion1;
		
		[Tooltip("First GameObject rotation")]
		public FsmOwnerDefault gameObject2;

		[Tooltip("Or Second Quaternion")]
		public FsmQuaternion orQuaternion2;

		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("the Angle between the two quaternion")]
		public FsmFloat angle;

		GameObject _Go1;
		GameObject _Go2;
		Quaternion _Q1;
		Quaternion _Q2;
		
		public override void Reset()
		{
			gameObject1 = null;
			gameObject2 = new FsmOwnerDefault ();
			gameObject2.OwnerOption = OwnerDefaultOption.SpecifyGameObject;
			gameObject2.GameObject = new FsmGameObject (){UseVariable = true};

			orQuaternion1 = new FsmQuaternion { UseVariable = true };
			orQuaternion2 = new FsmQuaternion { UseVariable = true };

			angle = null;
		}
		
		public override void OnEnter()
		{
			DoQuatAngle();
			
			if (!everyFrame)
			{
				Finish();
			}
		}
		
		public override void OnUpdate()
		{
			if (everyFrameOption == everyFrameOptions.Update )
			{
				DoQuatAngle();
			}
		}
		public override void OnLateUpdate()
		{
			if (everyFrameOption == everyFrameOptions.LateUpdate )
			{
				DoQuatAngle();
			}
		}
		public override void OnFixedUpdate()
		{
			if (everyFrameOption == everyFrameOptions.FixedUpdate )
			{
				DoQuatAngle();
			}
		}
		
		void DoQuatAngle()
		{
			if (!orQuaternion1.IsNone) {
				_Q1 = orQuaternion1.Value;
			} else {
				_Go1 = Fsm.GetOwnerDefaultTarget(gameObject1);
				if (_Go1!=null)
				{
					_Q1 = _Go1.transform.rotation;
				}
			}

			if (!orQuaternion2.IsNone) {
				_Q2 = orQuaternion2.Value;
			} else {
				_Go2 = Fsm.GetOwnerDefaultTarget(gameObject2);
				if (_Go2!=null)
				{
					_Q2 = _Go2.transform.rotation;
				}
			}

			angle.Value = Quaternion.Angle(_Q1,_Q2);

		}
	}
}

