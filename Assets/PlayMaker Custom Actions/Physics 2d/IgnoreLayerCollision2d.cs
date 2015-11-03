// (c) Copyright HutongGames, LLC 2010-2015. All rights reserved.
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Physics 2d")]
	[Tooltip("Choose whether to detect or ignore collisions between a specified pair of layers")]
	public class IgnoreLayerCollision2d : FsmStateAction
	{
	
		[Tooltip("The first layer")]
		[UIHint(UIHint.Layer)]
		public FsmInt layer1;
		
		[Tooltip("The second layer")]
		[UIHint(UIHint.Layer)]
		public FsmInt layer2;

		[Tooltip("Should collisions between these layers be ignored?")]
		public FsmBool ignore;


		public override void Reset()
		{
			layer1 = null;
			layer2 = null;
			ignore = true;
		}
		
		public override void OnEnter()
		{
			Physics2D.IgnoreLayerCollision(layer1.Value,layer2.Value);
			Finish();
		}

	}
}

