// (c) Copyright HutongGames, LLC 2010-2014. All rights reserved.
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Logic)]
	[Tooltip("Sends Events based on the comparison of 2 Vector2's.")]
	public class Vector2Compare : FsmStateAction
	{
		[RequiredField]
		public FsmVector2 vector2Variable1;
		[RequiredField]
		public FsmVector2 vector2Variable2;
		[RequiredField]
		public FsmFloat tolerance;
		[Tooltip("Event sent if Vector2 1 equals Vector1 2 (within Tolerance)")]
		public FsmEvent equal;
		[Tooltip("Event sent if the Vector1's are not equal")]
		public FsmEvent notEqual;
		public bool everyFrame;
		
		public override void Reset()
		{
			vector2Variable1 = null;
			vector2Variable2 = null;
			tolerance = 0f;
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
			if (vector2Variable1 == null || vector2Variable2 == null) return;
			
			if (Mathf.Abs(vector2Variable1.Value.x - vector2Variable2.Value.x) <= tolerance.Value 
			    && Mathf.Abs(vector2Variable1.Value.y - vector2Variable2.Value.y) <= tolerance.Value)
			{
				Fsm.Event(equal);
				return;
			} else {
				Fsm.Event(notEqual);
			}
			
		}
		
		public override string ErrorCheck()
		{
			if (FsmEvent.IsNullOrEmpty(equal) &&
			    FsmEvent.IsNullOrEmpty(notEqual))
				return "Action sends no events!";
			return "";
		}
	}
}
