// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Input)]
	[Tooltip("Sends events based on 16 polar direction ( NNW, SEE, etc). Expose an option to only send event when direction changed.")]
	public class PolarDirectionEvent : FsmStateAction
	{
		
		[Tooltip("Horizontal axis as defined in the Input Manager")]
		public FsmString horizontalAxis;
		
		[Tooltip("Vertical axis as defined in the Input Manager")]
		public FsmString verticalAxis;
		
		[UIHint(UIHint.Variable)]
		[Tooltip("The direction angle. Range from -180 to 180, 0 being EST")]
		public FsmFloat storeAngle;
		
		[Tooltip("Event to send if direction is North.")]
		public FsmEvent NorthEvent;
		
		[Tooltip("Event to send if direction is North North Est.")]
		public FsmEvent NorthNorthEstEvent;
		
		[Tooltip("Event to send if direction is North Est")]
		public FsmEvent NorthEstEvent;
		
		[Tooltip("Event to send if direction is North Est Est.")]
		public FsmEvent NorthEstEstEvent;
		
		[Tooltip("Event to send if direction is Est.")]
		public FsmEvent EstEvent;
		
		[Tooltip("Event to send if direction is South Est Est.")]
		public FsmEvent SouthEstEstEvent;
		
		[Tooltip("Event to send if direction is South Est.")]
		public FsmEvent SouthEstEvent;
		
		[Tooltip("Event to send if direction is South South Est.")]
		public FsmEvent SouthSouthEstEvent;
		
		[Tooltip("Event to send if direction is South.")]
		public FsmEvent SouthEvent;
		
		[Tooltip("Event to send if direction is South South West.")]
		public FsmEvent SouthSouthWestEvent;
		
		[Tooltip("Event to send if direction is South West.")]
		public FsmEvent SouthWestEvent;
		
		[Tooltip("Event to send if direction is South West West.")]
		public FsmEvent SouthWestWestEvent;
		
		[Tooltip("Event to send if direction is West.")]
		public FsmEvent WestEvent;
		
		[Tooltip("Event to send if direction is North West West.")]
		public FsmEvent NorthWestWestEvent;
		
		[Tooltip("Event to send if direction is North West.")]
		public FsmEvent NorthWestEvent;
		
		[Tooltip("Event to send if direction is North North West.")]
		public FsmEvent NorthNorthWestEvent;
		
		[Tooltip("Event to send if no axis input (centered).")]
		public FsmEvent noDirection;
		
		
		[Tooltip("Only send events when direction changes")]
		public bool discreteEvents;
			
		private int currentDirection =-2;
		
		public override void Reset()
		{
			
			horizontalAxis = "Horizontal";
			verticalAxis = "Vertical";
			storeAngle = null;
			
			
			NorthEvent = null;
			NorthNorthEstEvent = null;
			NorthEstEvent = null;
			NorthEstEstEvent = null;
			EstEvent = null;
			SouthEstEstEvent = null;
			SouthEstEvent = null;
			SouthSouthEstEvent = null;
			SouthEvent = null;
			SouthSouthWestEvent = null;
			SouthWestEvent = null;
			SouthWestWestEvent = null;
			WestEvent = null;
			NorthWestWestEvent = null;
			NorthWestEvent = null;
			NorthNorthWestEvent = null;
			
			noDirection = null;
			discreteEvents = true;
			

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
			
			if (direction == 4 && NorthEvent != null)
			{
				Fsm.Event(NorthEvent);
			} 
			else if (direction == 3 && NorthNorthEstEvent != null)
			{
				Fsm.Event(NorthNorthEstEvent);
			}
			else if (direction == 2 && NorthEstEvent != null)
			{
				Fsm.Event(NorthEstEvent);
			}			
			else if (direction == 1 && NorthEstEstEvent != null)
			{
				Fsm.Event(NorthEstEstEvent);
			}
			else if (direction == 0 && EstEvent != null)
			{
				Fsm.Event(EstEvent);
			}
			else if (direction == 15 && SouthEstEstEvent != null)
			{
				Fsm.Event(SouthEstEstEvent);
			}
			else if (direction == 14 && SouthEstEvent != null)
			{
				Fsm.Event(SouthEstEvent);
			}
			else if (direction == 13 && SouthSouthEstEvent != null)
			{
				Fsm.Event(SouthSouthEstEvent);
			}
			else if (direction == 12 && SouthEvent != null)
			{
				Fsm.Event(SouthEvent);
			}
			else if (direction ==11 && SouthSouthWestEvent != null)
			{
				Fsm.Event(SouthSouthWestEvent);
			}
			else if (direction == 10 && SouthWestEvent != null)
			{
				Fsm.Event(SouthWestEvent);
			}
			else if (direction == 9 && SouthWestWestEvent != null)
			{
				Fsm.Event(SouthWestWestEvent);
			}
			else if (direction == 8 && WestEvent != null)
			{
				Fsm.Event(WestEvent);
			}
			else if (direction == 7 && NorthWestWestEvent != null)
			{
				Fsm.Event(NorthWestWestEvent);
			}
			else if (direction == 6 && NorthWestEvent != null)
			{
				Fsm.Event(NorthWestEvent);
			}
			else if (direction == 5 && NorthNorthWestEvent != null)
			{
				Fsm.Event(NorthNorthWestEvent);
			}
		}
		
		private int GetCurrentDirection()
		{
			
			var x = horizontalAxis.Value != "" ? Input.GetAxis(horizontalAxis.Value) : 0;
			var y = verticalAxis.Value != "" ? Input.GetAxis(verticalAxis.Value) : 0;
			
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
			
			var angle = rawAngle + 11.25f;
			if (angle < 0f) 
			{
				angle += 360f;
			}
			
			
			return (int)(angle / 22.5f);
		}
	}
}

