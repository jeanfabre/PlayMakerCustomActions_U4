// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.GameObject)]
	[Tooltip("Gets the fsm name of the host  when runnin as a sub Fsm")]
	public class GetHost : FsmStateAction
	{
		[RequiredField]
		[UIHint(UIHint.Variable)]
		public FsmString hostName;

		public override void Reset()
		{
			hostName = null;
		}
		
		public override void OnEnter()
		{
			hostName.Value = Fsm.Host.Name;
			
			Finish();
		}
	}
}
