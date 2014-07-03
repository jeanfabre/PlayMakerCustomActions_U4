// (c) Copyright HutongGames, LLC 2010-2014. All rights reserved.
//--- __ECO__ __ACTION__ ---//

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Physics 2d")]
	[Tooltip("Sets the The Rigidbody2D object to which the other end of the joint is attached (ie, the object without the joint component).")]
	public class SetJoint2dConnectedBody : FsmStateAction
	{
		
		[RequiredField]
		[Tooltip("The Joint2d target")]
		[CheckForComponent(typeof(Joint2D))]
		public FsmOwnerDefault gameObject;
		
		
		[Tooltip("The Rigidbody2D object to which the other end of the joint is attached")]
		[CheckForComponent(typeof(Rigidbody2D))]
		public FsmGameObject connectedBody;
		
		Joint2D _joint2d;
		
		public override void Reset()
		{
			connectedBody = null;
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

			GameObject _go = connectedBody.Value;
			if (_go!=null)
			{
				Rigidbody2D _rb2d = _go.GetComponent<Rigidbody2D>();
				if(_rb2d!=null)
				{
					_joint2d.connectedBody = _rb2d;
					return;
				}
			}
			
			_joint2d.connectedBody = null;
		}
		
	}
}