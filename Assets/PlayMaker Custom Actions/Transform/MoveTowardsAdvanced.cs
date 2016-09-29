// (c) Copyright HutongGames, LLC 2010-2014. All rights reserved.
/*--- __ECO__ __PLAYMAKER__ __ACTION__
EcoMetaStart
{
"script dependancies":[
						"Assets/PlayMaker Custom Actions/__Internal/PlayMakerActionsUtils.cs"
					]
"version":"1.1.0"
}
EcoMetaEnd
---*/

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Transform)]
	[Tooltip("Moves a Game Object towards a Target. Optionally sends an event when successful. The Target can be specified as a Game Object or a world Position. If you specify both, then the Position is used as a local offset from the Object's Position.\n Advanced features allows selection of update type.")]
	public class MoveTowardsAdvanced : FsmStateAction
	{
		[RequiredField]
		[Tooltip("The GameObject to Move")]
		public FsmOwnerDefault gameObject;
		
		[Tooltip("A target GameObject to move towards. Or use a world Target Position below.")]
		public FsmGameObject targetObject;
		
		[Tooltip("A world position if no Target Object. Otherwise used as a local offset from the Target Object.")]
		public FsmVector3 targetPosition;
		
		[Tooltip("Ignore any height difference in the target.")]
		public FsmBool ignoreVertical;
		
		[HasFloatSlider(0, 20)]
		[Tooltip("The maximum movement speed. HINT: You can make this a variable to change it over time.")]
		public FsmFloat maxSpeed;
		
		[HasFloatSlider(0, 5)]
		[Tooltip("Distance at which the move is considered finished, and the Finish Event is sent.")]
		public FsmFloat finishDistance;
		
		[Tooltip("Event to send when the Finish Distance is reached.")]
		public FsmEvent finishEvent;

		public PlayMakerActionsUtils.EveryFrameUpdateSelector updateType;

		
		private GameObject go;
		private GameObject goTarget;
		private Vector3 targetPos;
		private Vector3 targetPosWithVertical;
		
		private Rigidbody _rb;
		
		public override void Reset()
		{
			gameObject = null;
			targetObject = null;
			maxSpeed = 10f;
			finishDistance = 1f;
			finishEvent = null;
			updateType = PlayMakerActionsUtils.EveryFrameUpdateSelector.OnUpdate;
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
				DoMoveTowards();
			}
		}
		
		public override void OnLateUpdate()
		{
			if (updateType == PlayMakerActionsUtils.EveryFrameUpdateSelector.OnLateUpdate)
			{
				DoMoveTowards();
			}
		}
		
		public override void OnFixedUpdate()
		{
			if (updateType == PlayMakerActionsUtils.EveryFrameUpdateSelector.OnFixedUpdate)
			{
				DoMoveTowards();
			}
		}
		
		void DoMoveTowards()
		{
			if (!UpdateTargetPos())
			{
				return;
			}
			
			if (_rb!=null)
			{
				go.rigidbody.position = Vector3.MoveTowards(go.rigidbody.position, targetPos, maxSpeed.Value * Time.deltaTime);
			}else{
				go.transform.position = Vector3.MoveTowards(go.transform.position, targetPos, maxSpeed.Value * Time.deltaTime);
			}
			
			var distance = (go.transform.position - targetPos).magnitude;
			if (distance < finishDistance.Value)
			{
				Fsm.Event(finishEvent);
				Finish();
			}
		}
		
		public bool UpdateTargetPos()
		{
			go = Fsm.GetOwnerDefaultTarget(gameObject);
			if (go == null)
			{
				return false;
			}
			
			if (go.rigidbody!=null)
			{
				_rb = go.rigidbody;
			}
			
			goTarget = targetObject.Value;
			if (goTarget == null && targetPosition.IsNone)
			{
				return false;
			}
			
			if (goTarget != null)
			{
				targetPos = !targetPosition.IsNone ?
					goTarget.transform.TransformPoint(targetPosition.Value) :
						goTarget.transform.position;
			}
			else
			{
				targetPos = targetPosition.Value;
			}
			
			targetPosWithVertical = targetPos;
			
			if (ignoreVertical.Value)
			{
				targetPos.y = go.transform.position.y;
			}
			
			return true;
		}
		
		public Vector3 GetTargetPos()
		{
			return targetPos;
		}
		
		public Vector3 GetTargetPosWithVertical()
		{
			return targetPosWithVertical;
		}
	}
}
