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
	[Tooltip("Sets the Rotation of a Game Object. To leave any axis unchanged, set variable to 'None'.\n Advanced features allows selection of update type.")]
	public class SetRotationAdvanced : FsmStateAction
	{

		[RequiredField]
		[Tooltip("The GameObject to rotate.")]
		public FsmOwnerDefault gameObject;

		[UIHint(UIHint.Variable)]
		[Tooltip("Use a stored quaternion, or vector angles below.")]
		public FsmQuaternion quaternion;
		
		[UIHint(UIHint.Variable)]
		[Title("Euler Angles")]
		[Tooltip("Use euler angles stored in a Vector3 variable, and/or set each axis below.")]
		public FsmVector3 vector;
		
		public FsmFloat xAngle;
		public FsmFloat yAngle;
		public FsmFloat zAngle;

		[Tooltip("Use local or world space.")]
		public Space space;
		
		[Tooltip("Repeat every frame.")]
		public bool everyFrame;
		
		public PlayMakerActionsUtils.EveryFrameUpdateSelector updateType;
		
		public override void Reset()
		{
			base.Reset();
						gameObject = null;
			quaternion = null;
			vector = null;
			// default axis to variable dropdown with None selected.
			xAngle = new FsmFloat { UseVariable = true };
			yAngle = new FsmFloat { UseVariable = true };
			zAngle = new FsmFloat { UseVariable = true };
			space = Space.World;
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
				DoSetRotation();
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
				DoSetRotation();
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
				DoSetRotation();
			}

			if (!everyFrame)
			{
				Finish();
			}
		}
		

		void DoSetRotation()
		{
			var go = Fsm.GetOwnerDefaultTarget(gameObject);
			if (go == null)
			{
				return;
			}

			// Individual angle axis can override Quaternion and Vector angles
			// So we build up the final rotation in steps

			Vector3 rotation;

			if (!quaternion.IsNone)
			{
				rotation = quaternion.Value.eulerAngles;
			}
			else if (!vector.IsNone)
			{
				rotation = vector.Value;
			}
			else
			{
				// use current rotation of the game object

				rotation = space == Space.Self ? go.transform.localEulerAngles : go.transform.eulerAngles;
			}	
			
			// Override each axis
			
			if (!xAngle.IsNone)
			{
				rotation.x = xAngle.Value;
			}
			
			if (!yAngle.IsNone)
			{
				rotation.y = yAngle.Value;
			}
			
			if (!zAngle.IsNone)
			{
				rotation.z = zAngle.Value;
			}

			// apply rotation

			if (space == Space.Self)
			{
				go.transform.localEulerAngles = rotation;
			}
			else
			{
				go.transform.eulerAngles = rotation;
			}
		}


	}
}
