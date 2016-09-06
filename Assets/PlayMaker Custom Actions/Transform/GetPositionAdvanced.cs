// (c) Copyright HutongGames, LLC 2010-2014. All rights reserved.
/*--- __ECO__ __PLAYMAKER__ __ACTION__ 
EcoMetaStart
{
"script dependancies":[
						"Assets/PlayMaker Custom Actions/__Internal/PlayMakerActionsUtils.cs"
					]
}
EcoMetaEnd
---*/

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Transform)]
	[Tooltip("Gets the Position of a Game Object and stores it in a Vector3 Variable or each Axis in a Float Variable.\n Advanced features allows selection of update type.")]
	public class GetPositionAdvanced : FsmStateAction
	{
		[RequiredField]
		public FsmOwnerDefault gameObject;
		
		[UIHint(UIHint.Variable)]
		public FsmVector3 vector;
		
		[UIHint(UIHint.Variable)]
		public FsmFloat x;
		
		[UIHint(UIHint.Variable)]
		public FsmFloat y;
		
		[UIHint(UIHint.Variable)]
		public FsmFloat z;
		
		public Space space;
		
		public PlayMakerActionsUtils.EveryFrameUpdateSelector updateType;
		
		public bool everyFrame;

		public override void Reset()
		{
			gameObject = null;
			vector = null;
			x = null;
			y = null;
			z = null;
			space = Space.World;
			updateType = PlayMakerActionsUtils.EveryFrameUpdateSelector.OnUpdate;
			everyFrame = false;
		}


		public override void Awake()
		{
			if (updateType == PlayMakerActionsUtils.EveryFrameUpdateSelector.OnFixedUpdate)
			{
				   Fsm.HandleFixedUpdate = true;
			}
		}
		
		public override void OnUpdate()
		{
			if (updateType == PlayMakerActionsUtils.EveryFrameUpdateSelector.OnUpdate)
			{
				DoGetPosition();
			}
			
			if (!everyFrame)
			{
				Finish();
			}
		}

		public override void OnLateUpdate()
		{
			if (updateType == PlayMakerActionsUtils.EveryFrameUpdateSelector.OnLateUpdate)
			{
				DoGetPosition();
			}

			if (!everyFrame)
			{
				Finish();
			}
		}
		
		public override void OnFixedUpdate()
		{
			if (updateType == PlayMakerActionsUtils.EveryFrameUpdateSelector.OnFixedUpdate)
			{
				DoGetPosition();
			}

			if (!everyFrame)
			{
				Finish();
			}
		}
		
		
		void DoGetPosition()
		{
			var go = Fsm.GetOwnerDefaultTarget(gameObject);
			if (go == null)
			{
				return;
			}

			var position = space == Space.World ? go.transform.position : go.transform.localPosition;				
			
			vector.Value = position;
			x.Value = position.x;
			y.Value = position.y;
			z.Value = position.z;
		}


	}
}
