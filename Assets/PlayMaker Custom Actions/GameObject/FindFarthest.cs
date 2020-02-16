// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
// Based on FindClosest2

/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.GameObject)]
	[Tooltip("Finds the farthest object to the specified Game Object.\nOptionally filter by Tag, layer and Visibility.")]
	public class FindFarthest : FsmStateAction
	{
		[RequiredField]
		[Tooltip("The GameObject to measure from.")]
		public FsmOwnerDefault gameObject;
		
		[RequiredField]
		[UIHint(UIHint.Tag)]
		[Tooltip("Only consider objects with this Tag. NOTE: It's generally a lot quicker to find objects with a Tag!")]
		public FsmString withTag;
		
		[Tooltip("If checked, ignores the object that owns this FSM.")]
		public FsmBool ignoreOwner;

		[Tooltip("Only consider objects visible to the camera.")]
		public FsmBool mustBeVisible;
		
		[UIHint(UIHint.Layer)]
        [Tooltip("Only consider objects from these layers.")]
        public FsmInt[] layerMask;
		
		[Tooltip("Invert the mask, so you pick from all layers except those defined above.")]
        public FsmBool invertMask;
		
		[UIHint(UIHint.Variable)]
		[Tooltip("Store the closest object.")]
		public FsmGameObject storeObject;
		
		[UIHint(UIHint.Variable)]
		[Tooltip("Store the distance to the farthest object.")]
		public FsmFloat storeDistance;
		
		[Tooltip("Repeat every frame")]
		public bool everyFrame;

		
		int _layerMask;
		
		public override void Reset()
		{
			gameObject = null;	
			withTag = "Untagged";
			ignoreOwner = true;
			mustBeVisible = false;
			layerMask = new FsmInt[0];
			invertMask = false;
			storeObject = null;
			storeDistance = null;
			everyFrame = false;
			
		}

		public override void OnEnter()
		{
			_layerMask = ActionHelpers.LayerArrayToLayerMask(layerMask, invertMask.Value);
				
			DoFindFarthest();
			
			if (!everyFrame)
			{
				Finish();
			}
		}
		
		public override void OnUpdate()
		{
			DoFindFarthest();
		}

		void DoFindFarthest()
		{
			var go = gameObject.OwnerOption == OwnerDefaultOption.UseOwner ? Owner : gameObject.GameObject.Value;

			GameObject[] objects; // objects to consider

			if (string.IsNullOrEmpty(withTag.Value) || withTag.Value == "Untagged")
			{
				objects = (GameObject[])GameObject.FindObjectsOfType(typeof(GameObject));
			}
			else
			{
				objects = GameObject.FindGameObjectsWithTag(withTag.Value);
			}	
			
			GameObject farthestObj = null;
			float farthestDist = 0f;

			foreach (GameObject obj in objects)
			{
				if (ignoreOwner.Value && obj == Owner)
				{
					continue;
				}
				
				if (mustBeVisible.Value && !ActionHelpers.IsVisible(obj))
				{
					continue;
				}
				
				if (layerMask.Length>0)
				{
  					if ((_layerMask & (1 << obj.layer)) == 0)
					{
					
						continue;
					}
				}
				
				var dist = (go.transform.position - obj.transform.position).sqrMagnitude;
				if (dist > farthestDist)
				{
					farthestDist = dist;
					farthestObj = obj;
				}
			}

			storeObject.Value = farthestObj;
			
			if (!storeDistance.IsNone)
			{
				storeDistance.Value = Mathf.Sqrt(farthestDist);
			}
		}
	}
}
