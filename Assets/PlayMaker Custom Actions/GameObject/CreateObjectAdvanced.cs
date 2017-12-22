// (c) Copyright HutongGames, LLC 2010-2017. All rights reserved.
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

		[Tooltip("If True, parenting will keep the objecr created in place")]
		public FsmBool worldPositionStays;

		[Tooltip("GameObject name. Leave to null or none for default")]
		public FsmString name;

		[Tooltip("Activate the created instance or not")]
		public FsmBool setActive;

		[Tooltip("Optional Spawn Point.")]
		public FsmGameObject spawnPoint;
		
		[Tooltip("Position. If a Spawn Point is defined, this is used as a local offset from the Spawn Point position.")]
		public FsmVector3 position;
		
		[Tooltip("Rotation. NOTE: Overrides the rotation of the Spawn Point if set.")]
		public FsmVector3 rotation;
		
		[UIHint(UIHint.Variable)]
		[Tooltip("Optionally store the created object.")]
		public FsmGameObject storeObject;


		GameObject newObject;

		public override void Reset()
		{
			gameObject = null;
			
			parent = new FsmOwnerDefault();
			parent.OwnerOption = OwnerDefaultOption.SpecifyGameObject;
			parent.GameObject = new FsmGameObject (){UseVariable=true};

			worldPositionStays = true;

			name = new FsmString() {UseVariable=true};

			setActive = true;

			spawnPoint = null;
			position = new FsmVector3 { UseVariable = true };
			rotation = new FsmVector3 { UseVariable = true };
			storeObject = null;

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
				
				newObject = (GameObject)Object.Instantiate(go, spawnPosition, Quaternion.Euler(spawnRotation));

                storeObject.Value = newObject;

				newObject.SetActive(setActive.Value);

				newObject.transform.position = spawnPosition;
				newObject.transform.eulerAngles = spawnRotation;
				
				var _parent = Fsm.GetOwnerDefaultTarget(parent);
				if (_parent!=null)
				{
					newObject.transform.SetParent(_parent.transform,worldPositionStays.Value);
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