// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Input)]
	[Tooltip("Gets the Vector3 Position of the mouse and stores it in a Variable.")]
	public class GetMousePosition : FsmStateAction
	{
		[RequiredField]
		[UIHint(UIHint.Variable)]
		public FsmVector3 mousePosition;
		public bool normalize;
		
		public bool everyFrame;
		
		public override void Reset()
		{
			mousePosition = null;
			normalize = true;
			everyFrame = false;
		}

		public override void OnEnter()
		{
			DoGetMousePosition();
			
			if (!everyFrame)
			{
				Finish();
			}
		}

		public override void OnUpdate()
		{
			DoGetMousePosition();
		}
		
		void DoGetMousePosition()
		{
			if (mousePosition != null)
			{
				Vector3 _mousePosition = Input.mousePosition;
				
				if (normalize)
				{
					_mousePosition.x /= Screen.width;
					_mousePosition.y /= Screen.height;
				}
				mousePosition.Value = _mousePosition;
			}
		}
	}
}

