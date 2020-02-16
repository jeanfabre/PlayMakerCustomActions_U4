// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__
EcoMetaStart
{
"script dependancies":["Assets/PlayMaker Custom Actions/GUI/Internal/GUISizer.cs"]
}
EcoMetaEnd
---*/

using UnityEngine;
using System.Collections;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.GUI)]
	[Tooltip("Set the default size of the gui when using GuiBeginSize and GuiEndSize")]
	public class GuiSizerSetDefaultSize : FsmStateAction
	{
		
		public FsmInt defaultWidth;
		public FsmInt defaultHeight;
	
		
		public override void Reset()
		{
			defaultWidth = 1024;
			defaultHeight = 768;
			
		}

		public override void OnEnter()
		{
			GUISizer.WIDTH = defaultWidth.Value;
			GUISizer.HEIGHT = defaultHeight.Value;
			Finish();
		}
	}
}
