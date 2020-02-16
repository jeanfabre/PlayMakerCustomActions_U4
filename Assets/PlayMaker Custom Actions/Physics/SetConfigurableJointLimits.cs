// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/
// covers Unity 4 and 5 with JointLimits differences.

#if (UNITY_4_3 || UNITY_4_5 || UNITY_4_6 || UNITY_4_7 || UNITY_5_0 )
#define UNITY_PRE_5_1
#endif


using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Physics)]
	[Tooltip("Sets the Joint Limits on a Configurable Joint Component")]
	public class SetConfigurableJointLimits : FsmStateAction
	{

		[Tooltip("The Game Object with the ConfigurableJoint component")]
		[CheckForComponent(typeof(ConfigurableJoint))]
		public FsmOwnerDefault gameObject;

		[Tooltip("The ConfigurableJoint component. Use this when multiple joints are on the same GameObject")]
		[ObjectType(typeof(ConfigurableJoint))]
		public FsmObject OrComponent;


		[Tooltip("Joint limits for each rotation axis and and linear degree of freedom.")]
		public Limits limitType;

		[ActionSection("Spring")]

		[Tooltip("The stiffness of the spring limit. When stiffness is zero the limit is hard, otherwise soft.")]
		public FsmFloat spring;

		[Tooltip("The damping of the spring limit. In effect when the stiffness of the sprint limit is not zero.")]
		public FsmFloat damper;

		[ActionSection("Limit")]

		[Tooltip("The limit position/angle of the joint (in degrees).")]
		public FsmFloat limit;

		[Tooltip("When the joint hits the limit, it can be made to bounce off it.")]
		public FsmFloat bounciness;

		[Tooltip("Determines how far ahead in space the solver can \"see\" the joint limit.")]
		public FsmFloat contactDistance;

		public bool everyFrame;

		ConfigurableJoint cj;

		public enum Limits{
			Linear,
			AngularXLow,
			AngularXHigh,
			AngularY,
			AngularZ
		}

		public override void Reset()
		{
			gameObject = null;
			OrComponent = new FsmObject(){UseVariable=true};

			limitType = Limits.Linear;
			spring = 0f;
			damper = 0f;
			limit = 0f;
			bounciness = 0f;
			contactDistance = 0f;
			everyFrame = false;

		}

		public override void OnEnter()
		{
			DoAction();

			if (!everyFrame)
			{
				Finish();
			}
		}
		
		public override void OnUpdate(){
			DoAction();
		}

		void DoAction(){ 
		
			if (cj== null)
			{
				if (!OrComponent.IsNone)
				{
					cj = OrComponent.Value as ConfigurableJoint;
				}else{
					GameObject go = Fsm.GetOwnerDefaultTarget (gameObject);
					
					if (go == null) {
						return;
					}
					
					cj = go.GetComponent<ConfigurableJoint> ();
				}
			}


			if (cj == null) {
				return;
			}

			SoftJointLimit sj = new SoftJointLimit ();
			#if !UNITY_PRE_5_1
				SoftJointLimitSpring sp = new SoftJointLimitSpring ();
			#endif
			sj.bounciness = bounciness.Value;
			#if !UNITY_PRE_5_1
				sj.contactDistance = contactDistance.Value;
			#endif
			sj.limit = limit.Value;

			#if !UNITY_PRE_5_1
				sp.damper = damper.Value;
				sp.spring = spring.Value;
			#else
				sj.damper = damper.Value;
				sj.spring = spring.Value;
			#endif
			//
			switch (limitType)
			{
			case Limits.Linear:
				{
					cj.linearLimit = sj;
				#if !UNITY_PRE_5_1
					cj.linearLimitSpring = sp;
				#endif
				}
				break;
			case Limits.AngularY:
				{
					cj.angularYLimit = sj;
				#if !UNITY_PRE_5_1
					cj.angularYZLimitSpring = sp;
				#endif
				}
				break;
			case Limits.AngularZ:
				{
					cj.angularZLimit = sj;
				#if !UNITY_PRE_5_1
					cj.angularYZLimitSpring = sp;
				#endif
				}
				break;
			case Limits.AngularXHigh:
				{
					cj.highAngularXLimit = sj;
				#if !UNITY_PRE_5_1
					cj.angularXLimitSpring = sp;
				#endif
				}
				break;
			case Limits.AngularXLow:
				{
					cj.lowAngularXLimit = sj;
				#if !UNITY_PRE_5_1
					cj.angularXLimitSpring = sp;
				#endif
				}
				break;
			}
			//

		}
	}
}
