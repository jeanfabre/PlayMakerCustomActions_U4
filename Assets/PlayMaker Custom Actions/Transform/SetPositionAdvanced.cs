// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__
EcoMetaStart
{
"script dependancies":[
						"Assets/PlayMaker Custom Actions/__Internal/PlayMakerActionsUtils.cs"
					],
"version":"1.1.0"
}
EcoMetaEnd
---*/

using UnityEngine;


namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Transform)]
	[Tooltip("Sets the Position of a Game Object. To leave any axis unchanged, set variable to 'None'.\n Advanced features allows selection of update type.")]
	public class SetPositionAdvanced : FsmStateAction
	{

		[RequiredField]
		[Tooltip("The GameObject to position.")]
		public FsmOwnerDefault gameObject;
		
		[UIHint(UIHint.Variable)]
		[Tooltip("Use a stored Vector3 position, and/or set individual axis below.")]
		public FsmVector3 vector;
		
		public FsmFloat x;
		public FsmFloat y;
		public FsmFloat z;

		[Tooltip("Use local or world space.")]
		public Space space;
		
		[Tooltip("Repeat every frame.")]
		public bool everyFrame;
		
		public PlayMakerActionsUtils.EveryFrameUpdateSelector updateType;
		
		public override void Reset()
		{
			
			gameObject = null;
			vector = null;
			// default axis to variable dropdown with None selected.
			x = new FsmFloat { UseVariable = true };
			y = new FsmFloat { UseVariable = true };
			z = new FsmFloat { UseVariable = true };
			space = Space.Self;
			everyFrame = false;
			
			updateType = PlayMakerActionsUtils.EveryFrameUpdateSelector.OnUpdate;
		}
		
		public override void OnPreprocess()
		{
			if (updateType == PlayMakerActionsUtils.EveryFrameUpdateSelector.OnFixedUpdate)
			{
				Fsm.HandleFixedUpdate = true;
			}
			
			#if PLAYMAKER_1_8_5_OR_NEWER
			if (updateType == PlayMakerActionsUtils.EveryFrameUpdateSelector.OnLateUpdate)
			{
				Fsm.HandleLateUpdate = true;
			}
			#endif
		}
		
		public override void OnUpdate()
		{
			if (updateType == PlayMakerActionsUtils.EveryFrameUpdateSelector.OnUpdate)
			{
				DoSetPosition();
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
				DoSetPosition();
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
				DoSetPosition();
			}

			if (!everyFrame)
			{
				Finish();
			}
		}
		

		void DoSetPosition()
		{
			var go = Fsm.GetOwnerDefaultTarget(gameObject);
			if (go == null)
			{
				return;
			}
			
			// init position
			
			Vector3 position;

			if (vector.IsNone)
			{
				position = space == Space.World ? go.transform.position : go.transform.localPosition;
			}
			else
			{
				position = vector.Value;
			}
			
			// override any axis

			if (!x.IsNone) position.x = x.Value;
			if (!y.IsNone) position.y = y.Value;
			if (!z.IsNone) position.z = z.Value;

			// apply
			
			if (space == Space.World)
			{
				go.transform.position = position;
			}
			else
			{
				go.transform.localPosition = position;
			}
		}


	}
}
