// (c) Copyright HutongGames, LLC 2010-2018. All rights reserved.
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Material)]
	[Tooltip("Gets a named float value from a game object's material.")]
	public class GetMaterialFloat : FsmStateAction
	{
		[Tooltip("The GameObject that the material is applied to.")]
		[CheckForComponent(typeof(Renderer))]
		public FsmOwnerDefault gameObject;
		
		[Tooltip("GameObjects can have multiple materials. Specify an index to target a specific material.")]
		public FsmInt materialIndex;
		
		[Tooltip("Alternatively specify a Material instead of a GameObject and Index.")]
		public FsmMaterial material;
		
		[UIHint(UIHint.NamedColor)]
		[Tooltip("The named float parameter in the shader.")]
		public FsmString namedFloat;
		
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("Get the parameter value.")]
		public FsmFloat value;

		[Tooltip("Event sent if material was not found")]
		public FsmEvent fail;

		[Tooltip("executes every frame")]
		public bool everyframe;

		
		
		public override void Reset()
		{
			gameObject = null;
			materialIndex = 0;
			material = null;
			namedFloat = "_Shininess";
			value = null;
			fail = null;
			everyframe = false;
		}
		
		public override void OnEnter()
		{
			DoGetMaterialFloat();

			if (!everyframe) {
				Finish ();
			}
		}

		public override void OnUpdate()
		{
			DoGetMaterialFloat();
		}

		
		void DoGetMaterialFloat()
		{
			if (value.IsNone)
			{
				return;
			}
			
			var floatName = namedFloat.Value;
			if (floatName == "") floatName = "_Shininess";
			
			if (material.Value != null)
			{
				if ( ! material.Value.HasProperty(floatName)) 
				{
					Fsm.Event(fail);
					return;
				}
				
				value.Value = material.Value.GetFloat(floatName);
				return;
			}
			
			var go = Fsm.GetOwnerDefaultTarget(gameObject);
			if (go == null) return;
			
			if (go.renderer == null)
			{
				LogError("Missing Renderer!");
				return;
			}
			
			if (go.renderer.material == null)
			{
				LogError("Missing Material!");
				return;
			}		
			
			if (materialIndex.Value == 0)
			{
				if ( ! go.renderer.material.HasProperty(floatName)) 
				{
					Fsm.Event(fail);
					return;
				}
				value.Value = go.renderer.material.GetFloat(floatName);
			}
			else if (go.renderer.materials.Length > materialIndex.Value)
			{
				var materials = go.renderer.materials;
				
				if ( ! materials[materialIndex.Value].HasProperty(floatName)) 
				{
					Fsm.Event(fail);
					return;
				}
				
				value.Value = materials[materialIndex.Value].GetFloat(floatName);			
			}		
		}
	}
}
