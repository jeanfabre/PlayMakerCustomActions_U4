// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
// Crafted by Elusiven https://github.com/elusiven
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using System;
using System.Globalization;
using System.Linq;
using System.Text;
using HutongGames.PlayMaker;

namespace Game.Scripts.CustomPlayMakerActions
{
    [ActionCategory(ActionCategory.String)]
    public class RemoveAccentsOnString : FsmStateAction
    {
        [ActionSection("Input")]
        [Tooltip("String to rip")] 
        public FsmString AccentedStr;

        [ActionSection("Options")] 
        public bool RipAccents;
        public bool RipPunctuation;
        public bool IgnoreCase;

        [ActionSection("Result")] 
        [Tooltip("Finished string")]
        public FsmString Result;
        
        public override void OnEnter()
        {
            DoRipping();
            Finish();
        }

        public override void Reset()
        {
            AccentedStr = null;
            RipAccents = false;
            RipPunctuation = false;
            IgnoreCase = false;
            Result = null;
        }

        private void DoRipping()
        {
            try
            {
                string asciiStr = AccentedStr.Value;
            
                if (RipAccents)
                {
                    asciiStr = RemoveDiacritics(asciiStr);
                }
            
                if (RipPunctuation)
                {
                    asciiStr = new string(asciiStr.Where(c => !char.IsPunctuation(c)).ToArray());
                }

                if (IgnoreCase)
                {
                    asciiStr = asciiStr.ToLower();
                }
            
                Result.Value = asciiStr;
            }
            catch (Exception e)
            {
                LogError(e.Message);
            }
        }
        
        private string RemoveDiacritics(string text)
        {
            if (string.IsNullOrEmpty(text))
                return text;

            text = text.Normalize(NormalizationForm.FormD);
            var chars = text.Where(c => CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark).ToArray();
            return new string(chars).Normalize(NormalizationForm.FormC);
        }

        private string RemoveAccents(string text)
        {
            byte[] tempBytes;
            tempBytes = System.Text.Encoding.GetEncoding("ISO-8859-8").GetBytes(text);
            text = System.Text.Encoding.UTF8.GetString(tempBytes);
            return text;
        }
    }
}