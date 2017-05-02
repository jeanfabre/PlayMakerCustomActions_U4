// (c) Copyright HutongGames, LLC 2010-2013. All rights reserved.
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;
using System.Collections;


namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Application)]
	[Tooltip("gets the unity version and place into a string")]
	public class UnityVersion : FsmStateAction
	{
        [RequiredField]
        [UIHint(UIHint.Variable)]
        public FsmString Store;


        public override void Reset()
		{
            Store = null;
        }

		public override void OnEnter()
		{
#if (UNITY_EDITOR)
			    Store.Value = Application.unityVersion;
#else
            Store.Value = "This is not a unity Editor" ;
#endif
            Finish();
		}
	}
}