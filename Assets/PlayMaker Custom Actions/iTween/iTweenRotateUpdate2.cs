// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("iTween")]
	[Tooltip("Similar to RotateTo but incredibly less expensive for usage inside the Update function or similar looping situations involving a 'live' set of changing values. Does not utilize an EaseType. Let's you choose the tyoe of update.")]
	public class iTweenRotateUpdate2 : FsmStateAction
	{
		public enum PlayMakerUpdateCallType {Update,LateUpdate,FixedUpdate};
		
		[RequiredField]
		public FsmOwnerDefault gameObject;
		[Tooltip("Rotate to a transform rotation.")]
		public FsmGameObject transformRotation;
		[Tooltip("A rotation the GameObject will animate from.")]
		public FsmVector3 vectorRotation;
		[Tooltip("The time in seconds the animation will take to complete. If transformRotation is set, this is used as an offset.")]
		public FsmFloat time;

		[Tooltip("Whether to animate in local or world space.")]
		public Space space = Space.World;
		
		
		public PlayMakerUpdateCallType updateCall;
		
		System.Collections.Hashtable hash;			
		GameObject go;
		
		public override void Reset()
		{
			transformRotation = new FsmGameObject { UseVariable = true};
			vectorRotation = new FsmVector3 { UseVariable = true };
			time = 1f;
			space = Space.World;
			
			updateCall = PlayMakerUpdateCallType.Update;
		}

		public override void OnPreprocess()
		{
			if(updateCall == PlayMakerUpdateCallType.FixedUpdate)
			{
				Fsm.HandleFixedUpdate = true;
			}
			
			#if PLAYMAKER_1_8_5_OR_NEWER
			if(updateCall == PlayMakerUpdateCallType.LateUpdate)
			{
				Fsm.HandleLateUpdate = true;
			}
			#endif
		}


		public override void OnEnter()
		{
			hash = new System.Collections.Hashtable();
			go = Fsm.GetOwnerDefaultTarget(gameObject);
			if (go == null) {
				Finish();
				return;
			}
			if(transformRotation.IsNone){
				hash.Add("rotation", vectorRotation.IsNone ? Vector3.zero : vectorRotation.Value);
			} else {
				if(vectorRotation.IsNone){
					hash.Add("rotation", transformRotation.Value.transform);
				} else {
					if(space == Space.World) hash.Add("rotation", transformRotation.Value.transform.eulerAngles + vectorRotation.Value);
					else  hash.Add("rotation", transformRotation.Value.transform.localEulerAngles + vectorRotation.Value);
				}
				
			}
			hash.Add("time", time.IsNone ? 1f : time.Value);
			hash.Add("islocal", space == Space.Self);
			DoiTween();
		}
		
		public override void OnExit(){
			
		}
		
		
		public override void OnLateUpdate()
		{
			if (updateCall == PlayMakerUpdateCallType.LateUpdate)
			{
				PerformUpdate();
			}
		}
		
		public override void OnFixedUpdate()
		{
			if (updateCall == PlayMakerUpdateCallType.FixedUpdate)
			{
				PerformUpdate();
			}
		}
		
		public override void OnUpdate()
		{
			if (updateCall == PlayMakerUpdateCallType.Update)
			{
				PerformUpdate();
			}
		}
		
		private void PerformUpdate(){
			hash.Remove("rotation");
			if(transformRotation.IsNone){
				hash.Add("rotation", vectorRotation.IsNone ? Vector3.zero : vectorRotation.Value);
			} else {
				if(vectorRotation.IsNone){
					hash.Add("rotation", transformRotation.Value.transform);
				} else {
					if(space == Space.World) hash.Add("rotation", transformRotation.Value.transform.eulerAngles + vectorRotation.Value);
					else  hash.Add("rotation", transformRotation.Value.transform.localEulerAngles + vectorRotation.Value);
				}
				
			}
			DoiTween();	
		}
		
		void DoiTween()
		{
			iTween.RotateUpdate(go, hash);		
		}
	}
}
