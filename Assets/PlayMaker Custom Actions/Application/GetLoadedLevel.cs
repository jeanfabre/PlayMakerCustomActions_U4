// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;
#if UNITY_5_3_OR_NEWER
using UnityEngine.SceneManagement;
#endif

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
			#if UNITY_5_3_OR_NEWER
			levelName.Value = SceneManager.GetActiveScene().name;
			level.Value = SceneManager.GetActiveScene().buildIndex;
			#else
			levelName.Value = Application.loadedLevelName;
			level.Value = Application.loadedLevel;
			#endif
			
			Finish();
		}
	}
}