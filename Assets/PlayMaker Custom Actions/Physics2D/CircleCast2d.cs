// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
// based on http://hutonggames.com/playmakerforum/index.php?topic=14969.0
/*--- __ECO__ __PLAYMAKER__ __ACTION__
EcoMetaStart
{
"script dependancies":[
						"Assets/PlayMaker Custom Actions/Physics2D/Editor/CircleCast2dActionEditor.cs"
					],
"version":"1.0.0"
}
EcoMetaEnd
---*/

using System;
using UnityEngine;
using HutongGames;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Physics2D)]
	public class CircleCast2d : FsmStateAction
	{
		[ActionSection("Setup")]
		
		[Tooltip("Start ray at game object position. \nOr use From Position parameter.")]
		public FsmOwnerDefault fromGameObject;
		
		[Tooltip("Start ray at a vector2 world position or as an offset of the FromGameObject if space is Self. \nOr use Game Object parameter.")]
		public FsmVector2 fromPosition;
		
		[Tooltip("A vector2 direction vector, if space is self, direction is based on FromGameObject reference")]
		public FsmVector2 direction;

		[Tooltip("Cast the ray in world or local space, position and direction are affected. Note if no Game Object is specified, the position and  direction is in world space.")]
		public Space space;

		[Tooltip("Radius of the circle being cast.")]
		public FsmFloat radius;

		[Tooltip("The length of the ray. Set to -1 for infinity.")]
		public FsmFloat distance;

		[Tooltip("Only include objects with a Z coordinate (depth) greater than this value. leave to none for no effect")]
		public FsmInt minDepth;

		[Tooltip("Only include objects with a Z coordinate (depth) less than this value. leave to none")]
		public FsmInt maxDepth;

		[ActionSection("Result")] 
		
		[Tooltip("Event to send if the ray hits an object.")]
		[UIHint(UIHint.Variable)]
		public FsmEvent hitEvent;
		
		[Tooltip("Set a bool variable to true if hit something, otherwise false.")]
		[UIHint(UIHint.Variable)]
		public FsmBool storeDidHit;
		
		[Tooltip("Store the game object hit in a variable.")]
		[UIHint(UIHint.Variable)]
		public FsmGameObject storeHitObject;
		
		[UIHint(UIHint.Variable)]
		[Tooltip("Get the 2d position of the ray hit point and store it in a variable.")]
		public FsmVector2 storeHitPoint;
		
		[UIHint(UIHint.Variable)]
		[Tooltip("Get the 2d normal at the hit point and store it in a variable.")]
		public FsmVector2 storeHitNormal;
		
		[UIHint(UIHint.Variable)]
		[Tooltip("Get the distance along the ray to the hit point and store it in a variable.")]
		public FsmFloat storeHitDistance;
		
		[UIHint(UIHint.Variable)]
		[Tooltip("Get the fraction along the ray to the hit point and store it in a variable. If the ray's direction vector is normalised then this value is simply the distance between the origin and the hit point. If the direction is not normalised then this distance is expressed as a 'fraction' (which could be greater than 1) of the vector's magnitude.")]
		public FsmFloat storeHitFraction;
		
		[ActionSection("Filter")] 
		
		[Tooltip("Set how often to cast a ray. 0 = once, don't repeat; 1 = everyFrame; 2 = every other frame... \nSince raycasts can get expensive use the highest repeat interval you can get away with.")]
		public FsmInt repeatInterval;
		
		[UIHint(UIHint.Layer)]
		[Tooltip("Pick only from these layers.")]
		public FsmInt[] layerMask;
		
		[Tooltip("Invert the mask, so you pick from all layers except those defined above.")]
		public FsmBool invertMask;
		
		[ActionSection("Debug")] 
		
		[Tooltip("The color to use for the debug line.")]
		public FsmColor debugColor;
		
		[Tooltip("Draw a debug line. Note: Check Gizmos in the Game View to see it in game.")]
		public FsmBool debug;

	   	private Transform _transform;
	    	private int repeat;


		private Vector2 _start;
		private Vector2 _direction;
		private float _length;
		private float _minDepth;
		private float _maxDepth; 

		public override void Reset()
		{
			fromGameObject = null;
			fromPosition = new FsmVector2 { UseVariable = true };
			direction = new FsmVector2 { UseVariable = true };

			space = Space.Self;

			minDepth = new FsmInt {UseVariable =true};
			maxDepth = new FsmInt {UseVariable =true};

			distance = 1;
			radius = 1;
			hitEvent = null;
			storeDidHit = null;
			storeHitObject = null;
			storeHitPoint = null;
			storeHitNormal = null;
			storeHitDistance = null;
			storeHitFraction = null;
			repeatInterval = 1;
			layerMask = new FsmInt[0];
			invertMask = false;
			debugColor = Color.yellow;
			debug = false;
		}
		
		public override void OnEnter()
		{


			DoRaycast();
			
			if (repeatInterval.Value == 0)
			{
				Finish();
			}		
		}
		
		public override void OnUpdate()
		{
			repeat--;
			
			if (repeat == 0)
			{
				DoRaycast();
			}
		}

		public void ComputeRayCastProperties(out Vector2 start, out Vector2 direction,out float length, out float minDepth,out float maxDepth)
		{
			minDepth = Mathf.NegativeInfinity;
			maxDepth = Mathf.Infinity;

			start = new Vector2();
			direction = new Vector2();

			var go = Fsm.GetOwnerDefaultTarget(fromGameObject);
			if (go!=null)
			{
				_transform = go.transform;
			}

			if (_transform!=null)
			{
				start.x += _transform.position.x;
				start.y += _transform.position.y;

			}

			if (!fromPosition.IsNone)
			{
				if (space == Space.Self)
				{
					start.x += fromPosition.Value.x;
					start.y += fromPosition.Value.y;

				}else{
					start.x = fromPosition.Value.x;
					start.y = fromPosition.Value.y;

				}


			}


			if (distance.Value >= 0 )
			{
				length = distance.Value;
			}else{
				length = Mathf.Infinity;
			}

			

			if(_transform != null )
			{
				if (!this.direction.IsNone)
				{
					if (space == Space.Self)
					{
						var dirVector = _transform.TransformDirection(new Vector3(this.direction.Value.x,this.direction.Value.y,0f));
						direction.x = dirVector.x;
						direction.y = dirVector.y;
					}else{
						direction.x = this.direction.Value.x;
						direction.y = this.direction.Value.y;
					}

				}else{
					direction = _transform.right;
				}


			}else{
				direction = this.direction.Value.normalized; // normalized to get the proper distance later using fraction from the rayCastHitinfo.
			}



			if (!this.minDepth.IsNone)
			{
				minDepth = this.minDepth.Value;
			}

			if (!this.maxDepth.IsNone)
			{
				maxDepth = this.maxDepth.Value;
			}

			

			if (_transform!=null && space == Space.Self)
			{
				if (!this.minDepth.IsNone)
				{
					minDepth += _transform.position.z;

				}
				if (!this.maxDepth.IsNone)
				{
					maxDepth += _transform.position.z;
					
				}
			}


		}

	    	private void DoRaycast()
		{
			repeat = repeatInterval.Value;
			
			if (Math.Abs(distance.Value) < Mathf.Epsilon)
			{
				return;
			}

			ComputeRayCastProperties(out _start,out _direction,out _length,out _minDepth,out _maxDepth);

			RaycastHit2D hitInfo;
		
			hitInfo = Physics2D.CircleCast(_start, radius.Value, _direction, _length, ActionHelpers.LayerArrayToLayerMask(layerMask, invertMask.Value), _minDepth, _maxDepth );


            		Fsm.RecordLastRaycastHit2DInfo(Fsm, hitInfo);
			
			var didHit = hitInfo.collider != null;
			
			storeDidHit.Value = didHit;
			
			if (didHit)
			{
				storeHitObject.Value = hitInfo.collider.gameObject;
				storeHitPoint.Value = hitInfo.point;
				storeHitNormal.Value = hitInfo.normal;
				storeHitDistance.Value = hitInfo.distance;
				storeHitFraction.Value = hitInfo.fraction;
				Fsm.Event(hitEvent);
			}
		}
		
	}
}

