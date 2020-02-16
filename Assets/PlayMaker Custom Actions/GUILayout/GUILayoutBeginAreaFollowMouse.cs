// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;
using System.Collections.Generic;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.GUILayout)]
	[Tooltip("Begin a GUILayout area that follows the mouse. Useful for overlays (e.g., playerName). NOTE: Block must end with a corresponding GUILayoutEndArea.")]
	public class GUILayoutBeginAreaFollowMouse : FsmStateAction
	{
		[RequiredField]
		public FsmFloat offsetLeft;
		
		[RequiredField]
		public FsmFloat offsetTop;
		
		[RequiredField]
		public FsmFloat width;
		
		[RequiredField]
		public FsmFloat height;
		
		[Tooltip("Use normalized screen coordinates (0-1).")]
		public FsmBool normalized;
		
		[Tooltip("Optional named style in the current GUISkin")]
		public FsmString style;
		
		public override void Reset()
		{

			offsetLeft = 0f;
			offsetTop = 0f;
			width = 1f;
			height = 1f;
			normalized = true;
			style = "";
		}

		public override void OnGUI()
		{

			// get screen position
			
			Vector2 screenPos = Input.mousePosition;
			
			var left = screenPos.x + (normalized.Value ? offsetLeft.Value * Screen.width : offsetLeft.Value);
			var top = screenPos.y + (normalized.Value ? offsetTop.Value * Screen.width : offsetTop.Value);
			
			var rect = new Rect(left, top, width.Value, height.Value);
			
			if (normalized.Value)
			{
				rect.width *= Screen.width;
				rect.height *= Screen.height;
			}
			
			// convert screen coordinates
			rect.y = Screen.height - rect.y;
			
			GUILayout.BeginArea(rect, style.Value);
		}

	}
}
