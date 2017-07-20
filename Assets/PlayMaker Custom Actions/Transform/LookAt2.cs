// (c) Copyright HutongGames, LLC 2010-2014. All rights reserved.
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Transform)]
	[Tooltip("Rotates a Game Object so its forward vector points at a Target. The Target can be specified as a GameObject or a world Position. If you specify both, then Position specifies a local offset from the target object's Position. Optionnaly let you define the look in local space, so that 'keep vertical' works locally")]
	public class LookAt2 : FsmStateAction
	{
		[RequiredField]
		[Tooltip("The GameObject to rotate.")]
		public FsmOwnerDefault gameObject;

		[Tooltip("The GameObject to Look At.")]
		public FsmGameObject targetObject;
		
		[Tooltip("World position to look at, or local offset from Target Object if specified.")]
		public FsmVector3 targetPosition;

		[Tooltip("Rotate the GameObject to point its up direction vector in the direction hinted at by the Up Vector. See Unity Look At docs for more details.")]
		public FsmVector3 upVector;
		
		[Tooltip("Don't rotate vertically.")]
		public FsmBool keepVertical;
		
		[Tooltip("If Keep vertical is true, constraintLocally will use the local vertical axis, not the world vertical axis.")]
		public FsmBool constraintLocally;
		
		[Title("Draw Debug Line")]
		[Tooltip("Draw a debug line from the GameObject to the Target.")]
		public FsmBool debug;

		[Tooltip("Color to use for the debug line.")] 
		public FsmColor debugLineColor;

		[Tooltip("Repeat every frame.")]
		public bool everyFrame = true;

	    private GameObject go;
	    private GameObject goTarget;
	    private Vector3 lookAtPos;
	    private Vector3 lookAtPosWithVertical;
        
		private Vector3 upVectorValue;
		public override void Reset()
		{
			gameObject = null;
			targetObject = null;
			targetPosition = new FsmVector3 { UseVariable = true};
			upVector = new FsmVector3 { UseVariable = true};
			keepVertical = true;
			constraintLocally = true;
			debug = false;
			debugLineColor = Color.yellow;
			everyFrame = true;
		}

		public override void OnPreprocess()
		{
			#if PLAYMAKER_1_8_5_OR_NEWER
			Fsm.HandleLateUpdate = true;
			#endif
		}

		public override void OnEnter()
		{
			DoLookAt();

			if (!everyFrame)
			{
				Finish();
			}
		}

		public override void OnLateUpdate()
		{
			DoLookAt();
		}

		void DoLookAt()
		{
			if (!UpdateLookAtPosition())
			{
			    return;
			}
			
			go.transform.LookAt(lookAtPos, upVectorValue);			
			
			if (debug.Value)
			{
				Debug.DrawLine(go.transform.position, go.transform.position + upVectorValue,Color.red);
				Debug.DrawLine(go.transform.position, lookAtPos, debugLineColor.Value);
			}
		}

        public bool UpdateLookAtPosition()
        {
            go = Fsm.GetOwnerDefaultTarget(gameObject);
            if (go == null)
            {
                return false;
            }

            goTarget = targetObject.Value;
            if (goTarget == null && targetPosition.IsNone)
            {
                return false;
            }

            if (goTarget != null)
            {
                lookAtPos = !targetPosition.IsNone ? goTarget.transform.TransformPoint(targetPosition.Value) : goTarget.transform.position;
            }
            else
            {
                lookAtPos = targetPosition.Value;
            }
			
			upVectorValue = upVector.IsNone ? Vector3.up : upVector.Value;
			
            lookAtPosWithVertical = lookAtPos;

            if (keepVertical.Value)
            {
				if (constraintLocally.Value)
				{
					lookAtPos = go.transform.InverseTransformPoint(lookAtPos);
					lookAtPos.y = 0;
					lookAtPos = go.transform.TransformPoint(lookAtPos);
					
					upVectorValue = go.transform.TransformPoint(upVector.Value);
				}else{
					lookAtPos.y = go.transform.position.y;
				} 
            }

            return true;
        }

        public Vector3 GetLookAtPosition()
        {
            return lookAtPos;
        }

        public Vector3 GetLookAtPositionWithVertical()
        {
            return lookAtPosWithVertical;
        }
	}
}
