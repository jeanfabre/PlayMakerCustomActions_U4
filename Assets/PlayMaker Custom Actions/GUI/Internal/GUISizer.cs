// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GUISizer {

    static public float  WIDTH = 1024;
    static public float HEIGHT = 768;

    static List<Matrix4x4> stack = new List<Matrix4x4> ();

    static public void BeginGUI() {
        stack.Add (GUI.matrix);
        Matrix4x4 m = new Matrix4x4 ();
        var w = (float)Screen.width;
        var h = (float)Screen.height;
        var aspect = w / h;
        var scale = 1f;
        var offset = Vector3.zero;
        if(aspect < (WIDTH/HEIGHT)) { //screen is taller
            scale = (Screen.width/WIDTH);
            offset.y += (Screen.height-(HEIGHT*scale))*0.5f;

        } else { // screen is wider
            scale = (Screen.height/HEIGHT);
            offset.x += (Screen.width-(WIDTH*scale))*0.5f;
        }
        m.SetTRS(offset,Quaternion.identity,Vector3.one*scale);
        GUI.matrix *= m;
    }

    static public void EndGUI() {
        GUI.matrix = stack[stack.Count - 1];
        stack.RemoveAt (stack.Count - 1);
    }
    
}