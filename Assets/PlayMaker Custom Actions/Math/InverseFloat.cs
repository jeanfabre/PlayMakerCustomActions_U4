// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Math)]
	[Tooltip("Inverts a float variable. For example '20.5' becomes '-20.5'")]
	public class InverseFloat : FsmStateAction
	{
		[RequiredField]
		[Tooltip("The Float variable to inverse.")]
		public FsmFloat floatVariable;

		[RequiredField]
		[Tooltip("Where to store the result.")]
		public FsmFloat storeFloat;

		[Tooltip("Set a base value, so as to offset growth.")]
		public FsmFloat baseValue;
		
		[Tooltip("Repeat every frame while the state is active.")]
		public bool everyFrame;

		private float temp;

		public override void Reset()
		{
			floatVariable = null;
			storeFloat = null;
			baseValue = 0;
			everyFrame = false;
		}

		public override void OnEnter()
		{
			DoFloatInverse();
			
			if (!everyFrame)
			{
				Finish();
			}
		}
		
		public override void OnUpdate()
		{
			DoFloatInverse();
		}
		
		void DoFloatInverse()
		{
			temp = baseValue.Value;
			temp -= floatVariable.Value;
			storeFloat.Value = temp;
		}
	}
}
