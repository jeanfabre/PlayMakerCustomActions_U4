// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Transform)]
	[Tooltip("Gets Vector3 positions of multiple GameObjects.")]
	public class GetPositionMultiple : FsmStateAction
	{
		[CompoundArray("Variables", "GameObject", "Store Position")]
		[UIHint(UIHint.FsmGameObject)]
		public FsmGameObject[] gameObjects;
		[UIHint(UIHint.Variable)]
		public FsmVector3[] storePositions;

		public Space space;
		public bool everyFrame;

		public override void Reset()
		{
			gameObjects = new FsmGameObject[1];
			storePositions = new FsmVector3[1];
			space = Space.World;
			everyFrame = false;
		}

		public override void OnEnter()
		{
			DoGetPositions();
			
			if (!everyFrame)
			{
				Finish();
			}		
		}

		public override void OnUpdate()
		{
			DoGetPositions();
		}

		void DoGetPositions()
		{
			for (var i = 0; i < gameObjects.Length; i++)
			{
				var go = gameObjects[i].Value;
				if (go == null) return;

				var position = space == Space.World ? go.transform.position : go.transform.localPosition;
				storePositions[i].Value = position;
			}
		}
	}
}
