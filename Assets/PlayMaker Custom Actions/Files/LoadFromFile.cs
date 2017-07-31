// (c) Copyright HutongGames, LLC 2010-2015. All rights reserved.
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;
using System.IO;

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory("Files")]
    [Tooltip("Load a File into a string for example Assets/myfile.txt")]
    public class LoadFromFile : FsmStateAction
    {
        [RequiredField]
        [Tooltip("Set the file path, for example : Assets/myfile.txt")]
        public FsmString filePath;

        [RequiredField]
        [Tooltip("The text")]
        [UIHint(UIHint.Variable)]
        public FsmString text;

        public FsmEvent successEvent;
        public FsmEvent failureEvent;


        public override void Reset()
        {
            filePath = null;
            text = null;

        }


        public override void OnEnter()
        {
            if (System.IO.File.Exists(filePath.Value))
            {
                text.Value = System.IO.File.ReadAllText(filePath.Value);
                Fsm.Event(successEvent);
                Finish();
            }
            else
            {
                Debug.Log("File does not exist. Did you enter the correct filename and file extension?");
                Fsm.Event(failureEvent);
            }
        }

    }
}