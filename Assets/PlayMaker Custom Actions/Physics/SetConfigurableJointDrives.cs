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
	[Tooltip("Sets the Drives on a Configurable Joint Component")]
	public class SetConfigurableJointDrives : FsmStateAction
	{

		[Tooltip("The Game Object with the ConfigurableJoint component")]
		[CheckForComponent(typeof(ConfigurableJoint))]
		public FsmOwnerDefault gameObject;

		[Tooltip("The ConfigurableJoint component. Use this when multiple joints are on the same GameObject")]
		[ObjectType(typeof(ConfigurableJoint))]
		public FsmObject OrComponent;

		[Tooltip("How the joint's movement will behave along its local axis.")]
		public Drives driveType;

		[Tooltip("Strength of a rubber-band pull toward the defined direction. Only used if mode includes Position.")]
		public FsmFloat positionSpring;

		[Tooltip("Resistance strength against the Position Spring. Only used if mode includes Position.")]
		public FsmFloat positionDamper	;

		[Tooltip("Amount of force applied to push the object toward the defined direction.")]
		public FsmFloat maximumForce;

		public bool everyFrame;

		ConfigurableJoint cj;

		public enum Drives{
			DriveX,
			DriveY,
			DriveZ,
			DriveAngularX,
			DriveAngularYZ,
			DriveSlerp
		}

		public override void Reset()
		{
			gameObject = null;
			OrComponent = new FsmObject(){UseVariable=true};

			driveType = Drives.DriveX;
			maximumForce = 3.402823e+38f;
			positionDamper	 = 0f;
			positionSpring = 0f;
			everyFrame = false;

		}

		public override void OnEnter()
		{
			DoAction();

			if (!everyFrame)
			{Finish();
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

			JointDrive jd = new JointDrive ();

			jd.maximumForce = maximumForce.Value;
			jd.positionDamper = positionDamper.Value;
			jd.positionSpring = positionSpring.Value;


			//
			switch (driveType)
			{
			case Drives.DriveX:
				{
					cj.xDrive = jd;
				}
				break;
			case Drives.DriveAngularX:
				{
					cj.angularXDrive = jd;
				}
				break;
			case Drives.DriveAngularYZ:
				{
					cj.angularYZDrive = jd;
				}
				break;
			case Drives.DriveZ:
				{
					cj.zDrive = jd;
				}
				break;
			case Drives.DriveY:
				{
					cj.yDrive = jd;
				}
				break;
			case Drives.DriveSlerp:
				{
					cj.slerpDrive = jd;
				}
				break;
			}
			//

		}
	}
}
