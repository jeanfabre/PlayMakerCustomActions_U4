// (c) Copyright HutongGames, LLC 2010-2015. All rights reserved.
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.GameObject)]
	[Tooltip("Creates a Game Object, usually from a Prefab. Let you define parent and name")]
	public class CreateObjectAdvanced : FsmStateAction
	{
		[RequiredField]
		[Tooltip("GameObject to create. Usually a Prefab.")]
		public FsmGameObject gameObject;
		
		[Tooltip("GameObject  parent. Leave to null or none for no parenting")]
		public FsmOwnerDefault parent;
		

		[Tooltip("GameObject name. Leave to null or none for default")]
		public FsmString name;
		
		[Tooltip("Optional Spawn Point.")]
		public FsmGameObject spawnPoint;
		
		[Tooltip("Position. If a Spawn Point is defined, this is used as a local offset from the Spawn Point position.")]
		public FsmVector3 position;
		
		[Tooltip("Rotation. NOTE: Overrides the rotation of the Spawn Point.")]
		public FsmVector3 rotation;
		
		[UIHint(UIHint.Variable)]
		[Tooltip("Optionally store the created object.")]
		public FsmGameObject storeObject;

		[Tooltip("Use Network.Instantiate to create a Game Object on all clients in a networked game.")]
		public FsmBool networkInstantiate;

		[Tooltip("Usually 0. The group number allows you to group together network messages which allows you to filter them if so desired.")]
		public FsmInt networkGroup;

		public override void Reset()
		{
			gameObject = null;
			
			parent = new FsmOwnerDefault();
			parent.OwnerOption = OwnerDefaultOption.SpecifyGameObject;
			
			name = new FsmString() {UseVariable=true};
			
			spawnPoint = null;
			position = new FsmVector3 { UseVariable = true };
			rotation = new FsmVector3 { UseVariable = true };
			storeObject = null;
			networkInstantiate = false;
			networkGroup = 0;
		}

		public override void OnEnter()
		{
			var go = gameObject.Value;
			
			if (go != null)
			{
				var spawnPosition = Vector3.zero;
				var spawnRotation = Vector3.up;
				
				if (spawnPoint.Value != null)
				{
					spawnPosition = spawnPoint.Value.transform.position;
					
					if (!position.IsNone)
					{
						spawnPosition += position.Value;
					}
					
					spawnRotation = !rotation.IsNone ? rotation.Value : spawnPoint.Value.transform.eulerAngles;
				}
				else
				{
					if (!position.IsNone)
					{
						spawnPosition = position.Value;
					}
					
					if (!rotation.IsNone)
					{
						spawnRotation = rotation.Value;
					}
                }
				
				GameObject newObject;
				
#if !(UNITY_FLASH || UNITY_NACL || UNITY_METRO || UNITY_WP8)
				

				if (!networkInstantiate.Value)
				{
					newObject = (GameObject)Object.Instantiate(go, spawnPosition, Quaternion.Euler(spawnRotation));
				}
				else
				{
					newObject = (GameObject)Network.Instantiate(go, spawnPosition, Quaternion.Euler(spawnRotation), networkGroup.Value);
				}
#else
                newObject = (GameObject)Object.Instantiate(go, spawnPosition, Quaternion.Euler(spawnRotation));
#endif
                storeObject.Value = newObject;
				
				//newObject.transform.position = spawnPosition;
				//newObject.transform.eulerAngles = spawnRotation;
				
				var _parent = Fsm.GetOwnerDefaultTarget(parent);
				if (_parent!=null)
				{
					newObject.transform.parent = _parent.transform;
				}
				
				if (!string.IsNullOrEmpty(name.Value))
				{
					newObject.name = name.Value;
				}
			}
			
			
			
			
			Finish();
		}

	}
}