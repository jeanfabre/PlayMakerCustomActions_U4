// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/
// Made By : Djaydino

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory(ActionCategory.Input)]
    [Tooltip("Sends an event based on the  inactive direction of Input Axis")]
    public class AxisEndedEvent : FsmStateAction
    {
        enum Directions { left, right, up, down, upLeft, upRight, downLeft, downRight }

        [Tooltip("Horizontal axis as defined in the Input Manager if no axis set, the value will be 0")]
        public FsmString horizontalAxis;

        [Tooltip("Vertical axis as defined in the Input Manager if no axis set, the value will be 0")]
        public FsmString verticalAxis;

        [Tooltip("The direction to check")]
        [ObjectType(typeof(Directions))]
        public FsmEnum directionToCheck;

        [Tooltip("Set a tolerance when the event should trigger (deadzone) This must be a value between 0 and 1")]
        public FsmFloat tolerance;

        [Tooltip("Check if the other axis is active, this only works for Left / Right / Up / Down")]
        public FsmBool checkOtherAxis;

        [UIHint(UIHint.Variable)]
        public FsmBool directionInactive;

        [Tooltip("Event to send if input is to the left.")]
        public FsmEvent endedEvent;

        [Tooltip("Event to send if input is to the left.")]
        public FsmEvent otherAxisActive;
        private float x;
        private float y;

        public override void Reset()
        {
            horizontalAxis = "Horizontal";
            verticalAxis = "Vertical";
            endedEvent = null;
            directionToCheck = null;
            tolerance = 0;
            directionInactive = false;
            checkOtherAxis = false;
            otherAxisActive = null;
        }

        public override void OnUpdate()
        {
            // get axes offsets
            if (!horizontalAxis.IsNone || horizontalAxis.Value != "")
            {
                x = Input.GetAxis(horizontalAxis.Value);
            }
            else x = 0;
           if(!verticalAxis.IsNone || verticalAxis.Value != "")
            {
                y = Input.GetAxis(verticalAxis.Value);
            }
            else y = 0;

            switch ((Directions)directionToCheck.Value)
            {

                case Directions.up:
                    if (y > tolerance.Value) directionInactive.Value = false;
                    else
                    {
                        directionInactive.Value = true;
                        Fsm.Event(endedEvent);
                    }
                    if (checkOtherAxis.Value && (x < tolerance.Value * -1 || x > tolerance.Value)) Fsm.Event(otherAxisActive);
                    break;

                case Directions.down:
                    if (y < tolerance.Value * -1) directionInactive.Value = false;
                    else
                    {
                        directionInactive.Value = true;
                        Fsm.Event(endedEvent);
                    }
                    if (checkOtherAxis.Value && (x < tolerance.Value * -1 || x > tolerance.Value)) Fsm.Event(otherAxisActive);
                    break;

                case Directions.left:
                    if (x < tolerance.Value * -1) directionInactive.Value = false;
                    else
                    {
                        directionInactive.Value = true;
                        Fsm.Event(endedEvent);
                    }
                    if (checkOtherAxis.Value && (y < tolerance.Value * -1 || y > tolerance.Value)) Fsm.Event(otherAxisActive);
                    break;

                case Directions.right:
                    if (x > tolerance.Value) directionInactive.Value = false;
                    else
                    {
                        directionInactive.Value = true;
                        Fsm.Event(endedEvent);
                    }
                    if (checkOtherAxis.Value && (y < tolerance.Value * -1 || y > tolerance.Value)) Fsm.Event(otherAxisActive);
                    break;

                case Directions.upLeft:
                    if (y > tolerance.Value && x < tolerance.Value * -1) directionInactive.Value = false;
                    else
                    {
                        directionInactive.Value = true;
                        Fsm.Event(endedEvent);
                    }
                    break;

                case Directions.upRight:
                    if (y > tolerance.Value && x > tolerance.Value) directionInactive.Value = false;
                    else
                    {
                        directionInactive.Value = true;
                        Fsm.Event(endedEvent);
                    }
                    break;

                case Directions.downLeft:
                    if (y < tolerance.Value * -1 && x < tolerance.Value * -1) directionInactive.Value = false;
                    else
                    {
                        directionInactive.Value = true;
                        Fsm.Event(endedEvent);
                    }
                    break;

                case Directions.downRight:
                    if (y < tolerance.Value * -1 && x > tolerance.Value) directionInactive.Value = false;
                    else
                    {
                        directionInactive.Value = true;
                        Fsm.Event(endedEvent);
                    }
                    break;
            }
        }
    }
}

