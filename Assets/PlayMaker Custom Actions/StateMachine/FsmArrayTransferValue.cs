// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0)
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/
//Made by nightcorelv
// forumlink : http://hutonggames.com/playmakerforum/index.php?topic=18563.0

using UnityEngine;
using System.Collections.Generic;

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory(ActionCategory.StateMachine)]
    [Tooltip("Transfer a value from a FSM array to another, basically a copy/cut paste action on steroids.")]
    public class FsmArrayTransferValue : BaseFsmVariableAction
    {
        public enum ArrayTransferType { Copy, Cut, nullify };
        public enum ArrayPasteType { AsFirstItem, AsLastItem, InsertAtSameIndex, ReplaceAtSameIndex };

        [RequiredField]
        [Tooltip("The GameObject that owns the FSM.")]
        public FsmOwnerDefault gameObject;

        [UIHint(UIHint.FsmName)]
        [Tooltip("Optional name of FSM on Game Object")]
        public FsmString fsmName;

        [RequiredField]
        [UIHint(UIHint.FsmArray)]
        [Tooltip("The name of the FSM variable.")]
        public FsmString variableName;

        [RequiredField]
        [UIHint(UIHint.Variable)]
        [Tooltip("The Array Variable target.")]
        public FsmArray arrayTarget;

        [MatchFieldType("array")]
        [Tooltip("The index to transfer.")]
        public FsmInt indexToTransfer;

        public FsmBool Invert;

        [ActionSection("Transfer Options")]

        [ObjectType(typeof(ArrayTransferType))]
        public FsmEnum copyType;

        [ObjectType(typeof(ArrayPasteType))]
        public FsmEnum pasteType;

        [ActionSection("Result")]

        [Tooltip("Event sent if this if FSM array does not contains that element (described below)")]
        public FsmEvent indexOutOfRange;

        public override void Reset()
        {
            arrayTarget = null;
            Invert = false;
            indexToTransfer = null;

            copyType = ArrayTransferType.Copy;
            pasteType = ArrayPasteType.AsLastItem;
        }

        public override void OnEnter()
        {
            DoTransferValue();
            Finish();
        }

        private void DoTransferValue()
        {
            if (Invert.Value)
            {
                var go = Fsm.GetOwnerDefaultTarget(gameObject);
                if (!UpdateCache(go, fsmName.Value))
                {
                    return;
                }

                var fsmArray = fsm.FsmVariables.GetFsmArray(variableName.Value);

                if (arrayTarget.IsNone || fsmArray.IsNone)
                {
                    return;
                }
                int _index = indexToTransfer.Value;

                if (_index < 0 || _index >= arrayTarget.Length)
                {
                    Fsm.Event(indexOutOfRange);
                    return;
                }
                var _value = arrayTarget.Values[_index];
                
                if ((ArrayTransferType)copyType.Value == ArrayTransferType.Cut)
                {
                    List<object> _list = new List<object>(arrayTarget.Values);
                    _list.RemoveAt(_index);
                    arrayTarget.Values = _list.ToArray();
                }
                else if ((ArrayTransferType)copyType.Value == ArrayTransferType.nullify)
                {
                    arrayTarget.Values.SetValue(null, _index);
                }

                /////////////

                if ((ArrayPasteType)pasteType.Value == ArrayPasteType.AsFirstItem)
                {
                    List<object> _listTarget = new List<object>(fsmArray.Values);
                    _listTarget.Insert(0, _value);
                    fsmArray.Values = _listTarget.ToArray();

                }
                else if ((ArrayPasteType)pasteType.Value == ArrayPasteType.AsLastItem)
                {
                    fsmArray.Resize(fsmArray.Length + 1);
                    fsmArray.Set(fsmArray.Length - 1, _value);

                }
                else if ((ArrayPasteType)pasteType.Value == ArrayPasteType.InsertAtSameIndex)
                {
                    if (_index >= fsmArray.Length)
                    {
                        Fsm.Event(indexOutOfRange);
                    }
                    List<object> _listTarget = new List<object>(fsmArray.Values);
                    _listTarget.Insert(_index, _value);
                    fsmArray.Values = _listTarget.ToArray();

                }
                else if ((ArrayPasteType)pasteType.Value == ArrayPasteType.ReplaceAtSameIndex)
                {
                    if (_index >= fsmArray.Length)
                    {
                        Fsm.Event(indexOutOfRange);
                    }
                    else
                    {
                        fsmArray.Set(_index, _value);
                    }
                }
            }
            else
            {


                var go = Fsm.GetOwnerDefaultTarget(gameObject);
                if (!UpdateCache(go, fsmName.Value))
                {
                    return;
                }

                var fsmArray = fsm.FsmVariables.GetFsmArray(variableName.Value);

                if (fsmArray.IsNone || arrayTarget.IsNone)
                {
                    return;
                }
                int _index = indexToTransfer.Value;

                if (_index < 0 || _index >= fsmArray.Length)
                {
                    Fsm.Event(indexOutOfRange);
                    return;
                }
                var _value = fsmArray.Values[_index];

                if ((ArrayTransferType)copyType.Value == ArrayTransferType.Cut)
                {
                    List<object> _list = new List<object>(fsmArray.Values);
                    _list.RemoveAt(_index);
                    fsmArray.Values = _list.ToArray();
                }
                else if ((ArrayTransferType)copyType.Value == ArrayTransferType.nullify)
                {
                    fsmArray.Values.SetValue(null, _index);
                }

                if ((ArrayPasteType)pasteType.Value == ArrayPasteType.AsFirstItem)
                {
                    List<object> _listTarget = new List<object>(arrayTarget.Values);
                    _listTarget.Insert(0, _value);
                    arrayTarget.Values = _listTarget.ToArray();

                }
                else if ((ArrayPasteType)pasteType.Value == ArrayPasteType.AsLastItem)
                {
                    arrayTarget.Resize(arrayTarget.Length + 1);
                    arrayTarget.Set(arrayTarget.Length - 1, _value);

                }
                else if ((ArrayPasteType)pasteType.Value == ArrayPasteType.InsertAtSameIndex)
                {
                    if (_index >= arrayTarget.Length)
                    {
                        Fsm.Event(indexOutOfRange);
                    }
                    List<object> _listTarget = new List<object>(arrayTarget.Values);
                    _listTarget.Insert(_index, _value);
                    arrayTarget.Values = _listTarget.ToArray();

                }
                else if ((ArrayPasteType)pasteType.Value == ArrayPasteType.ReplaceAtSameIndex)
                {
                    if (_index >= arrayTarget.Length)
                    {
                        Fsm.Event(indexOutOfRange);
                    }
                    else
                    {
                        arrayTarget.Set(_index, _value);
                    }
                }
            }
        }
    }
}
