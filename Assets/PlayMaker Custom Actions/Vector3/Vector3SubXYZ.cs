// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Vector3)]
	[Tooltip("Subtracts XYZ values from Vector3 Variable.")]
	public class Vector3SubXYZ : FsmStateAction
	{
		[RequiredField]
		[UIHint(UIHint.Variable)]
		public FsmVector3 vector3Variable;
		public FsmFloat subX;
		public FsmFloat subY;
		public FsmFloat subZ;
		public bool everyFrame;
		public bool perSecond;

		public override void Reset()
		{
			vector3Variable = null;
			subX = 0;
			subY = 0;
			subZ = 0;
			everyFrame = false;
			perSecond = false;
		}

		public override void OnEnter()
		{
			DoVector3SubXYZ();
			
			if (!everyFrame)
				Finish();		
		}

		public override void OnUpdate()
		{
			DoVector3SubXYZ();
		}
		
		void DoVector3SubXYZ()
		{
			var vector = new Vector3(subX.Value, subY.Value, subZ.Value);
			
			if(perSecond)
			{
				vector3Variable.Value -= vector * Time.deltaTime;
			}
			else
			{
				vector3Variable.Value -= vector;
			}
				
		}
	}
}

