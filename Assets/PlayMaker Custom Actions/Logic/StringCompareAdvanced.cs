// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
// Crafted by Elusiven https://github.com/elusiven
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using System;
using System.Globalization;
using HutongGames.PlayMaker;
using UnityEngine;

namespace Game.Scripts.CustomPlayMakerActions
{
    [ActionCategory(ActionCategory.Logic)]
    [HutongGames.PlayMaker.Tooltip("Compares 2 Strings and sends Events based on the result. This version has a few extra options.")]
    public class StringCompareAdvanced : FsmStateAction
    {
        [RequiredField]
        [UIHint(UIHint.Variable)]
        public FsmString stringVariable;
        public FsmString compareTo;
        public FsmEvent equalEvent;
        public FsmEvent notEqualEvent;
        [UIHint(UIHint.Variable)]
        [HutongGames.PlayMaker.Tooltip("Store the true/false result in a bool variable.")]
        public FsmBool storeResult;
        [HutongGames.PlayMaker.Tooltip("Repeat every frame. Useful if any of the strings are changing over time.")]
        public bool everyFrame;

        [ActionSection("OPTIONAL")] 
        [HutongGames.PlayMaker.Tooltip("Ignore accents in current culture, e.g. ø, å etc. ")]
        public bool ignoreAccents;
        [HutongGames.PlayMaker.Tooltip("Ignore case sensitivity")]
        public bool ignoreCase;

        public override void Reset()
        {
            stringVariable = null;
            compareTo = "";
            equalEvent = null;
            notEqualEvent = null;
            storeResult = null;
            everyFrame = false;
            ignoreAccents = false;
            ignoreCase = false;
        }

        public override void OnEnter()
        {
            if (ignoreAccents & ignoreCase)
            {
                DoStringCompareWithIgnoreAccentsAndCase();
            } else if (ignoreAccents & !ignoreCase)
            {
                DoStringCompareWithIgnoreAccents();
            } else if (ignoreCase & !ignoreAccents)
            {
                DoStringCompareWithIgnoreCase();
            }
            else
            {
                DoStringCompare();
            }
            
            
            if (!everyFrame)
            {
                Finish();
            }
        }

        public override void OnUpdate()
        {
            if (ignoreAccents & ignoreCase)
            {
                DoStringCompareWithIgnoreAccentsAndCase();
            } else if (ignoreAccents & !ignoreCase)
            {
                DoStringCompareWithIgnoreAccents();
            } else if (ignoreCase & !ignoreAccents)
            {
                DoStringCompareWithIgnoreCase();
            }
            else
            {
                DoStringCompare();
            }
        }
		
        void DoStringCompare()
        {
            if (stringVariable == null || compareTo == null) return;
			
            var equal = stringVariable.Value == compareTo.Value;

            if (storeResult != null)
            {
                storeResult.Value = equal;
            }

            if (equal && equalEvent != null)
            {
                Fsm.Event(equalEvent);
                return;
            }

            if (!equal && notEqualEvent != null)
            {
                Fsm.Event(notEqualEvent);
            }

        }

        void DoStringCompareWithIgnoreAccentsAndCase()
        {
            if (stringVariable == null || compareTo == null) return;
            if (String.Compare(stringVariable.Value, compareTo.Value, CultureInfo.CurrentCulture, CompareOptions.IgnoreNonSpace & CompareOptions.IgnoreCase & CompareOptions.IgnoreSymbols) == 0)
            {              
                // both strings are equal
                if (storeResult != null)
                {
                    storeResult.Value = true;
                }
                Debug.LogWarning("Doing string comparisment with accents and cases");
                Fsm.Event(equalEvent);
            }
            else
            {
                // both strings are equal
                if (storeResult != null)
                {
                    storeResult.Value = false;
                }
                
                Fsm.Event(notEqualEvent);
            }
        }
        
        void DoStringCompareWithIgnoreAccents()
        {
            if (stringVariable == null || compareTo == null) return;
            if (String.Compare(stringVariable.Value, compareTo.Value, CultureInfo.CurrentCulture, CompareOptions.IgnoreNonSpace) == 0)
            {              
                // both strings are equal
                if (storeResult != null)
                {
                    storeResult.Value = true;
                }
                
                Fsm.Event(equalEvent);
            }
            else
            {
                // both strings are NOT equal
                if (storeResult != null)
                {
                    storeResult.Value = false;
                }
                
                Fsm.Event(notEqualEvent);
            }
        }
        
        void DoStringCompareWithIgnoreCase()
        {
            if (stringVariable == null || compareTo == null) return;
            if (String.Compare(stringVariable.Value, compareTo.Value, CultureInfo.CurrentCulture, CompareOptions.IgnoreCase) == 0)
            {              
                // both strings are equal
                if (storeResult != null)
                {
                    storeResult.Value = true;
                }
                
                Fsm.Event(equalEvent);
            }
            else
            {
                // both strings are NOT equal
                if (storeResult != null)
                {
                    storeResult.Value = false;
                }
                
                Fsm.Event(notEqualEvent);
            }
        }
    }
}