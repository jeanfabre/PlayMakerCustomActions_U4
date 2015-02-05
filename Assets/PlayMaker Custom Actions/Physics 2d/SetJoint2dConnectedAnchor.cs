// (c) Copyright HutongGames, LLC 2010-2014. All rights reserved.
//--- __ECO__ __ACTION__ ---//

using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	#if UNITY_4_3 || UNITY_4_4
	[Obsolete("This action is only available starting from Unity 4.5 onward")]
	#endif
	[ActionCategory("Physics 2d")]
	[Tooltip("Sets the joint's anchor point on the second object (ie, the one which doesn't have the joint component).")]
	public class SetJoint2dConnectedAnchor : FsmStateAction
	{
		
		[RequiredField]
		[Tooltip("The AnchoredJoint2D target")]
		#if UNITY_4_3 || UNITY_4_4
		#else
		[CheckForComponent(typeof(AnchoredJoint2D))]
		#endif
		public FsmOwnerDefault gameObject;
		
		
		[Tooltip("The joint's anchor point on the second object (ie, the one which doesn't have the joint component).")]
		public FsmVector2 connectedAnchor;
		
		[Tooltip("The anchor position expressed as a vector3, z is ignored. If anchor is define, anchorVector3 is added to it.")]
		public FsmVector3 connectedAnchorVector3;
		
		[Tooltip("Repeat every frame while the state is active.")]
		public bool everyFrame;
		#if UNITY_4_3 || UNITY_4_4
		#else
		AnchoredJoint2D _anchoredJoint2d;
		
		public override void Reset()
		{
			gameObject = null;
			connectedAnchor = null;
			connectedAnchorVector3 = new FsmVector3() {UseVariable=true};
			everyFrame = false;
		}
		
		public override void OnEnter()
		{
			var go = Fsm.GetOwnerDefaultTarget(gameObject);
			
			if (go != null)
			{
				_anchoredJoint2d = go.GetComponent<AnchoredJoint2D>();
			}
			
			SetProperty();
			
			if(!everyFrame)
			{
				Finish();
			}
		}
		
		public override void OnUpdate()
		{
			SetProperty();
		}
		
		void SetProperty()
		{
			if(_anchoredJoint2d==null)
			{
				return;
			}
			
			Vector2 _connectedAnchor = connectedAnchor.Value;
			if (!connectedAnchorVector3.IsNone)
			{
				_connectedAnchor.x += connectedAnchorVector3.Value.x;
				_connectedAnchor.y += connectedAnchorVector3.Value.y;
			}
			_anchoredJoint2d.connectedAnchor = _connectedAnchor;
		}
	#endif
	}
}