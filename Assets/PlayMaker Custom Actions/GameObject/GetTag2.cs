// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.GameObject)]
	[Tooltip("Gets a Game Object's Tag and stores it in a String Variable.")]
	public class GetTag2 : FsmStateAction
	{
		[Tooltip("The GameObject to get the tag from.")]
		[RequiredField]
		public FsmOwnerDefault gameObject;

		[Tooltip("The tag of the GameObject")]
		[RequiredField]
		[UIHint(UIHint.Variable)]
		public FsmString storeResult;

		[Tooltip("runs everyframe, useful if value is expected to change often")]
		public bool everyFrame;
		
		private GameObject _go;
		
		public override void Reset()
		{
			gameObject = null;
			storeResult = null;
			everyFrame = false;
		}
		
		public override void OnEnter()
		{
			DoGetTag();
			
			if (!everyFrame)
				Finish();
		}
		
		public override void OnUpdate()
		{
			DoGetTag();
		}
		
		void DoGetTag()
		{
			_go = gameObject.OwnerOption == OwnerDefaultOption.UseOwner ? Owner : gameObject.GameObject.Value;

			if (_go == null) 
				return;
			
			storeResult.Value = _go.tag;
		}
		
	}
}