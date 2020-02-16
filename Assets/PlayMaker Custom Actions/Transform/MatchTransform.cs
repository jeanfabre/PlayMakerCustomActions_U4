// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/
// Made By : Terry and sebasLive
// Topic : http://hutonggames.com/playmakerforum/index.php?topic=10930


using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Transform)]
	[Tooltip("Sets GameObject's transform to match TargetObject's.")]
	public class MatchTransform : FsmStateAction
	{
		[RequiredField]
		[Tooltip("The GameObject that will be set.")]
		public FsmOwnerDefault gameObject;
		
		[RequiredField]
		[Tooltip("The TargetObject to be matched.")]
		public FsmGameObject targetObject;
		
		public bool matchPosition;
		
		public bool matchRotation;
		
		public bool matchScale;
		
		[Tooltip("Use local or world space.")]
		public Space space;
		
		[Tooltip("Repeat every frame.")]
		public bool everyFrame;

		[Tooltip("Perform in LateUpdate.")]
		public bool lateUpdate;
		
		private GameObject go;
        private GameObject goTarget;
		private Vector3 position;
		private Vector3 rotation;
		private Vector3 scale;

		public override void Reset()
		{
			gameObject = null;
			targetObject = null;
            matchPosition = true;
            matchRotation = true;
            matchScale = true;
			space = Space.World;
			everyFrame = true;
			lateUpdate = true;
		}

		public override void OnEnter()
		{
			if (!everyFrame && !lateUpdate)
			{
				DoSetTransform();
				Finish();
			}			
		}

		public override void OnPreprocess()
		{
			#if PLAYMAKER_1_8_5_OR_NEWER
			if (lateUpdate)
			{
				Fsm.HandleLateUpdate = true;
			}
			#endif
		}

		public override void OnUpdate()
		{
			if (!lateUpdate)
			{
				DoSetTransform();
			}
		}
		
		public override void OnLateUpdate()
		{
			if (lateUpdate)
			{
				DoSetTransform();
			}

			if (!everyFrame)
			{
				Finish();
			}
		}
		
		void DoSetTransform()
		{
		var go = Fsm.GetOwnerDefaultTarget(gameObject);
		var goTarget = targetObject.Value;
		
		if (matchPosition)
		{
		var position = space == Space.World ? goTarget.transform.position : goTarget.transform.localPosition;	

			if (space == Space.World)
			{
				go.transform.position = position;
			}
			else
			{
				go.transform.localPosition = position;
			}	
        }
		
		if (matchRotation)
		{
		var rotation = space == Space.World ? goTarget.transform.rotation : goTarget.transform.localRotation;	

			if (space == Space.World)
			{
				go.transform.rotation = rotation;
			}
			else
			{
				go.transform.localRotation = rotation;
			}	
        }
		
		if (matchScale)
		{
		var scale = goTarget.transform.localScale;	
		go.transform.localScale = scale;

        }
		
        }


	}
}
