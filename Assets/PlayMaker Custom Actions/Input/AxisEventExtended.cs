// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Input)]
	[Tooltip("Sends events based on the direction of Input Axis (Left/Right/Up/Down...). Also sends intermedidate directions ( left-Right). Expose an option to only send event when direction changed.")]
	public class AxisEventExtended : FsmStateAction
	{
		[Tooltip("Horizontal axis as defined in the Input Manager")]
		public FsmString horizontalAxis;
		
		[Tooltip("Vertical axis as defined in the Input Manager")]
		public FsmString verticalAxis;

        [Tooltip("Set a tolerance when the event should trigger (deadzone) This must be a value between 0 and 1")]
        public FsmFloat tolerance;

        [UIHint(UIHint.Variable)]
		[Tooltip("The direction angle. Range from -180 to 180, 0 being full up")]
		public FsmFloat storeAngle;
		
		[Tooltip("Event to send if input is to the left.")]
		public FsmEvent leftEvent;
		
		[Tooltip("Event to send if input is to the right.")]
		public FsmEvent rightEvent;
		
		[Tooltip("Event to send if input is to the up.")]
		public FsmEvent upEvent;
		
		[Tooltip("Event to send if input is to the down.")]
		public FsmEvent downEvent;
		
		[Tooltip("Event to send if input is to the up left.")]
		public FsmEvent upLeftEvent;
		
		[Tooltip("Event to send if input is to the up right.")]
		public FsmEvent upRightEvent;
		
		[Tooltip("Event to send if input is to the down left.")]
		public FsmEvent downLeftEvent;
		
		[Tooltip("Event to send if input is to the down right.")]
		public FsmEvent downRightEvent;
		
		[Tooltip("Event to send if no axis input (centered).")]
		public FsmEvent noDirection;
		
		[Tooltip("Only send events when direction changes")]
		public bool discreteEvents;
			
		private int currentDirection =-2;
        private float x;
        private float y;

        public override void Reset()
		{
			horizontalAxis = "Horizontal";
			verticalAxis = "Vertical";
			leftEvent = null;
			rightEvent = null;
			upEvent = null;
			downEvent = null;
			upLeftEvent = null;
			upRightEvent = null;
			downLeftEvent = null;
			downRightEvent = null;
			noDirection = null;
			discreteEvents = true;
            tolerance = 0;
        }
		
		public override void OnEnter()
		{
			currentDirection = GetCurrentDirection();
		}
		
		public override void OnUpdate()
		{
			int direction = GetCurrentDirection();
			
			if (currentDirection==direction && discreteEvents)
			{
				return;	
			}
			
			
			if (direction<0 && noDirection != null)
			{
				Fsm.Event(noDirection);
			}
			// send events bases on direction
			
			if (direction == 0 && rightEvent != null && x >= tolerance.Value)
			{
				Fsm.Event(rightEvent);
				//Debug.Log("Right");
			} 
			else if (direction == 1 && upRightEvent != null && x >= tolerance.Value && y >= tolerance.Value)
			{
				Fsm.Event(upRightEvent);
				//Debug.Log("UpRight");
			}
			else if (direction == 2 && upEvent != null && y >= tolerance.Value)
			{
				Fsm.Event(upEvent);
				//Debug.Log("Up");
			}			
			else if (direction == 3 && upLeftEvent != null && y >= tolerance.Value && x <= tolerance.Value * -1)
			{
				Fsm.Event(upLeftEvent);
				//Debug.Log("upLeftEvent");
			}
				else if (direction == 4 && leftEvent != null && x <= tolerance.Value * -1)
			{
				Fsm.Event(leftEvent);
				//Debug.Log("LeftEvent");
			}
				else if (direction == 5 && downLeftEvent != null && y <= tolerance.Value * -1 && x <= tolerance.Value * -1)
			{
				Fsm.Event(downLeftEvent);
				//Debug.Log("downLeftEvent");
			}
				else if (direction == 6 && downEvent != null && y <= tolerance.Value * -1)
			{
				Fsm.Event(downEvent);
				//Debug.Log("downEvent");
			}
				else if (direction == 7 && downRightEvent != null && y <= tolerance.Value * -1 && x >= tolerance.Value)
			{
				Fsm.Event(downRightEvent);
				//Debug.Log("downRightEvent");
			}
		}
		
		private int GetCurrentDirection()
		{
			x = horizontalAxis.Value != "" ? Input.GetAxis(horizontalAxis.Value) : 0;
			y = verticalAxis.Value != "" ? Input.GetAxis(verticalAxis.Value) : 0;
			
			// get squared offset from center
			
			var offset = (x * x) + (y * y);
			
			// no offset?
			
			if (offset.Equals(0))
			{
				if (!storeAngle.IsNone)
				{
					storeAngle.Value = 0;
				}
				return -1;
			}

			float rawAngle = (Mathf.Atan2(y, x) * Mathf.Rad2Deg);
			if (! storeAngle.IsNone)
			{
				storeAngle.Value = rawAngle;
			}
			
			var angle = rawAngle + 22.5f;
			if (angle < 0f) 
			{
				angle += 360f;
			}
			
			return (int)(angle / 45);
		}
	}
}

