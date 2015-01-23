// (c) Copyright HutongGames, LLC 2010-2015. All rights reserved.
// Action made by DjayDino
/*--- __ECO__ __ACTION__ ---*/

using UnityEngine;
namespace HutongGames.PlayMaker.Actions
{	
	[ActionCategory("Easy Save 2")]
	public class ES2_AddInt : ES2LoadAction
	{
	[Tooltip("The data we we want to add.")]
	public FsmInt add;
	private FsmInt loadValue;

	public override void Reset()
		{
		loadValue = new FsmInt();
		add = null;
		base.Reset (); // Ensure that base.Reset() is called when done.
		}

	public override void OnEnter()
		{
		loadValue.Value = ES2.Load<int>(filename.Value, GetSettings(new ES2Settings()));
		
		loadValue.Value += add.Value;
		
		ES2.Save(loadValue.Value, filename.Value, GetSettings(new ES2Settings()));
		base.OnEnter(); // Ensure that base.OnEnter() is called when done.
		}
	}
}