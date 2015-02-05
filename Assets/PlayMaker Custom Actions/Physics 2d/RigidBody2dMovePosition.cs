// (c) Copyright HutongGames, LLC 2010-2015. All rights reserved.
/*--- __ECO__ __ACTION__ ---*/

using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	#pragma warning disable 219
	#if UNITY_4_3 || UNITY_4_4
	[Obsolete("This action is only available starting from Unity 4.5 onward")]
	#endif
	[Tooltip("Moves a Game Object's Rigid Body 2D to a new world position. To leave any axis unchanged, set variable to 'None'.")]
	public class RigidBody2dMovePosition : FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(Rigidbody2D))]
		public FsmOwnerDefault gameObject;

		[UIHint(UIHint.Variable)]
		public FsmVector2 vector;

		public FsmFloat x;
		public FsmFloat y;

		public bool everyFrame;

		Rigidbody2D _rb2d;

		public override void Reset()
		{
			gameObject = null;
			vector = null;
			// default axis to variable dropdown with None selected.
			x = new FsmFloat { UseVariable = true };
			y = new FsmFloat { UseVariable = true };

			everyFrame = false;
		}

		public override void Awake()
		{
			Fsm.HandleFixedUpdate = true;
		}

		public override void OnEnter()
		{
			GetRb();
		}

		public override void OnFixedUpdate()
		{
			DoMovePosition();

			if (!everyFrame)
				Finish();	
		
		}

		void DoMovePosition()
		{
		
			if (_rb2d==null)
			{
				return;
			}

			// init position
			
			Vector3 position =  new Vector3(vector.Value.x,vector.Value.y,_rb2d.transform.position.z);

			if (vector.IsNone)
			{
					position = _rb2d.transform.position;
			}
			
			// override any axis

			if (!x.IsNone) position.x = x.Value;
			if (!y.IsNone) position.y = y.Value;

			// apply

			Vector2 position2d = new Vector3(position.x,position.y);
			#if UNITY_4_3 || UNITY_4_4
			#else
			_rb2d.MovePosition(position2d);
			#endif
		
		}

		void GetRb()
		{
			GameObject go = Fsm.GetOwnerDefaultTarget(gameObject);
			if (go == null) return;
			if (go.rigidbody2D == null) return;

			_rb2d = go.rigidbody2D;
		}
	}
}