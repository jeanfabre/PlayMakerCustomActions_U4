// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Physics)]
	[Tooltip("Raycast using info on the last collision event and store raycast infos in variables. See Unity Physics docs.")]
	public class GetCollisionRayCastInfo : FsmStateAction
	{
		[UIHint(UIHint.Variable)]
        [Tooltip("Get the GameObject hit.")]
		public FsmGameObject gameObjectHit;


		[ActionSection("Collision Infos")]
		[UIHint(UIHint.Variable)]
        [Tooltip("Get the relative velocity of the collision.")]
		public FsmVector3 relativeVelocity;

		[UIHint(UIHint.Variable)]
        [Tooltip("Get the relative speed of the collision. Useful for controlling reactions. E.g., selecting an appropriate sound fx.")]
		public FsmFloat relativeSpeed;

		[UIHint(UIHint.Variable)]
		[Tooltip("Get the world position of the collision contact. Useful for spawning effects etc.")]
        public FsmVector3 contactPoint;

		[UIHint(UIHint.Variable)]
        [Tooltip("Get the collision normal vector. Useful for aligning spawned effects etc.")]
		public FsmVector3 contactNormal;

		[UIHint(UIHint.Variable)]
		[Tooltip("Get the name of the physics material of the colliding GameObject. Useful for triggering different effects. Audio, particles...")]
		public FsmString physicsMaterialName;

		
		[ActionSection("Raycast Infos")]

		[UIHint(UIHint.Variable)]
		[Tooltip("Did the raycast hit something")]
		[Title("Hit Point")]
		public FsmBool hit;

		[UIHint(UIHint.Variable)]
		[Tooltip("Get the GameObject hit by the raycast.")]
		public FsmGameObject gameObjectRaycastHit;

		[UIHint(UIHint.Variable)]
		[Tooltip("Get the world position of the ray hit point and store it in a variable.")]
		[Title("Hit Point")]
		public FsmVector3 point;
		
		[UIHint(UIHint.Variable)]
		[Tooltip("Get the normal at the hit point and store it in a variable.")]
		public FsmVector3 normal;
		
		[UIHint(UIHint.Variable)]
		[Tooltip("Get the distance along the ray to the hit point and store it in a variable.")]
		public FsmFloat distance;

		[UIHint(UIHint.Variable)]
		[Tooltip("Get the barycentric coordinate of the triangle we hit.")]
		public FsmVector3 barycentricCoordinate;
		
		[UIHint(UIHint.Variable)]
		[Tooltip("Get the index of the triangle that was hit.")]
		public FsmInt triangleIndex;

		[UIHint(UIHint.Variable)]
		[Tooltip("Get the uv texture coordinate at the impact point.")]
		public FsmVector2 textureCoord;
		
		[UIHint(UIHint.Variable)]
		[Tooltip("Get the secondary uv texture coordinate at the impact point.")]
		public FsmVector2 textureCoord2;
		
		[UIHint(UIHint.Variable)]
		[Tooltip("Get the uv lightmap coordinate at the impact point.")]
		public FsmVector2 lightmapCoord;

		RaycastHit _hit;
		Ray _ray;

		public override void Reset()
		{
			gameObjectHit = null;
			relativeVelocity = null;
			relativeSpeed = null;
			contactPoint = null;
			contactNormal = null;
			physicsMaterialName = null;
		}

		void StoreCollisionInfo()
		{
			if (Fsm.CollisionInfo == null)
			{
			    return;
			}
			
			if (!gameObjectHit.IsNone) gameObjectHit.Value = Fsm.CollisionInfo.gameObject;
			if (!relativeSpeed.IsNone) relativeSpeed.Value = Fsm.CollisionInfo.relativeVelocity.magnitude;
			if (!relativeVelocity.IsNone) relativeVelocity.Value = Fsm.CollisionInfo.relativeVelocity;
			if (!physicsMaterialName.IsNone) physicsMaterialName.Value = Fsm.CollisionInfo.collider.material.name;

			if (Fsm.CollisionInfo.contacts != null && Fsm.CollisionInfo.contacts.Length > 0)
			{
				if (!contactPoint.IsNone) contactPoint.Value = Fsm.CollisionInfo.contacts[0].point;
				if (!contactNormal.IsNone) contactNormal.Value = Fsm.CollisionInfo.contacts[0].normal;
			}

			_hit = new RaycastHit();
			_ray = new Ray( Fsm.CollisionInfo.contacts[ 0 ].point -Fsm.CollisionInfo.contacts[ 0 ].normal, Fsm.CollisionInfo.contacts[ 0 ].normal );
			if( Physics.Raycast( _ray, out _hit ) ){

				if (!gameObjectRaycastHit.IsNone) gameObjectRaycastHit.Value = _hit.collider.gameObject;
				if (!point.IsNone) point.Value = _hit.point;
				if (!normal.IsNone) normal.Value = _hit.normal;
				if (!distance.IsNone) distance.Value = _hit.distance;
				if (!triangleIndex.IsNone) triangleIndex.Value	= _hit.triangleIndex;
				if (!textureCoord.IsNone) textureCoord.Value	= _hit.textureCoord;
				if (!textureCoord2.IsNone) textureCoord2.Value	= _hit.textureCoord2;
				if (!lightmapCoord.IsNone) lightmapCoord.Value	= _hit.lightmapCoord;
			}
		}

		public override void OnEnter()
		{
			StoreCollisionInfo();
			
			Finish();
		}
	}
}