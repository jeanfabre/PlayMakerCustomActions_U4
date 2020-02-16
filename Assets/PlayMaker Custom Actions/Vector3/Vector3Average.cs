// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Vector3)]
	[Tooltip("Gets the average of all listed Vector3 variables.")]
	public class Vector3Average : FsmStateAction
	{
		[RequiredField]
		[UIHint(UIHint.Variable)]
		public FsmVector3[] vectorArray;

		[RequiredField]
		[UIHint(UIHint.Variable)]
		public FsmVector3 storeResult;

		public bool everyFrame;

		public override void Reset()
		{
			vectorArray = null;
			storeResult = null;
			everyFrame = false;
		}

		public override void OnEnter()
		{
			DoAverage();

			if (!everyFrame)
			{
				Finish();
			}
		}

		public override void OnUpdate()
		{
			DoAverage();
		}

		void DoAverage()
		{
			int i = 0;
			Vector3 average = new Vector3(0,0,0);
			while (i < vectorArray.Length)
			{
				average += vectorArray[i].Value;
				i++;
			}

			storeResult.Value = average / (vectorArray.Length);
		}
	}
}
