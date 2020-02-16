// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory(ActionCategory.Debug)]
    [Tooltip("Logs the value of a Quaternion Variable in the PlayMaker Log Window.")]
    public class DebugQuaternion : BaseLogAction
    {
        [Tooltip("Info, Warning, or Error.")]
        public LogLevel logLevel;

        [UIHint(UIHint.Variable)]
        [Tooltip("The Vector3 variable to debug.")]
        public FsmQuaternion quaternionVariable;

        public override void Reset()
        {
            logLevel = LogLevel.Info;
            quaternionVariable = null;
            base.Reset();
        }

        public override void OnEnter()
        {
            var text = "None";

            if (!quaternionVariable.IsNone)
            {
                text = quaternionVariable.Name + ": " + quaternionVariable.Value;
            }

            ActionHelpers.DebugLog(Fsm, logLevel, text, sendToUnityLog);

            Finish();
        }
    }
}