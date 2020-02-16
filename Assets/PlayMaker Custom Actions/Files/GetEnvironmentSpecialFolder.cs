// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;
using System.IO;
using System;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Files")]
	[Tooltip("Get Operating Special folder path into a string")]
	public class GetEnvironmentSpecialFolder : FsmStateAction
	{
		
		[Tooltip("The Special Folder type")]
		public Environment.SpecialFolder specialFolder;
		
		[RequiredField]
		[Tooltip("The special folder path as string")]
		[UIHint(UIHint.Variable)]
		public FsmString specialFolderPath;
		
		
		public override void Reset()
		{
			specialFolder = Environment.SpecialFolder.Personal;
			specialFolderPath = null;
		}
		
		
		public override void OnEnter()
		{
			#if ! UNITY_WEBPLAYER
				specialFolderPath.Value = Environment.GetFolderPath(specialFolder);
			#endif
			
			Finish ();
		}

	}
}

