// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Physics)]
	[Tooltip("Shoot a RigidBody. Optionnaly set a target gameObject to show the impact point")]
	public class PhysicsShoot : FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(Rigidbody))]
		public FsmOwnerDefault gameObject;
		
		[RequiredField]
		[Tooltip("the range of the shoot in the forward direction")]
		public FsmFloat range;
		
		[RequiredField]
		[Tooltip("The shoot angle")]
		public FsmFloat angle;
		
		[Tooltip("The optional target gameObject to show the impact point")]
		public FsmGameObject target;
		

		public override void Reset()
		{
			range = 20f;
			angle = 65f;
			target = null;
		}

		public override void OnEnter()
		{
			var go = Fsm.GetOwnerDefaultTarget(gameObject);
			if (go == null)
			{
				return;
			}

			if (go.rigidbody == null)
			{
				LogWarning("Missing rigid body: " + go.name);
				return;
			}

			
			float Angle = angle.Value;
			float Range = range.Value;
			
			// Calculate trajectory velocity
       		 Vector3 v = new Vector3(0, Mathf.Sin(Angle * Mathf.Deg2Rad), Mathf.Cos(Angle * Mathf.Deg2Rad));
			
			v = Quaternion.AngleAxis(go.transform.eulerAngles.y, Vector3.up) * v;
			
        	go.rigidbody.velocity = v * Mathf.Sqrt(Range * Physics.gravity.magnitude / Mathf.Sin(2f*Angle*Mathf.Deg2Rad));
        
        	// Position another object at the target range to verify that the trajectory calculate works.
        	if (target.Value != null)
			{
          	  target.Value.transform.position = go.transform.position + go.transform.forward * Range;
			}
			
			Finish();		
		}


	}
}