// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/
// https://twitter.com/Wahooney/status/785798323038937088

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Convert)]
	[Tooltip("Converts a volume value to Full Scale decibels (DBFS).")]
	[HelpUrl("http://www.playdotsound.com/portfolio-item/decibel-db-to-float-value-calculator-making-sense-of-linear-values-in-audio-tools/")]
	public class ConvertFloatToDecibels : FsmStateAction
	{
		
		[RequiredField]
		[HasFloatSlider(0,1)]
		[Tooltip("The Float volume to convert to full scale decibels (DBFS). range from 0 to 1")]
		public FsmFloat volume;
		
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("The Full Scalce decibels (DBFS) result. Range from -80 to 0")]
		public FsmFloat decibels;
		
		[Tooltip("Repeats every frame")]
		public bool everyFrame;
		
		public override void Reset()
		{
			volume = null;
			decibels = null;
			everyFrame = false;
		}
		
		public override void OnEnter()
		{
			DoConvert();
			
			if (!everyFrame)
			{
				Finish();
			}
		}
		
		public override void OnUpdate()
		{
			DoConvert();
		}
		
		void DoConvert()
		{
			
			if (volume.Value>0)
			{
				decibels.Value = 20f*Mathf.Log10(volume.Value);
			}else{
				decibels.Value  = -80f;
			}
			
		}
	}
}