// (c) Copyright HutongGames, LLC 2010-2016. All rights reserved.
// __ECO__ __PLAYMAKER__ __ACTION__ 

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Material)]
	[Tooltip("Sets the friction value of a PhysicsMaterial2d.")]
	public class SetPhysicsMaterial2dFriction : FsmStateAction
	{
		[Tooltip("The GameObject that the material is applied to. Requires a Collider2d components")]
		[CheckForComponent(typeof(Collider2D))]
		public FsmOwnerDefault gameObject;

		[Tooltip("alternativly, can set directly, without a reference of the gameobject. Leave to null if targeting the gameobject")]
		public PhysicsMaterial2D physicsMaterial2d;
		
		[RequiredField]
		[Tooltip("Set the friction value")]
		public FsmFloat friction;
		
		[Tooltip("Repeat every frame. Useful if the value is animated.")]
		public bool everyFrame;

		GameObject _go;
		Collider2D _col2d;

		public override void Reset()
		{
			gameObject = null;
			physicsMaterial2d = null;
			friction = null;
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
			if (physicsMaterial2d != null)
			{
				physicsMaterial2d.friction = friction.Value;
				return;
			}
			
			_go = Fsm.GetOwnerDefaultTarget(gameObject);
			if (_go==null)
			{

				return;
			}

			_col2d = _go.GetComponent<Collider2D>();
			if (_col2d==null)
			{
				return;
			}

			if (_col2d.sharedMaterial != null)
			{
				_col2d.sharedMaterial.friction = friction.Value;
			}		
					
		}
	}
}