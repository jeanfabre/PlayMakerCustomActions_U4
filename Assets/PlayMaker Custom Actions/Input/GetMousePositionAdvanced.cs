// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Input)]
	[Tooltip("Get Mouse Position in screen space.")]
	public class GetMousePositionAdvanced : FsmStateAction
	{
		[RequiredField]
		[Tooltip("Store the position in this Vector3")]
		public FsmVector3 storeMousePosition;

		[Tooltip("Do this every frame?")]
		public bool everyFrame;

		[Tooltip("Get the position relative to the center of the screen? (note: doesn't work with Invert Y checked)")]
		public bool fromScreenCenter;

		[Tooltip("Invery Y. For use with GUI space.")]
		public bool invertY;

		[Tooltip("Normalize the Vector3 result?")]
		public bool normalized;

		
		public override void Reset()
		{
			storeMousePosition = new FsmVector3 { UseVariable = true };
			everyFrame = false;
			fromScreenCenter = false;
			normalized = false;
			invertY = false;
		}
		
		public override void OnEnter()
		{
			DoGetPosition();

			if (!everyFrame)
			{
				Finish();
			}
		}

		public override void OnUpdate()
		{
			DoGetPosition();
		}

		void DoGetPosition()
		{
			var mousePos = Input.mousePosition;
			if (invertY)
			{
				mousePos.y = Screen.height - mousePos.y;
			}

			if (fromScreenCenter)
			{
				mousePos.x -= Screen.width/2;
				mousePos.y -= Screen.height/2;
			}

			if (normalized)
			{
				mousePos.x /= Screen.width;
				mousePos.y /= Screen.height;
			}

			storeMousePosition.Value = mousePos;
		}
	}
}
