// (c) Copyright HutongGames, LLC 2010-2016. All rights reserved.
//--- __ECO__ __PLAYMAKER__ __ACTION__ ---//

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{

	
	#pragma warning disable 168

	[ActionCategory(ActionCategory.GameObject)]
	[Tooltip("Finds a Game Object by Name and/or Tag. If searching by tags defined as FsmString, optional event to catch when tag doesn't exists")]
	public class FindGameObject2 : FsmStateAction
	{
		[Tooltip("The name of the GameObject to find. You can leave this empty if you specify a Tag.")]
		public FsmString objectName;
		
		[UIHint(UIHint.Tag)]
		[Tooltip("Find a GameObject with this tag. If Object Name is specified then both name and Tag must match.")]
		public FsmString withTag;
		
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("Store the result in a GameObject variable.")]
		public FsmGameObject store;
		
		[Tooltip("Event fired if tag is not found, typically possible if you are using a FsmString to define the tag")]
		public FsmEvent TagDoesntExistEvent;
		
		
		public override void Reset()
		{
			objectName = "";
			withTag = "Untagged";
			store = null;
			TagDoesntExistEvent = null;
		}
		
		public override void OnEnter()
		{
			Find();
			Finish();
		}
		
		void Find()
		{
			
			
			if (withTag.Value != "Untagged")
			{
				if (!string.IsNullOrEmpty(objectName.Value))
				{
					var possibleGameObjects = GameObject.FindGameObjectsWithTag(withTag.Value);
					
					foreach (var go in possibleGameObjects)
					{
						if (go.name == objectName.Value)
						{
							store.Value = go;
							return;
						}
					}
					
					store.Value = null;
					return;
				}
				try{
					
					store.Value = GameObject.FindGameObjectWithTag(withTag.Value);
				}catch(UnityException e)
				{
					Fsm.Event(TagDoesntExistEvent);
				}
				
				return;
			}
			
			store.Value = GameObject.Find(objectName.Value);
			
		}
		
		public override string ErrorCheck()
		{
			if (string.IsNullOrEmpty(objectName.Value) && string.IsNullOrEmpty(withTag.Value))
			{
				return "Specify Name, Tag, or both.";
			}
			
			return null;
		}
		
	}
}