// (c) Copyright HutongGames, LLC 2010-2016. All rights reserved.
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/
// original action by Kiriri: http://hutonggames.com/playmakerforum/index.php?topic=1681.msg7348#msg7348

using HutongGames.PlayMaker;
using UnityEngine;
using System;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.GameObject)]
	[Tooltip("Generates a unique identification string")]
	public class GenerateGuid : FsmStateAction
	{
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("A unique identification string")]
		public FsmString storeResult;

		public override void OnEnter()
		{
			storeResult.Value = System.Guid.NewGuid().ToString();
			Finish();
		}
	}
}