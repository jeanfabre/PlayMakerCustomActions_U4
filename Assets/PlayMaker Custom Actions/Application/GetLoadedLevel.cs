// (c) Copyright HutongGames, LLC 2010-2014. All rights reserved.
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Application)]
	[Tooltip("Get the name or id of the level that was last loaded (Read Only).")]
	public class GetLoadedLevel : FsmStateAction
	{
		
		[UIHint(UIHint.Variable)]
		public FsmString levelName;
	
		[UIHint(UIHint.Variable)]
		public FsmInt level;
		
		public override void Reset()
		{
			levelName =null;
				level = null;
		}

		public override void OnEnter()
		{
			levelName.Value = Application.loadedLevelName;
			level.Value = Application.loadedLevel;
			
			Finish();
		}
	}
}
