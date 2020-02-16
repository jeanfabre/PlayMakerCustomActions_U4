// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Array)]
	[Tooltip("Check if a GameObject ( by name and/or tag) is within an array.")]
	public class ArrayContainsGameObject : FsmStateAction
	{
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("The Array Variable to use.")]
		public FsmArray array;
		
		[Tooltip("The name of the GameObject to find in the arrayList. You can leave this empty if you specify a Tag.")]
		public FsmString gameObjectName;

		[UIHint(UIHint.Tag)]
        [Tooltip("Find a GameObject in this arrayList with this tag. If GameObject Name is specified then both name and Tag must match.")]
		public FsmString withTag;

		[ActionSection("Result")]
		
		[UIHint(UIHint.Variable)]
        [Tooltip("Store the result in a GameObject variable.")]
		public FsmGameObject result;
		
		[UIHint(UIHint.Variable)]
		public FsmInt resultIndex;
		
		[Tooltip("Store in a bool wether it contains or not that GameObject")]
		[UIHint(UIHint.Variable)]
		public FsmBool isContained;	
		
		[Tooltip("Event sent if this arraList contains that GameObject")]
		[UIHint(UIHint.FsmEvent)]
		public FsmEvent isContainedEvent;	

		[Tooltip("Event sent if this arraList does not contains that GameObject")]
		[UIHint(UIHint.FsmEvent)]
		public FsmEvent isNotContainedEvent;
		
		
		
		public override void Reset()
		{
		
			array = null;
			
			gameObjectName = null;

			withTag = new FsmString () {UseVariable=true};
			result = null;
			resultIndex = null;
			
			isContained = null;
			isContainedEvent = null;
			isNotContainedEvent = null;
			
		}
		
		
		public override void OnEnter()
		{

			int elementIndex = DoContainsGo();
			if (elementIndex>=0)
			{
				isContained.Value = true;
				result.Value = (GameObject)array.objectReferences[elementIndex];
				resultIndex.Value = elementIndex;
				Fsm.Event(isContainedEvent);
			}else{
				isContained.Value = false;	
				Fsm.Event(isNotContainedEvent);
			}

			Finish();
		}
		

		int DoContainsGo()
		{

			int _index =0;
			
			string _nameToken = gameObjectName.Value;
			string _tagToken = withTag.Value;
			
			foreach(GameObject _go in array.objectReferences)
			{
				
				if (_go!=null) 
				{
					if (_tagToken == "Untagged" || withTag.IsNone)
					{
						
						if (_go.name.Equals(_nameToken))
						{
							return _index;
						}
					}else{
					
						if (string.IsNullOrEmpty(_nameToken))
						{
							if (_go.tag.Equals(_tagToken))
							{
								return _index;
							}
						}else{
							
							if (_go.name.Equals(_nameToken) && _go.tag.Equals(_tagToken))
							{
								return _index;
							}
						}
					}
					
					
				}
				_index++;
			}
			
			
			
			return -1;
		}
		
	}
}