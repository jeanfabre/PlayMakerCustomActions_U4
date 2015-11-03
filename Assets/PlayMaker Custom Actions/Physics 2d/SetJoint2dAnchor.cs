// (c) Copyright HutongGames, LLC 2010-2015. All rights reserved.
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{

	[ActionCategory("Physics 2d")]
	[Tooltip("Sets the anchor position of an AnchoredJoint2D component")]
	#if UNITY_4_3 || UNITY_4_4
	[Obsolete("This action is only available starting from Unity 4.5 onward")]
	#endif
	public class SetJoint2dAnchor : FsmStateAction
	{
		[RequiredField]
		[Tooltip("The AnchoredJoint2D target")]
		#if UNITY_4_3 || UNITY_4_4
		#else
		[CheckForComponent(typeof(AnchoredJoint2D))]
		#endif
		public FsmOwnerDefault gameObject;
		
		
		[Tooltip("The anchor position")]
		public FsmVector2 anchor;

		[Tooltip("The anchor position expressed as a vector3, z is ignored. If anchor is define, anchorVector3 is added to it.")]
		public FsmVector3 anchorVector3;

		[Tooltip("Repeat every frame while the state is active.")]
		public bool everyFrame;
		#if UNITY_4_3 || UNITY_4_4
		#else
		AnchoredJoint2D _anchoredJoint2d;
		#endif
		public override void Reset()
		{
			gameObject = null;
			anchor = null;
			anchorVector3 = new FsmVector3() {UseVariable=true};
			everyFrame = false;
		}

		#if UNITY_4_3 || UNITY_4_4
		#else
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

			Vector2 _anchor = anchor.Value;
			if (!anchorVector3.IsNone)
			{
				_anchor.x += anchorVector3.Value.x;
				_anchor.y += anchorVector3.Value.y;
			}
			_anchoredJoint2d.anchor = _anchor;
		}
		#endif
	}
}
