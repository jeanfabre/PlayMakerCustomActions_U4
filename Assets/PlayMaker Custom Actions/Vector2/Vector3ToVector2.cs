// (c) Copyright HutongGames, LLC 2010-2014. All rights reserved.
// original action by collidernyc (http://hutonggames.com/playmakerforum/index.php?topic=7769.0)
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Vector2)]
	[Tooltip("Store a Vector3 XY component into a Vector2 XY component. Drops the Vector3 z component, you can optionally save it to a float.")]
	public class Vector3toVector2 : FsmStateAction
	{
		
		[UIHint(UIHint.Variable)]
		[Tooltip("The vector3")]
		public FsmVector3 vector3;

		[UIHint(UIHint.Variable)]
		[Tooltip("The vector2")]
		public FsmVector2 vector2;

		[UIHint(UIHint.Variable)]
		[Tooltip("Optional z value stored as float")]
		public FsmFloat zValue;

		public bool everyFrame;
		
		public override void Reset()
		{
			vector2 = null;
			vector3 = null;
			zValue = null;
			everyFrame = false;
		}
		
		public override void OnEnter()
		{
			ConvertVector3toVector2();
			
			if (!everyFrame)
			{
				Finish();
			}
		}
		
		public override void OnUpdate()
		{
			ConvertVector3toVector2();
		}

		void ConvertVector3toVector2()
		{
			vector2.Value = new Vector2(vector3.Value.x,vector3.Value.y);
			
			if (!zValue.IsNone)
			{
				zValue.Value = vector3.Value.z;
			}
		}
	}
}
