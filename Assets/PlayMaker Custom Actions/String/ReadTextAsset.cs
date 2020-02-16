// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.String)]
	[Tooltip("Get the content of a text asset")]
	public class ReadTextAsset : FsmStateAction
	{
		[RequiredField]
		[Tooltip("The text asset")]
		public TextAsset textAsset;
		
		[Tooltip("The content of the text asset")]
		public FsmString content;
		
		
		public override void Reset()
		{
			textAsset = null;
			content = "";
		}

		public override void OnEnter()
		{
			
			if (textAsset!=null)
			{
				content.Value = textAsset.text;
			}
			
			Finish();

		}
	}
}