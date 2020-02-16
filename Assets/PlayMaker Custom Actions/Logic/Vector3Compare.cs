// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Logic)]
	[Tooltip("Sends Events based on the comparison of 2 Vector3's.")]
	public class Vector3Compare : FsmStateAction
	{
		[RequiredField]
		public FsmVector3 vector3Variable1;
		[RequiredField]
		public FsmVector3 vector3Variable2;
		[RequiredField]
		public FsmFloat tolerance;

		[Tooltip("true Vector3 1 equals Vector3 2 (within Tolerance)")]
		public FsmBool result;

		[Tooltip("Event sent if Vector3 1 equals Vector3 2 (within Tolerance)")]
		public FsmEvent equal;
		[Tooltip("Event sent if the Vector3's are not equal")]
		public FsmEvent notEqual;
		public bool everyFrame;
		
		public override void Reset()
		{
			vector3Variable1 = null;
			vector3Variable2 = null;
			tolerance = 0f;
			result = null;
			equal = null;
			notEqual = null;
			everyFrame = false;
		}
		
		public override void OnEnter()
		{
			DoCompare();
			
			if (!everyFrame)
				Finish();
		}
		
		public override void OnUpdate()
		{
			DoCompare();
		}
		
		void DoCompare()
		{
			if (vector3Variable1 == null || vector3Variable2 == null) return;
			
			if (Mathf.Abs(vector3Variable1.Value.x - vector3Variable2.Value.x) <= tolerance.Value 
			    && Mathf.Abs(vector3Variable1.Value.y - vector3Variable2.Value.y) <= tolerance.Value
			    && Mathf.Abs(vector3Variable1.Value.z - vector3Variable2.Value.z) <= tolerance.Value)
			{
				result.Value = true;
				Fsm.Event(equal);

			} else {
				result.Value = false;
				Fsm.Event(notEqual);
			}
			
		}
	}
}
