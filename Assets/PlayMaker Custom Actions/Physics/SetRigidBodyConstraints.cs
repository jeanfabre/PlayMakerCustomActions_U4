// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/
// http://hutonggames.com/playmakerforum/index.php?topic=1888.0

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Physics)]
	[Tooltip("Sets the constraints of a rigidBody")]
	public class SetRigidBodyConstraints : ComponentAction<Rigidbody> 
	{
		[RequiredField]
		[Tooltip("The Rigidbody GameObject")]
		[CheckForComponent(typeof(Rigidbody))]
		public FsmOwnerDefault gameObject;

		[Tooltip("freezeAll option. Leave to none for no effect")]
		public FsmBool freezeAll;

		[Tooltip("The all Positions constraint. Leave to none for no effect")]
		public FsmBool freezePosition;

		[Tooltip("The all Rotations constraint. Leave to none for no effect")]
		public FsmBool freezeRotation;

		[Tooltip("The X Position constraint. Leave to none for no effect")]
		public FsmBool freezePositionX;

		[Tooltip("The Y Position constraint. Leave to none for no effect")]
		public FsmBool freezePositionY;

		[Tooltip("The Z Position constraint. Leave to none for no effect")]
		public FsmBool freezePositionZ;

		[Tooltip("The X Rotation constraint. Leave to none for no effect")]
		public FsmBool freezeRotationX;

		[Tooltip("The Y Rotation constraint. Leave to none for no effect")]
		public FsmBool freezeRotationY;

		[Tooltip("The Z Rotation constraint. Leave to none for no effect")]
		public FsmBool freezeRotationZ;

		[Tooltip("repeats every frame")]
		public bool everyFrame;

		public override void Reset()
		{
			gameObject = null;

			freezeAll = new FsmBool() {UseVariable=true};

			freezeRotation = new FsmBool() {UseVariable=true};

			freezePosition = new FsmBool() {UseVariable=true};

			freezePositionX = new FsmBool() {UseVariable=true};
			freezePositionY = new FsmBool() {UseVariable=true};
			freezePositionZ = new FsmBool() {UseVariable=true};
			
			freezeRotationX = new FsmBool() {UseVariable=true};
			freezeRotationY = new FsmBool() {UseVariable=true};
			freezeRotationZ = new FsmBool() {UseVariable=true};

			everyFrame = false;
		}

		public override void OnEnter()
		{
			DoSetConstraints();
			
			if (!everyFrame)
				Finish();		
		}

		public override void OnUpdate()
		{
			DoSetConstraints();

		}

		void DoSetConstraints()
		{
			if (!UpdateCache(Fsm.GetOwnerDefaultTarget(gameObject)))
			{
				return;
			}
		
			if (!freezePositionX.IsNone)
			{
				if (freezePositionX.Value) {
					this.rigidbody.constraints = this.rigidbody.constraints | RigidbodyConstraints.FreezePositionX;
				}else{
					
					this.rigidbody.constraints = this.rigidbody.constraints & ~RigidbodyConstraints.FreezePositionX;	
				}
			}
			
			if (!freezePositionY.IsNone)
			{
				if (freezePositionY.Value) {
					this.rigidbody.constraints = this.rigidbody.constraints | RigidbodyConstraints.FreezePositionY;
				}else{
					
					this.rigidbody.constraints = this.rigidbody.constraints & ~RigidbodyConstraints.FreezePositionY;	
				}
			}
			
			if (!freezePositionZ.IsNone)
			{
				if (freezePositionZ.Value) {
					this.rigidbody.constraints = this.rigidbody.constraints | RigidbodyConstraints.FreezePositionZ;
				}else{
					
					this.rigidbody.constraints = this.rigidbody.constraints & ~RigidbodyConstraints.FreezePositionZ;	
				}
			}
			
			if (!freezeRotationX.IsNone)
			{
				if (freezeRotationX.Value) {
					this.rigidbody.constraints = this.rigidbody.constraints | RigidbodyConstraints.FreezeRotationX;
				}else{
					
					this.rigidbody.constraints = this.rigidbody.constraints & ~RigidbodyConstraints.FreezeRotationX;	
				}
			}
			
			if (!freezeRotationY.IsNone)
			{
				if (freezeRotationY.Value) {
					this.rigidbody.constraints = this.rigidbody.constraints | RigidbodyConstraints.FreezeRotationY;
				}else{
					
					this.rigidbody.constraints = this.rigidbody.constraints & ~RigidbodyConstraints.FreezeRotationY;	
				}
			}
			
			if (!freezeRotationZ.IsNone)
			{
				if (freezeRotationZ.Value) {
					this.rigidbody.constraints = this.rigidbody.constraints | RigidbodyConstraints.FreezeRotationZ;
				}else{
					
					this.rigidbody.constraints = this.rigidbody.constraints & ~RigidbodyConstraints.FreezeRotationZ;	
				}
			}

			if (!freezeAll.IsNone)
			{
				if (freezeAll.Value) {
					this.rigidbody.constraints = this.rigidbody.constraints | RigidbodyConstraints.FreezeAll;
				}else{
					
					this.rigidbody.constraints = this.rigidbody.constraints & ~RigidbodyConstraints.FreezeAll;	
				}
			}
			
			if (!freezePosition.IsNone)
			{
				if (freezePosition.Value) {
					this.rigidbody.constraints = this.rigidbody.constraints | RigidbodyConstraints.FreezePosition;
				}else{
					
					this.rigidbody.constraints = this.rigidbody.constraints & ~RigidbodyConstraints.FreezePosition;	
				}
			}

			if (!freezeRotation.IsNone)
			{
				if (freezeRotation.Value) {
					this.rigidbody.constraints = this.rigidbody.constraints | RigidbodyConstraints.FreezeRotation;
				}else{
					
					this.rigidbody.constraints = this.rigidbody.constraints & ~RigidbodyConstraints.FreezeRotation;	
				}
			}
			


		}
	}
}