// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/
// Edited by DjayDino
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Input)]
	[Tooltip("Gets a world direction Vector from 2 Input Axis. Typically used for a third person controller with Relative To set to the camera.")]
	public class GetAxisVectorExtended : FsmStateAction
	{
		public enum AxisPlane
		{
			XZ,
			XY,
			YZ
		}
		
		[Tooltip("The name of the horizontal input axis. See Unity Input Manager.")]
		public FsmString horizontalAxis;
		
		[Tooltip("The name of the vertical input axis. See Unity Input Manager.")]
		public FsmString verticalAxis;
		
		[Tooltip("Input axis are reported in the range -1 to 1, this multiplier lets you set a new range.")]
		public FsmFloat multiplier;
		
		[RequiredField]
		[Tooltip("The world plane to map the 2d input onto.")]
		public AxisPlane mapToPlane;
		
		[Tooltip("Make the result relative to a GameObject, typically the main camera.")]
		public FsmGameObject relativeTo;
		

		[UIHint(UIHint.Variable)]
		[Tooltip("Store the direction vector.")]
		public FsmVector3 storeVector;
		
		[UIHint(UIHint.Variable)]
		[Tooltip("Store the length of the direction vector.")]
		public FsmFloat storeMagnitude;
		
		[UIHint(UIHint.Variable)]
		[Tooltip("Store the angle of the direction vector. Ranges from -180 to 180, 0, being fully vertical, 90, fully right,-90 fully left")]
		public FsmFloat storeAngle;

        [Tooltip("If Uncheck, this will prevent to check the angle when the magnitude is 0 ( both axis are 0)")]
        public bool checkAngleIfAxis0;

		public bool useRawAxis;


		float h;
		float v;

        public override void Reset()
		{
			horizontalAxis = "Horizontal";
			verticalAxis = "Vertical";
			multiplier = 1.0f;
			mapToPlane = AxisPlane.XZ;
			storeVector = null;
			storeMagnitude = null;
			storeAngle = null;
            checkAngleIfAxis0 = true;
			useRawAxis = false;
		}

		public override void OnUpdate()
		{
			var forward = new Vector3();
			var right = new Vector3();
			
			if (relativeTo.Value == null)
			{
				switch (mapToPlane) 
				{
				case AxisPlane.XZ:
					forward = Vector3.forward;
					right = Vector3.right;
					break;
					
				case AxisPlane.XY:
					forward = Vector3.up;
					right = Vector3.right;
					break;
					
				case AxisPlane.YZ:
					forward = Vector3.up;
					right = Vector3.forward;
					break;
				}
			}
			else
			{
				var transform = relativeTo.Value.transform;
				
				switch (mapToPlane) 
				{
				case AxisPlane.XZ:
					forward = transform.TransformDirection(Vector3.forward);
					forward.y = 0;
					forward = forward.normalized;
					right = new Vector3(forward.z, 0, -forward.x);
					break;
					
				case AxisPlane.XY:
				case AxisPlane.YZ:
					// NOTE: in relative mode XY ans YZ are the same!
					forward = Vector3.up;
					forward.z = 0;
					forward = forward.normalized;
					right = transform.TransformDirection(Vector3.right);
					break;
				}
				
				// Right vector relative to the object
				// Always orthogonal to the forward vector
				
			}
			
			// get individual axis
			// leaving an axis blank or set to None sets it to 0


			h = (horizontalAxis.IsNone || string.IsNullOrEmpty(horizontalAxis.Value)) ? 0f : GetInputAxis(horizontalAxis.Value);
			v = (verticalAxis.IsNone || string.IsNullOrEmpty(verticalAxis.Value)) ? 0f : GetInputAxis(verticalAxis.Value);


			// calculate resulting direction vector

			var direction = h * right + v * forward;
			direction *= multiplier.Value;
			
			storeVector.Value = direction;
			
			if (!storeMagnitude.IsNone)
			{
				storeMagnitude.Value = direction.magnitude;
			}
			
			if (! storeAngle.IsNone)
			{
                if(checkAngleIfAxis0)
                {
                    storeAngle.Value = Mathf.Atan2(h, v) * Mathf.Rad2Deg;
                }
                else
                {
                    if (direction.magnitude > 0f)
                    {
                        storeAngle.Value = Mathf.Atan2(h, v) * Mathf.Rad2Deg;
                    }
                }
            }
        }

		float GetInputAxis(string axis)
		{
			if (useRawAxis) {
				return Input.GetAxisRaw(axis);
			}

			return  Input.GetAxis(axis);
		}

	}
}
