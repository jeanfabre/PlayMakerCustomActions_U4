// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Vector3)]
	[Tooltip("Get Vector3 Length.")]
	public class GetVector3Length : FsmStateAction
	{
		public FsmVector3 vector3;
		[RequiredField]
		[UIHint(UIHint.Variable)]
		public FsmFloat storeLength;
        [Tooltip("Repeat every frame")]
        public bool everyFrame;

        public override void Reset()
		{
			vector3 = null;
			storeLength = null;
            everyFrame = false;
        }

		public override void OnEnter()
		{
			DoVectorLength();
            if (!everyFrame)
            {
                Finish();
            }
        }

        public override void OnUpdate()
        {
            DoVectorLength();
        }

        void DoVectorLength()
		{
			if (vector3 == null) return;
			if (storeLength == null) return;
			storeLength.Value = vector3.Value.magnitude;
		}
	}
}
