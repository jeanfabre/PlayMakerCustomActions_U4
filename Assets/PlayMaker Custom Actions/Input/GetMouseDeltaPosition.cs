// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Input)]
	[Tooltip("Gets the delta Position of the mouse as a Vector3, vector2, or individual axis.")]
	public class GetMouseDeltaPosition : FsmStateAction
	{
		
		[UIHint(UIHint.Variable)]
		public FsmVector3 mouseDeltaPosition;
		
		[UIHint(UIHint.Variable)]
		public FsmVector2 mousePositionDeltaVector2;
		
		[UIHint(UIHint.Variable)]
		public FsmFloat mouseDeltaPositionX;
		
		[UIHint(UIHint.Variable)]
		public FsmFloat mouseDeltaPositionY;
		
		public bool normalize;
		
		Vector3 _lastPosition;
			
		public override void Reset()
		{
			mouseDeltaPosition = null;
			mousePositionDeltaVector2 = null;
			mouseDeltaPositionX = null;
			mouseDeltaPositionY =null;
			normalize = false;
		}

		public override void OnEnter()
		{
			_lastPosition = Input.mousePosition;
			DoGetMouseDelta();

		}

		public override void OnUpdate()
		{
			DoGetMouseDelta();
		}
		
		void DoGetMouseDelta()
		{
		
			
			Vector3 _mousePosition = Input.mousePosition;
			
			Vector3 _delta = _mousePosition-_lastPosition;
		
			if (normalize)
			{
				_delta.x /= Screen.width;
				_delta.y /= Screen.height;
			}
		
			if (!mouseDeltaPosition.IsNone)
			{
				mouseDeltaPosition.Value = _delta;
			}
			
			if (!mousePositionDeltaVector2.IsNone)
			{
				mousePositionDeltaVector2.Value = new Vector2(_delta.x,_delta.y);
			}
			
			if (!mouseDeltaPositionX.IsNone)
			{
				mouseDeltaPositionX.Value = _delta.x;

			}
			if (!mouseDeltaPositionY.IsNone)
			{
				mouseDeltaPositionY.Value = _delta.y;
			}
			
			_lastPosition = _mousePosition;
		}
	}
}

