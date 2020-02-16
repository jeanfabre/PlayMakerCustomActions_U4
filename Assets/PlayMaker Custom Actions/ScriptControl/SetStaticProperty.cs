// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using System;
using System.Reflection;
using UnityEngine;
using System.Collections.Generic;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.ScriptControl)]
	[Tooltip("Set a public static property in a class.")]
	public class SetStaticProperty : FsmStateAction
	{
		[Tooltip("Full path to the class that contains the static method.")]
		[RequiredField]
		public FsmString className;

		[Tooltip("The public static property to set.")]
		[RequiredField]
		public FsmString propertyName;

		[Tooltip("Property Value. NOTE: must match the property's type")]
		[RequiredField]
		public FsmVar propertyValue;

		[Tooltip("Repeat every frame.")]
		public bool everyFrame;

		private Type cachedType;
		private string cachedClassName;
		private string cachedPropertyName;
		private FieldInfo cachedFieldInfo;
		private object[] parametersArray;
		private string errorString;
        
		public override void Reset()
		{
			className = null;
			propertyName = null;
			propertyValue = null;
			everyFrame = false;
		}

		public override void OnEnter ()
		{
			DoSetStaticValue ();

			if (!everyFrame) {
				Finish ();
			}
		}

		public override void OnUpdate ()
		{
			DoSetStaticValue ();
		}

		private void DoSetStaticValue ()
		{
		
			if (className == null || string.IsNullOrEmpty (className.Value)) {
				Finish ();
				return;
			}

			if (cachedClassName != className.Value || cachedPropertyName != propertyName.Value) {
				errorString = string.Empty;
				if (!DoCache ()) {
					Debug.LogError (errorString);
					Finish ();
					return;
				}
			}

			propertyValue.UpdateValue();
			cachedFieldInfo.SetValue(null,propertyValue.GetValue () );
          
		}

		private bool DoCache ()
		{
			cachedType = ReflectionUtils.GetGlobalType (className.Value);
			if (cachedType == null) {
				errorString += "Class is invalid: " + className.Value + "\n";
				Finish ();
				return false;
			}
			cachedClassName = className.Value;


#if NETFX_CORE
		cachedPropertyInfo = cachedType.GetTypeInfo().GetDeclaredField(propertyName.Value);
#else


			cachedFieldInfo = cachedType.GetField (propertyName.Value, 
			                                       BindingFlags.Public | 
			                                       BindingFlags.Static | 
			                                       BindingFlags.FlattenHierarchy);
#endif            
			if (cachedFieldInfo == null) {
				errorString += "Invalid Property Name: " + propertyName.Value + "\n";
				Finish ();
				return false;
			}

			cachedPropertyName = propertyName.Value;

			return true;
		}


        public override string ErrorCheck()
        {
            errorString = string.Empty;
            DoCache();

            if (!string.IsNullOrEmpty(errorString))
            {
                return errorString;
            }


            if (ReferenceEquals(cachedFieldInfo.FieldType, typeof(void)))
            {
                
                return "Class not found";
                
            }
		else if (!ReferenceEquals(cachedFieldInfo.FieldType,propertyValue.RealType))
            {
		return "propertyValue is of the wrong type.\nIt should be of type: " + cachedFieldInfo.FieldType;
            }

            return string.Empty;
        }
        

	}
}
