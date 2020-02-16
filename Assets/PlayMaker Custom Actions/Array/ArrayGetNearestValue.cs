// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
// __ECO__ __PLAYMAKER__ __ACTION__ 

using UnityEngine;

using System.Linq;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Array)]
	[Tooltip("Get the nearest index and value for a given variable. Only works for float and int")]
	public class ArrayGetNearestValue : FsmStateAction
	{
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("The Array to use.")]
		public FsmArray array;

		[RequiredField]
		[Tooltip("The value to check for")]
		[MatchElementType("array")]
		public FsmVar value;

		[Tooltip("Store the nearest value found in the array.")]
		[UIHint(UIHint.Variable)]
		[MatchElementType("array")]
		public FsmVar result;
		
		[Tooltip("The index of the nearest value found in the array.")]
		[UIHint(UIHint.Variable)]
		public FsmInt index;

		[Tooltip("Repeat every frame while the state is active.")]
		public bool everyFrame;


		public override void Reset()
		{
			array = null;
			value = null;
			result =null;
			index = null;
			everyFrame = false;
		}


		public override void OnEnter()
		{
			DoGetNearestValue();
			
			if (!everyFrame)
			{
				Finish();
			}
		}

		public override string ErrorCheck()
		{
			if (array.IsNone)
				return string.Empty;

			if (array.ElementType == VariableType.Float)
				return string.Empty;

			if (array.ElementType == VariableType.Int)
				return string.Empty;

			return "Only floats and ints are supported";
		}
		
		public override void OnUpdate()
		{
			DoGetNearestValue();
			
		}
		
		private void DoGetNearestValue()
		{

			if(array.ElementType == VariableType.Float)
			{
				float closest = array.floatValues.OrderBy(v => Mathf.Abs(v - (float)value.GetValue())).First();

				if (!result.IsNone) {
					result.SetValue(closest);
				}

				if (!index.IsNone) {
					index.Value = System.Array.IndexOf(this.array.floatValues,closest);
				}
				return;
			}

			if(array.ElementType == VariableType.Int)
			{
				int closest = array.intValues.OrderBy(v => Mathf.Abs(v - (int)value.GetValue())).First();
				
				if (!result.IsNone) {
					result.SetValue(closest);
				}
				
				if (!index.IsNone) {
					index.Value = System.Array.IndexOf(this.array.intValues,closest);
				}
				return;
			}
		}
		
		
	}
}

