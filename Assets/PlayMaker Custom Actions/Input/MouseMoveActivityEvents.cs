// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Input)]
	[Tooltip("Send Events when mouse becomes still and moves after some inactivity, inactivity time can be fine tuned.")]
	public class MouseMoveActivityEvents : FsmStateAction
	{
		
		[Tooltip("Time threshold in seconds under which events are not sent. so the mouse must not move for this duration to trigger a future event when finally moved again")]
		[HasFloatSlider(0, 10)]
		public FsmFloat inactivityThreshold;
		
		[ActionSection("Result")]
		public FsmBool isInactive;
		
		[ActionSection("Events")]
		[Tooltip("Where to send the mouse move event.")]
		public FsmEventTarget eventTarget;
		
		[Tooltip("The event to send when mouse becomes inactive. NOTE: Events must be marked Global to send between FSMs.")]
		public FsmEvent mouseStillEvent;
		
		[Tooltip("The event to send when mouse moves again. NOTE: Events must be marked Global to send between FSMs.")]
		public FsmEvent mouseMoveEvent;
		
		Vector3 _lastPosition;
		float _lastMoveTime;
		bool _isInactive;
		
		public override void Reset()
		{
			inactivityThreshold = 5;
			eventTarget = null;
			mouseStillEvent = null;
			mouseMoveEvent = null;
		}

		public override void OnEnter()
		{
			_lastPosition = Input.mousePosition;
			WatchMouse();

		}

		public override void OnUpdate()
		{
			WatchMouse();
		}
		
		void WatchMouse()
		{
			Vector3 _mousePosition = Input.mousePosition;
			if (!_mousePosition.Equals(_lastPosition))
			{	
				if (_isInactive)
				{
					_isInactive = false;
					isInactive.Value = _isInactive;
					Fsm.Event(eventTarget,mouseMoveEvent);
				}
				
				_lastMoveTime = Time.realtimeSinceStartup;
				
				_lastPosition = _mousePosition;
			}else{
			
				if (Time.realtimeSinceStartup - _lastMoveTime > inactivityThreshold.Value)
				{
					_isInactive = true;
					isInactive.Value = _isInactive;
					Fsm.Event(eventTarget,mouseStillEvent);
				}
				
			}
			
			
		}
	}
}

