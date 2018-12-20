// (c) Copyright HutongGames, LLC 2010-2018. All rights reserved.
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Material)]
	[Tooltip("Gets all material of a GameObject with a Renderer component")]
	public class GetMaterials : FsmStateAction
	{
		[Tooltip("The GameObject that the material is applied to.")]
		[CheckForComponent(typeof(Renderer))]
		public FsmOwnerDefault gameObject;

		[Tooltip("All materials on the GameObject's Renderer component")]
		[ArrayEditor(VariableType.Material)]
		[UIHint(UIHint.Variable)]
		public FsmArray materials;

		[Tooltip("Event sent if material was not found")]
		public FsmEvent fail;
		
		public override void Reset()
		{
			gameObject = null;
			materials = null;
			fail = null;
		}
		
		public override void OnEnter()
		{
			bool ok = DoGetMaterialName();

			if (!ok) Fsm.Event(fail);

			Finish();
		}
		
		bool DoGetMaterialName()
		{
			var go = Fsm.GetOwnerDefaultTarget(gameObject);
			if (go == null) return false;
			
			if (go.renderer == null)
			{
				LogError("Missing Renderer!");
				return false;
			}

			materials.Values = go.renderer.materials;

			return true;
		}

	}
}
