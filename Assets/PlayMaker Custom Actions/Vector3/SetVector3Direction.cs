// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
// __ECO__ __PLAYMAKER__ __ACTION__

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Vector3)]
	[Tooltip("Sets the value of a Vector3 Variable using direction presets.")]
	public class SetVector3Direction: FsmStateAction
	{
		public enum Vector3Direction {Up,Down,Left,Right,Forward,Back};

		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("The vector3 to store direction")]
		public FsmVector3 vector3Variable;

		[RequiredField]
		[Tooltip("The direction preset")]
		public Vector3Direction direction;
		
		public override void Reset()
		{
			vector3Variable = null;
			direction = Vector3Direction.Up;
		}
		
		public override void OnEnter()
		{
			switch(direction)
			{
			case Vector3Direction.Up:
				vector3Variable.Value = Vector3.up;
				break;
			case Vector3Direction.Down:
				vector3Variable.Value = Vector3.down;
				break;
			case Vector3Direction.Left:
				vector3Variable.Value = Vector3.left;
				break;
			case Vector3Direction.Right:
				vector3Variable.Value = Vector3.right;
				break;
			case Vector3Direction.Forward:
				vector3Variable.Value = Vector3.forward;
				break;
			case Vector3Direction.Back:
				vector3Variable.Value = Vector3.back;
				break;
			}

			Finish();		
		}

	}
}

