// (c) Copyright HutongGames, LLC 2010-2018. All rights reserved.
// __ECO__ __PLAYMAKER__ __ACTION__ 

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Material)]
	[Tooltip("Sets the various properties of a Physics Material.")]
	public class SetPhysicMaterialProperties : FsmStateAction
	{
		[Tooltip("The GameObject that the material is applied to. Requires a Collider components")]
		[CheckForComponent(typeof(Collider))]
		public FsmOwnerDefault gameObject;

		[Tooltip("alternativly, can set directly, without a reference of the gameobject. Leave to null if targeting the gameobject")]
		public PhysicMaterial physicsMaterial;

		[Tooltip("Set the dynamicFriction value")]
		public FsmFloat dynamicFriction;

		[Tooltip("Set the staticFriction value")]
		public FsmFloat staticFriction;

		[Tooltip("Set the bounciness value")]
		public FsmFloat bounciness;

		[Tooltip("Set the frictionCombine value")]
		[ObjectType(typeof(PhysicMaterialCombine))]
		public FsmEnum frictionCombine;

		[Tooltip("Set the bounceCombine value")]
		[ObjectType(typeof(PhysicMaterialCombine))]
		public FsmEnum bounceCombine;

		[Tooltip("Set the frictionDirection2 value")]
		public FsmVector3 frictionDirection2;

		[Tooltip("Set the dynamicFriction2 value")]
		public FsmFloat dynamicFriction2;

		[Tooltip("Set the staticFriction2 value")]
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

			dynamicFriction = new FsmFloat (){UseVariable=true};
			staticFriction = new FsmFloat (){UseVariable=true};
			bounciness = new FsmFloat (){UseVariable=true};
			frictionCombine = new FsmEnum (){UseVariable=true};
			bounceCombine = new FsmEnum (){UseVariable=true};
			frictionDirection2 = new FsmVector3 (){UseVariable=true};
			dynamicFriction2 = new FsmFloat (){UseVariable=true};
			staticFriction2 = new FsmFloat (){UseVariable=true};

			everyFrame = false;
		}
		
		public override void OnEnter()
		{
			DoSetMaterialValue();
			
			if (!everyFrame)
			{
				Finish();
			}
		}
		
		public override void OnUpdate()
		{
			DoSetMaterialValue();
		}
		
		void DoSetMaterialValue()
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
				_mat.bounceCombine = (PhysicMaterialCombine)bounceCombine.Value;
			}

			if(!bounciness.IsNone)
			{
				_mat.bounciness = bounciness.Value;
			}

			if (!dynamicFriction.IsNone)
			{
				_mat.dynamicFriction = dynamicFriction.Value;
			}

			if (!dynamicFriction2.IsNone)
			{
				_mat.dynamicFriction2 = dynamicFriction2.Value;
			}

			if (!frictionCombine.IsNone)
			{
				_mat.frictionCombine = (PhysicMaterialCombine) frictionCombine.Value ;
			}

			if (!frictionDirection2.IsNone)
			{
				_mat.frictionDirection2 = frictionDirection2.Value;
			}

			if (!staticFriction.IsNone)
			{
				_mat.staticFriction = staticFriction.Value;
			}

			if (!staticFriction2.IsNone)
			{
				_mat.staticFriction2 = staticFriction2.Value;
			}
		}
	}
}