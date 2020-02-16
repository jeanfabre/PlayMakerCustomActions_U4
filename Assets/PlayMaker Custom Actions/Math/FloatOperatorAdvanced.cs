// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__
EcoMetaStart
{
"script dependancies":[
						"Assets/PlayMaker Custom Actions/__Internal/FsmStateActionAdvanced.cs"
					]
}
EcoMetaEnd
---*/

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Math)]
	[Tooltip("Performs math operations on 2 Floats: Add, Subtract, Multiply, Divide, Min, Max. This advanced version let you choose when to perform the action.")]
	public class FloatOperatorAdvanced : FsmStateActionAdvanced
	{
		public enum Operation
		{
			Add,
			Subtract,
			Multiply,
			Divide,
			Min,
			Max,
			Power
		}

		[RequiredField]
        [Tooltip("The first float.")]
		public FsmFloat float1;

		[RequiredField]
        [Tooltip("The second float.")]
		public FsmFloat float2;

        [Tooltip("The math operation to perform on the floats.")]
		public Operation operation = Operation.Add;

		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("Store the result of the operation in a float variable.")]
        public FsmFloat storeResult;
		

		public override void Reset()
		{
			base.Reset();
			
			float1 = null;
			float2 = null;
			operation = Operation.Add;
			storeResult = null;

		}

		public override void OnActionUpdate()
		{
			DoFloatOperator();
		}
		
		void DoFloatOperator()
		{
			var v1 = float1.Value;
			var v2 = float2.Value;

			switch (operation)
			{
				case Operation.Add:
					storeResult.Value = v1 + v2;
					break;

				case Operation.Subtract:
					storeResult.Value = v1 - v2;
					break;

				case Operation.Multiply:
					storeResult.Value = v1 * v2;
					break;

				case Operation.Divide:
					storeResult.Value = v1 / v2;
					break;

				case Operation.Min:
					storeResult.Value = Mathf.Min(v1, v2);
					break;

				case Operation.Max:
					storeResult.Value = Mathf.Max(v1, v2);
					break;
				case Operation.Power:
					storeResult.Value = Mathf.Pow(v1, v2);
				break;
			}
		}
	}
}
