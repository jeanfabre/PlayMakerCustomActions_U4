// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;
using System;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Math)]
	[Tooltip("Returns the minimum float from a list")]
	public class GetFloatsMin : FsmStateAction
	{
		[RequiredField]
		[Tooltip("The float variables.")]
		public FsmFloat[] floats;

		[RequiredField]
		[UIHint(UIHint.Variable)]
		public FsmFloat storeResult;
		
		public bool everyFrame;
	
		public override void Reset()
		{
			floats = null;
            storeResult = null;
			everyFrame = false;
		}

		public override void OnEnter()
		{
			DoMinFromFloats();

			if (!everyFrame)
			{
				Finish();
			}
		}

		public override void OnUpdate()
		{
			DoMinFromFloats();
		}

		void DoMinFromFloats()
		{

			float _min = Mathf.Infinity;
			
			foreach(FsmFloat _value in floats)
			{
				float _val = _value.Value;
				if (_value.UseVariable && !_value.IsNone)
				{
					_val = this.Fsm.Variables.GetFsmFloat(_value.Name).Value;
				}
				_min = Mathf.Min(_min, _val);
			}

			storeResult.Value = _min;

		}
	}
}
