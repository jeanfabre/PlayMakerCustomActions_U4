// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Physics)]
	[Tooltip("Gets the name of game object's Physics2D material.")]
	public class GetPhysics2dMaterialName : FsmStateAction
	{
		[Tooltip("The GameObject that the Physics material is applied to ( requires a Collider2D).")]
		[CheckForComponent(typeof(Collider2D))]
		public FsmOwnerDefault gameObject;
		
		[Tooltip("Alternatively specify a Physics Material 2D instead of a GameObject and Index.")]
		[ObjectType(typeof(PhysicsMaterial2D))]
		public FsmObject material;

		[Tooltip("The name of the material.")]
		[RequiredField]
		[UIHint(UIHint.Variable)]
		public FsmString materialName;

		[Tooltip("Event sent if material was not found")]
		public FsmEvent fail;

		string _token = " (instance)";

		Collider2D _c;

		public override void Reset()
		{
			gameObject = null;
			material = null;

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

			_c = go.GetComponent<Collider2D> ();

			if (_c == null)
			{
				LogError("Missing Collider!");
				return false;
			}

			if (_c.sharedMaterial != null) {
				materialName.Value = _c.sharedMaterial.name;
				return true;
			}

			return false;
		}

	}
}
