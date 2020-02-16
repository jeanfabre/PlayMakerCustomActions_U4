// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Quaternion")]
	[Tooltip("Smoothly Rotates the local y axis of a gameObject to a target, rotating around the local Z axis.")]
	public class SmoothPointAtY : FsmStateAction
	{

		[RequiredField]
		[Tooltip("The GameObject to rotate to face a target.")]
		public FsmOwnerDefault gameObject;
		
		[RequiredField]
		[Tooltip("the target")]
		public FsmGameObject targetObject;
		
		[Tooltip("The degrees per seconds to point at the target")]
		public FsmFloat AngleSpeed;
		
		
		[Tooltip("Repeat every frame.")]
		public bool everyFrame;

		public override void Reset()
		{
			gameObject = null;
			targetObject = null;
			AngleSpeed = 90f;
	
			everyFrame = true;
		
		}
		
		GameObject _go;
		
		public override void OnEnter()
		{
			 _go = Fsm.GetOwnerDefaultTarget(gameObject);
			if (_go == null)
			{
				Finish();
				return;
			}

			DoPointAt();
			
			if (!everyFrame)
			{
				Finish();
			}
		}

		public override void OnUpdate()
		{
			DoPointAt();
		}
		
		float smoothAngle;
		

		void DoPointAt()
		{
			var goTarget = targetObject.Value;
			if (goTarget == null && targetObject.IsNone)
			{
				return;
			}

			
			Vector3 targetPos = goTarget.transform.position;
	
		   float ZRot = -Mathf.Atan2(targetPos.x - _go.transform.position.x, targetPos.y - _go.transform.position.y)* (180f / Mathf.PI) ;
			   
		   smoothAngle = Mathf.MoveTowardsAngle(_go.transform.eulerAngles.z, ZRot, AngleSpeed.Value * Time.deltaTime);
			   
			   
		   _go.transform.localEulerAngles = new Vector3(0, 0, smoothAngle);	
		}
	}
}
