// (c) Copyright HutongGames, LLC 2010-2014. All rights reserved.
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/
/*--- keywords: mapper range cross multiplication rule of three ---*/

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Math)]
	[Tooltip("Remap a float from one scale to the other. example: we have 2 existing between 1 and 3, remapping 2 between 0 and 10 would give 5")]
	public class FloatRemap : FsmStateAction
	{
		[RequiredField]
		[Tooltip("The Valuae")]
		public FsmFloat theFloat;
		
		[RequiredField]
		[Tooltip("The base start reference")]
		public FsmFloat baseStart;
		
		[RequiredField]
		[Tooltip("The base end reference")]
		public FsmFloat baseEnd;
		
		[RequiredField]
		[Tooltip("The target start reference")]
		public FsmFloat targetStart;
		
		[RequiredField]
		[Tooltip("The target end reference")]
		public FsmFloat targetEnd;
		
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("Store the result in this float variable.")]
		public FsmFloat storeResult;

		[Tooltip("Repeat every frame. Useful if any of the values are changing.")]
		public bool everyFrame;

		public override void Reset()
		{
			theFloat = 50f;
			baseStart = 0f;
			baseEnd = 100f;
			
			targetStart = 0;
			targetEnd = 1f;
			
			storeResult = null;
			everyFrame = true;
		}

		public override void OnEnter()
		{
			DoFloatRemap();

			if (!everyFrame)
			{
				Finish();
			}
		}

		public override void OnUpdate()
		{
			DoFloatRemap();
		}

		void DoFloatRemap()
		{
			storeResult.Value = map (theFloat.Value,baseStart.Value,baseEnd.Value,targetStart.Value,targetEnd.Value);
		}
		
		float map(float s, float a1, float a2, float b1, float b2)
		{
		    return b1 + (s-a1)*(b2-b1)/(a2-a1);
		}
		
	}
}

