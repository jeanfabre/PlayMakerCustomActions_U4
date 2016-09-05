// (c) Copyright HutongGames, LLC 2010-2014. All rights reserved.
/*--- __ECO__ __PLAYMAKER__ __ACTION__
EcoMetaStart
{
"script dependancies":["Assets/PlayMaker Custom Actions/Physics/internal/PlayMakerWheelFrictionCurveClass.cs"],
"version":"1.0.1"
}
EcoMetaEnd

ChangeLogStart
###1.0.1
**Fix:** Added Stiffness access for forwardFriction and sidewaysFriction, thanks to [600](http://hutonggames.com/playmakerforum/index.php?topic=3981.msg42561#msg42561)

###1.0.0
**note:** Initial Public Release
ChangeLogEnd
---*/

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Physics)]
	[Tooltip("Sets the suspension, forward and sideways properties, cause they are not accessible via SetProperty by design")]
	public class SetWheelColliderProperties : FsmStateAction
	{
		
		[RequiredField]
		[Tooltip("The wheel target")]
		public FsmOwnerDefault gameObject;
		
		[ActionSection("Wheel")]
		
		public FsmFloat mass;
		
		public FsmFloat radius;
		
		[ActionSection("Suspension")]
		
		public FsmFloat distance;
		public FsmFloat spring;
		public FsmFloat damper;
		public FsmFloat targetPosition;
		
		[ActionSection("Forward Friction")]
		public PlayMakerWheelFrictionCurveClass forwardFriction;
		
		[ActionSection("Sideways Friction")]
		public PlayMakerWheelFrictionCurveClass sidewaysFriction;
		
		[ActionSection("Repeats every frame, usefull if value must change overtime")]
		public bool everyFrame;
		
		WheelCollider _wc;
		
		public override void Reset()
		{
			mass = new FsmFloat() {UseVariable=true};
			
			radius = new FsmFloat() {UseVariable=true};
			distance = new FsmFloat() {UseVariable=true};
			spring = new FsmFloat() {UseVariable=true};
			damper = new FsmFloat() {UseVariable=true};
			targetPosition = new FsmFloat() {UseVariable=true};
			
			forwardFriction = new PlayMakerWheelFrictionCurveClass();
			forwardFriction.Reset();
			sidewaysFriction = new PlayMakerWheelFrictionCurveClass();
			sidewaysFriction.Reset();
			
			everyFrame = false;
		}
		
		public override void OnEnter()
		{
			var go = Fsm.GetOwnerDefaultTarget(gameObject);
	
			_wc = go.GetComponent<WheelCollider>();

			_setProperties();
			
			if(!everyFrame)
			{
				Finish();
			}
		}
		public override void OnUpdate()
		{
			_setProperties();
		}
		
		public void _setProperties()
		{
			if (_wc==null)
			{
				return;
			}
			
			if (!mass.IsNone)
			{
				_wc.mass = mass.Value;
			}
			if (!radius.IsNone)
			{
				_wc.radius = radius.Value;
			}
			if (!distance.IsNone)
			{
				_wc.suspensionDistance = distance.Value;
			}
			
			if (!spring.IsNone || !damper.IsNone || !targetPosition.IsNone)
			{
				JointSpring _spring = _wc.suspensionSpring;
				if (!spring.IsNone)
				{
					_spring.spring = spring.Value;
				}
				if (!damper.IsNone)
				{
					_spring.damper = damper.Value;
				}
				if (!targetPosition.IsNone)
				{
					_spring.targetPosition = targetPosition.Value;
				}
				_wc.suspensionSpring = _spring;
			}
			
			
			
			
			// forwardFriction
			WheelFrictionCurve _forwardFrictionCurve = _wc.forwardFriction;
			if (!forwardFriction.extremumValue.IsNone)
			{
				_forwardFrictionCurve.extremumValue = forwardFriction.extremumValue.Value;
			}
			if (!forwardFriction.extremumSlip.IsNone)
			{
				_forwardFrictionCurve.extremumSlip = forwardFriction.extremumSlip.Value;
			}
			if (!forwardFriction.asymptoteValue.IsNone)
			{
				_forwardFrictionCurve.asymptoteValue = forwardFriction.asymptoteValue.Value;
			}
			if (!forwardFriction.asymptoteSlip.IsNone)
			{
				_forwardFrictionCurve.asymptoteSlip = forwardFriction.asymptoteSlip.Value;
			}
			if (!forwardFriction.stiffness.IsNone)
			{
				_forwardFrictionCurve.stiffness = forwardFriction.stiffness.Value;
			}
			_wc.forwardFriction =  _forwardFrictionCurve;
			
			
			
			
			// sidewaysFriction
			WheelFrictionCurve _sidewaysFrictionCurve = _wc.sidewaysFriction;
			if (!sidewaysFriction.extremumValue.IsNone)
			{
				_sidewaysFrictionCurve.extremumValue = sidewaysFriction.extremumValue.Value;
			}
			if (!sidewaysFriction.extremumSlip.IsNone)
			{
				_sidewaysFrictionCurve.extremumSlip = sidewaysFriction.extremumSlip.Value;
			}
			if (!sidewaysFriction.asymptoteValue.IsNone)
			{
				_sidewaysFrictionCurve.asymptoteValue = sidewaysFriction.asymptoteValue.Value;
			}
			if (!sidewaysFriction.asymptoteSlip.IsNone)
			{
				_sidewaysFrictionCurve.asymptoteSlip = sidewaysFriction.asymptoteSlip.Value;
			}
			if (!sidewaysFriction.stiffness.IsNone)
			{
				_sidewaysFrictionCurve.stiffness = sidewaysFriction.stiffness.Value;
			}
			_wc.sidewaysFriction =  _sidewaysFrictionCurve;
			
		}
		
	}
}
