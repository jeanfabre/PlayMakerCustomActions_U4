// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Physics)]
	[Tooltip("Gets the name of game object's Physics material.")]
	public class GetPhysicsMaterialName : FsmStateAction
	{
		[Tooltip("The GameObject that the Physcismaterial is applied to ( requires a Collider).")]
		[CheckForComponent(typeof(Collider))]
		public FsmOwnerDefault gameObject;
		
		[Tooltip("Alternatively specify a Physics Material instead of a GameObject and Index.")]
		[ObjectType(typeof(PhysicMaterial))]
		public FsmObject material;

		[Tooltip("If the pointer to the material is an instance, the name will be appended with ' (instance)'. If True, this will be removed, else the name will be as is")]
		public bool trimName;

		[Tooltip("The name of the material.")]
		[RequiredField]
		[UIHint(UIHint.Variable)]
		public FsmString materialName;

		[Tooltip("Event sent if material was not found")]
		public FsmEvent fail;

		string _token = " (instance)";

		Collider _c;

		public override void Reset()
		{
			gameObject = null;
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

			_c = go.GetComponent<Collider> ();

			if (_c == null)
			{
				LogError("Missing Collider!");
				return false;
			}


			if (_c.material != null) {
				materialName.Value = parseMaterialName(_c.material.name);
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
