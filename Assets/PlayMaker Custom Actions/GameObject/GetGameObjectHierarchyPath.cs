// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
//--- __ECO__ __PLAYMAKER__ __ACTION__ ---//
// based on http://hutonggames.com/playmakerforum/index.php?topic=14578.msg67762#msg67762

using UnityEngine;
using System.Collections.Generic;


namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.GameObject)]
	[Tooltip("Gets the hierarchy Path of the selected GameObject.")]
	public class GetGameObjectHierarchyPath : FsmStateAction
	{
		
		[RequiredField]
		[Tooltip("The GameObject from which you return a String of its Path.")]
		public FsmOwnerDefault GameObject;

		[Tooltip("Include The GameObject in the path")]
		public FsmBool IncludeSelf;

		[UIHint(UIHint.Variable)]
		[Tooltip("Store the final String of the GO's Path in a variable.")]
		public FsmString StoreFullPath;

		[UIHint(UIHint.Variable)]
		[Tooltip("Store all parents names")]
		[VariableType(VariableType.String)]
		public FsmArray StoreParentNames;

		[UIHint(UIHint.Variable)]
		[Tooltip("Store the all parents in an array.")]
		[VariableType(VariableType.GameObject)]
		public FsmArray StoreParents;

		
		private string result;
		private Transform tf;
		private string name;
		
		public override void Reset()
		{
			GameObject = null;
			IncludeSelf = false;
			StoreFullPath = null;
			StoreParentNames = null;
			StoreParents = null;
		}
		
		public override void OnEnter()
		{
			Find();
			Finish();
		}
		
		void Find()
		{
			var go1 = Fsm.GetOwnerDefaultTarget(GameObject);
			tf = go1.transform;
			result = string.Empty;

			List<GameObject> _goList = new List<GameObject>();
			List<string> _stringList = new List<string>();

			if (IncludeSelf.Value)
			{
				result = tf.name;
				_stringList.Add(tf.name);
				_goList.Add(tf.gameObject);

			}

			while (tf.parent != null)
			{
				tf = tf.parent;
				result = tf.name + "/" + result;

				_goList.Insert(0,tf.gameObject);
				_stringList.Insert(0,tf.name);
			}
			if (!StoreFullPath.IsNone)
			{
				StoreFullPath.Value = result;
			}
			if (!StoreParentNames.IsNone)
			{
				StoreParentNames.RawValue = _stringList.ToArray();
			}
			if (!StoreParents.IsNone)
			{
				StoreParents.RawValue = _goList.ToArray();
			}


		}
	}
}