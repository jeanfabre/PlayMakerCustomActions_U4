// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/
// https://twitter.com/Wahooney/status/785798323038937088

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Convert)]
	[Tooltip("Converts Full Scale decibels (DBFS) to a volume value.")]
	[HelpUrl("http://www.playdotsound.com/portfolio-item/decibel-db-to-float-value-calculator-making-sense-of-linear-values-in-audio-tools/")]
	public class ConvertDecibelsToFloat : FsmStateAction
	{

		[RequiredField]

		[HasFloatSlider(-80f,0f)]
		[Tooltip("The Full Scale decibels (DBFS), Range from -80 to 0")]
		public FsmFloat decibels;

		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("The result Float volume from decibels.  range from 0 to 1")]
		public FsmFloat volume;
		
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
			volume.Value = Mathf.Pow(10f,decibels.Value/20f);	
		}
	}
}