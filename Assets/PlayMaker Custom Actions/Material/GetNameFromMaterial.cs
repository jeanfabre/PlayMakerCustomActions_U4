// (c) Copyright HutongGames, LLC 2010-2016. All rights reserved.
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Material)]
	[Tooltip("Gets the name of game object's material.")]
	public class GetNameFromMaterial : FsmStateAction
	{
		
		[Tooltip("Alternatively specify a Material instead of a GameObject and Index.")]
		public FsmMaterial material;

		[Tooltip("If the pointer to the material is an instance, the name will be appended with ' (instance)'. If True, this will be removed, else the name will be as is")]
		public bool trimName;

		[Tooltip("The name of the material.")]
		[RequiredField]
		[UIHint(UIHint.Variable)]
		public FsmString materialName;



        public override void Reset()
		{
			material = null;
			trimName = true;
			materialName = null;
		}
		
		public override void OnEnter()
		{


            if (trimName && (material.Value.name.EndsWith(" (Instance)") || material.Value.name.EndsWith(" (Instance)")))
            {
                materialName.Value = material.Value.name.Substring(0, material.Value.name.Length - 11);
            }
            else
            {
                materialName.Value = material.Value.name;
            }
            Finish();
		}
	}
}
