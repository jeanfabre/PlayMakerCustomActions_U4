// (c) Copyright HutongGames, LLC 2010-2018. All rights reserved.
// __ECO__ __PLAYMAKER__ __ACTION__ 

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Material)]
	[Tooltip("Gets the various properties of a Physics Material.")]
	public class GetPhysicMaterialProperties : FsmStateAction
	{
		[Tooltip("The GameObject that the material is applied to. Requires a Collider components")]
		[CheckForComponent(typeof(Collider))]
		public FsmOwnerDefault gameObject;

		[Tooltip("alternativly, can set directly, without a reference of the gameobject. Leave to null if targeting the gameobject")]
		public PhysicMaterial physicsMaterial;

		[Tooltip("Get the dynamicFriction value")]
		[UIHint(UIHint.Variable)]
		public FsmFloat dynamicFriction;

		[Tooltip("Get the staticFriction value")]
		[UIHint(UIHint.Variable)]
		public FsmFloat staticFriction;

		[Tooltip("Get the bounciness value")]
		[UIHint(UIHint.Variable)]
		public FsmFloat bounciness;

		[Tooltip("Get the frictionCombine value")]
		[UIHint(UIHint.Variable)]
		[ObjectType(typeof(PhysicMaterialCombine))]
		public FsmEnum frictionCombine;

		[Tooltip("Get the bounceCombine value")]
		[UIHint(UIHint.Variable)]
		[ObjectType(typeof(PhysicMaterialCombine))]
		public FsmEnum bounceCombine;

		[Tooltip("Get the frictionDirection2 value")]
		[UIHint(UIHint.Variable)]
		public FsmVector3 frictionDirection2;

		[Tooltip("Get the dynamicFriction2 value")]
		[UIHint(UIHint.Variable)]
		public FsmFloat dynamicFriction2;

		[Tooltip("Get the staticFriction2 value")]
		[UIHint(UIHint.Variable)]
		public FsmFloat staticFriction2;

		[Tooltip("Repeat every frame. Useful if the value is animated.")]
		public bool everyFrame;

		GameObject _go;
		Collider _col;
		PhysicMaterial _mat;

		public override void Reset()
		{
			gameObject = null;
			physicsMaterial = null;

			dynamicFriction = null;
			staticFriction = null;
			bounciness = null;
			frictionCombine = null;
			bounceCombine = null;
			frictionDirection2 = null;
			dynamicFriction2 = null;
			staticFriction2 = null;

			everyFrame = false;
		}
		
		public override void OnEnter()
		{
			DoGetMaterialProperties();
			
			if (!everyFrame)
			{
				Finish();
			}
		}
		
		public override void OnUpdate()
		{
			DoGetMaterialProperties();
		}
		
		void DoGetMaterialProperties()
		{
			if (physicsMaterial != null) {
				_mat = physicsMaterial;
			} else {
			
				_go = Fsm.GetOwnerDefaultTarget (gameObject);
				if (_go == null) {

					return;
				}

				_col = _go.GetComponent<Collider> ();
				if (_col == null) {
					return;
				}
				_mat = 	_col.material;
			}
				
			if (!bounceCombine.IsNone)
			{
				bounceCombine.Value = _mat.bounceCombine;
			}

			if(!bounciness.IsNone)
			{
				bounciness.Value = _mat.bounciness;
			}

			if (!dynamicFriction.IsNone)
			{
				dynamicFriction.Value = _mat.dynamicFriction;
			}

			if (!dynamicFriction2.IsNone)
			{
				dynamicFriction2.Value = _mat.dynamicFriction2;
			}

			if (!frictionCombine.IsNone)
			{
				frictionCombine.Value = _mat.frictionCombine;
			}

			if (!frictionDirection2.IsNone)
			{
				frictionDirection2.Value = _mat.frictionDirection2;
			}

			if (!staticFriction.IsNone)
			{
				staticFriction.Value =_mat.staticFriction;
			}

			if (!staticFriction2.IsNone)
			{
				staticFriction2.Value =_mat.staticFriction2;
			}
		}
	}
}