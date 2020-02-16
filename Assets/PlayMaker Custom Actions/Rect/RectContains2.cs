// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Rect)]
	[Tooltip("Tests if a point is inside a rectangle. It also takes in account negative width and height")]
	public class RectContains2 : FsmStateAction
	{
		[RequiredField]
		[Tooltip("Rectangle to test.")]
		public FsmRect rectangle;

		[Tooltip("Point to test.")]
		public FsmVector3 point;

		[Tooltip("Specify/override X value.")]
		public FsmFloat x;

		[Tooltip("Specify/override Y value.")]
		public FsmFloat y;

		//[ActionSection("")]

		[Tooltip("Event to send if the Point is inside the Rectangle.")]
		public FsmEvent trueEvent;

		[Tooltip("Event to send if the Point is outside the Rectangle.")]
		public FsmEvent falseEvent;

		[UIHint(UIHint.Variable)]
		[Tooltip("Store the result in a variable.")]
		public FsmBool storeResult;

		//[ActionSection("")]

		[Tooltip("Repeat every frame.")]
		public bool everyFrame;

		public override void Reset()
		{
			rectangle = new FsmRect { UseVariable = true };
			point = new FsmVector3 { UseVariable = true };
			x = new FsmFloat { UseVariable = true };
			y = new FsmFloat { UseVariable = true };
			storeResult = null;
			trueEvent = null;
			falseEvent = null;
			everyFrame = false;
		}

		public override void OnEnter()
		{
			DoRectContains();

			if (!everyFrame)
			{
				Finish();
			}
		}

		public override void OnUpdate()
		{
			DoRectContains();
		}

		void DoRectContains()
		{
			if (rectangle.IsNone)
			{
				return;
			}

			// get point to test from inputs

			var testPoint = point.Value;

			if (!x.IsNone)
			{
				testPoint.x = x.Value;
			}

			if (!y.IsNone)
			{
				testPoint.y = y.Value;
			}
			
			Rect _rect = rectangle.Value;
			if (_rect.width<0)
			{
				_rect.x = rectangle.Value.x + rectangle.Value.width;
				_rect.width = - rectangle.Value.width;
			}
			if (_rect.height<0)
			{
				_rect.y = rectangle.Value.y + rectangle.Value.height;
				_rect.height = - rectangle.Value.height;
			}
			
			// do results

			var contained = _rect.Contains(testPoint);

			storeResult.Value = contained;

			Fsm.Event(contained ? trueEvent : falseEvent);
		}
	}
}
