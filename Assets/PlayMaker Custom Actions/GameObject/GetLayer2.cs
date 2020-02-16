// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.GameObject)]
	[Tooltip("Gets a Game Object's Layer and stores it in an Int Variable or a String Variable.")]
	public class GetLayer2 : FsmStateAction
	{
		[RequiredField]
		public FsmGameObject gameObject;
		[UIHint(UIHint.Variable)]
		public FsmInt storeIntValue;
		public FsmString storeNameValue;
		public bool everyFrame;
		
		public override void Reset()
		{
			gameObject = null;
			storeIntValue = null;
			storeNameValue = null;
			everyFrame = false;
		}

		public override void OnEnter()
		{
			DoGetLayer();
			
			if (!everyFrame)
				Finish();
		}
		
		public override void OnUpdate()
		{
			DoGetLayer();
		}
		
		void DoGetLayer()
		{
			if (gameObject.Value == null) return;
			
			storeIntValue.Value = gameObject.Value.layer;
			storeNameValue.Value = LayerMask.LayerToName(gameObject.Value.layer);

		}
	}
}

















