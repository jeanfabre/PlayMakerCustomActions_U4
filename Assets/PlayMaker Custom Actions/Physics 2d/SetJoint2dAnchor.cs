// (c) Copyright HutongGames, LLC 2010-2014. All rights reserved.
//--- __ECO__ __ACTION__ ---//

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Physics 2d")]
	[Tooltip("Sets the anchor position of an AnchoredJoint2D component")]
	public class SetJoint2dAnchor : FsmStateAction
	{
		
		[RequiredField]
		[Tooltip("The AnchoredJoint2D target")]
		[CheckForComponent(typeof(AnchoredJoint2D))]
		public FsmOwnerDefault gameObject;
		
		
		[Tooltip("The anchor position")]
		public FsmVector2 anchor;

		[Tooltip("The anchor position expressed as a vector3, z is ignored. If anchor is define, anchorVector3 is added to it.")]
		public FsmVector3 anchorVector3;

		[Tooltip("Repeat every frame while the state is active.")]
		public bool everyFrame;

		AnchoredJoint2D _anchoredJoint2d;
		
		public override void Reset()
		{
			gameObject = null;
			anchor = null;
			anchorVector3 = new FsmVector3() {UseVariable=true};
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

			Vector2 _anchor = anchor.Value;
			if (!anchorVector3.IsNone)
			{
				_anchor.x += anchorVector3.Value.x;
				_anchor.y += anchorVector3.Value.y;
			}
			_anchoredJoint2d.anchor = _anchor;
		}
		
	}
}