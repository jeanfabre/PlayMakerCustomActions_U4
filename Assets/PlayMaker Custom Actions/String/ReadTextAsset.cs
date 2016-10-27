// (c) Copyright HutongGames, LLC 2010-2016. All rights reserved.
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