// (c) Copyright HutongGames, LLC 2010-2016. All rights reserved.
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Material)]
	[Tooltip("Gets the name of game object's material.")]
	public class GetMaterialName : FsmStateAction
	{
		[Tooltip("The GameObject that the material is applied to.")]
		[CheckForComponent(typeof(Renderer))]
		public FsmOwnerDefault gameObject;
		
		[Tooltip("GameObjects can have multiple materials. Specify an index to target a specific material.")]
		public FsmInt materialIndex;
		
		[Tooltip("Alternatively specify a Material instead of a GameObject and Index.")]
		public FsmMaterial material;

		[Tooltip("If the pointer to the material is an instance, the name will be appended with ' (instance)'. If True, this will be removed, else the name will be as is")]
		public bool trimName;

		[Tooltip("The name of the material.")]
		[RequiredField]
		[UIHint(UIHint.Variable)]
		public FsmString materialName;

		[Tooltip("Event sent if material was not found")]
		public FsmEvent fail;

		string _token = " (instance)";
		
		public override void Reset()
		{
			gameObject = null;
			materialIndex = 0;
			material = null;

			trimName = true;
			materialName = null;
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
			if (materialName.IsNone)
			{
				return false;
			}

			if (material.Value != null)
			{

				materialName.Value = material.Value.name;
				return true;
			}


			var go = Fsm.GetOwnerDefaultTarget(gameObject);
			if (go == null) return false;
			
			if (go.renderer == null)
			{
				LogError("Missing Renderer!");
				return false;
			}
			
			if (go.renderer.material == null)
			{
				LogError("Missing Material!");
				return false;
			}		
			
			if (materialIndex.Value == 0)
			{
				materialName.Value = go.renderer.material.name;
				return true;
				
			}
			else if (go.renderer.materials.Length > materialIndex.Value)
			{
				var materials = go.renderer.materials;
				materialName.Value = materials[materialIndex.Value].name;
				return true;
			}	

			return false;
		}

		string parseMaterialName(string name)
		{
			if ( trimName && name.EndsWith(_token))
			{
				return name.Replace(_token,"");
			}
			return name;
		}
	}
}
