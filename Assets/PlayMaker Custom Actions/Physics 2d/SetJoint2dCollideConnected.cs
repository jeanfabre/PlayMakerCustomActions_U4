// (c) Copyright HutongGames, LLC 2010-2015. All rights reserved.
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Physics 2d")]
	[Tooltip("Sets the various properties of a WheelJoint2d component")]
	public class SetJoint2dcollideConnected : FsmStateAction
	{
		
		[RequiredField]
		[Tooltip("The Joint2d target")]
		[CheckForComponent(typeof(Joint2D))]
		public FsmOwnerDefault gameObject;

		
		[Tooltip("Can the joint collide with the other Rigidbody2D object to which it is attached?")]
		[RequiredField]
		public FsmBool collideConnected;

		Joint2D _joint2d;

		public override void Reset()
		{
			collideConnected = null;
		}
		
		public override void OnEnter()
		{
			var go = Fsm.GetOwnerDefaultTarget(gameObject);
			
			if (go != null)
			{
				_joint2d = go.GetComponent<Joint2D>();
			}
			
			SetProperty();

				Finish();
		}

		
		void SetProperty()
		{
			if(_joint2d==null)
			{
				return;
			}
			
			_joint2d.collideConnected = collideConnected.Value;
		}
		
	}
}
