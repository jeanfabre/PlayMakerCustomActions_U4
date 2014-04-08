//	(c) Jean Fabre, 2013 All rights reserved.
//	http://www.fabrejean.net
//  contact: http://www.fabrejean.net/contact.htm
//
// Version Alpha 0.1

// INSTRUCTIONS
// This set of utils is here to help custom action development, and scripts in general that wants to connect and work with PlayMaker API.

using UnityEngine;
using System.Text.RegularExpressions;

public partial class PlayMakerUtils {
	
	
	public static string ParseValueToString(object item,bool useBytes)
	{
		return "";
	}
	
	public static string ParseValueToString(object item)
	{
		if(item.GetType()==typeof(string))
		{
			return "string("+item.ToString()+")";
		}
		if(item.GetType()==typeof(bool))
		{
			int _val = (bool)item?1:0;
			return "bool("+_val+")";
		}
		if(item.GetType()==typeof(float))
		{
			float _val= float.Parse(item.ToString());
			return "float("+_val+")";
		}
		if(item.GetType()==typeof(int))
		{
			int _val= int.Parse(item.ToString());
			return "int("+_val+")";
		}
		if(item.GetType()==typeof(Vector2))
		{
			Vector2 _val= (Vector2)item;
			
			return "vector2("+_val.x+","+_val.y+")";
		}
		if(item.GetType()==typeof(Vector3))
		{
			Vector3 _val= (Vector3)item;
			
			return "vector3("+_val.x+","+_val.y+","+_val.z+")";
		}
		if(item.GetType()==typeof(Vector4))
		{
			Vector4 _val= (Vector4)item;
			
			return "vector4("+_val.x+","+_val.y+","+_val.z+","+_val.w+")";
		}
		if(item.GetType()==typeof(Quaternion))
		{
			Quaternion _val= (Quaternion)item;
			
			return "quaternion("+_val.x+","+_val.y+","+_val.z+","+_val.w+")";
		}
		if(item.GetType()==typeof(Rect))
		{
			Rect _val= (Rect)item;
			
			return "quaternion("+_val.x+","+_val.y+","+_val.width+","+_val.height+")";
		}
		if(item.GetType()==typeof(Color))
		{
			Color _val= (Color)item;
			
			return "color("+_val.r+","+_val.g+","+_val.b+","+_val.a+")";
		}
		if(item.GetType()==typeof(GameObject))
		{
			GameObject _val= (GameObject)item;
			
			//Debug.Log("GameObject "+_val.GetInstanceID().ToString());
			//return "gameObject("+_val.GetInstanceID().ToString()+")";
			return "gameObject("+_val.name+")";
		}
		
		Debug.LogWarning("ParseValueToString type not supported "+item.GetType());
		
		return "<"+item.GetType()+"> not supported";
	}
	
	
	public static object ParseValueFromString(string source,bool useBytes)
	{
		return null;
	}
	
	
	public static object ParseValueFromString(string source)
	{
		if (source==null)
		{
			return null;
		}
		
		if (source.StartsWith("string("))
		{
			source = source.Substring(7,source.Length-8);
			return source;
		}
		if (source.StartsWith("bool("))
		{
			source =  source.Substring(5,source.Length-6);
			bool _val= int.Parse(source)==1?true:false;
			return _val;
		}
		if (source.StartsWith("int("))
		{
			source =  source.Substring(4,source.Length-5);
			int _val= int.Parse(source);
			return _val;
		}
		if (source.StartsWith("float("))
		{
			source =  source.Substring(6,source.Length-7);
			float _val= float.Parse(source);
			return _val;
		}
		if (source.StartsWith("vector2("))
		{
			string fullregex = "vector2\\([x],[y]\\)";
			
			string floatregex = "[-+]?[0-9]*\\.?[0-9]+([eE][-+]?[0-9]+)?";
			
			// could be improved with some more regex to match x y z and inject in the replace... but let's not go there...
			fullregex = fullregex.Replace("[x]","(?<x>" + floatregex + ")");
			fullregex = fullregex.Replace("[y]","(?<y>" + floatregex + ")");
			
			fullregex = "^\\s*" + fullregex;
			
			Regex r = new Regex(fullregex);
   			Match m = r.Match(source);
			
			if ( m.Groups["x"].Value!="" && m.Groups["y"].Value!=""){
				return new Vector2(float.Parse(m.Groups["x"].Value),float.Parse(m.Groups["y"].Value));
			}
			
			return Vector2.zero;
		}
		if (source.StartsWith("vector3("))
		{
			string fullregex = "vector3\\([x],[y],[z]\\)";
			
			string floatregex = "[-+]?[0-9]*\\.?[0-9]+([eE][-+]?[0-9]+)?";
			
			// could be improved with some more regex to match x y z and inject in the replace... but let's not go there...
			fullregex = fullregex.Replace("[x]","(?<x>" + floatregex + ")");
			fullregex = fullregex.Replace("[y]","(?<y>" + floatregex + ")");
			fullregex = fullregex.Replace("[z]","(?<z>" + floatregex + ")");
				
			fullregex = "^\\s*" + fullregex;
			
			Regex r = new Regex(fullregex);
   			Match m = r.Match(source);
			
			if ( m.Groups["x"].Value!="" && m.Groups["y"].Value!="" && m.Groups["z"].Value!="")
			{
				return new Vector3(
									float.Parse(m.Groups["x"].Value),
									float.Parse(m.Groups["y"].Value),
									float.Parse(m.Groups["z"].Value) 
							);
			}
			
			return Vector3.zero;
		}
		if (source.StartsWith("vector4("))
		{
			string fullregex = "vector4\\([x],[y],[z],[w]\\)";
			
			string floatregex = "[-+]?[0-9]*\\.?[0-9]+([eE][-+]?[0-9]+)?";
			
			// could be improved with some more regex to match x y z and inject in the replace... but let's not go there...
			fullregex = fullregex.Replace("[x]","(?<x>" + floatregex + ")");
			fullregex = fullregex.Replace("[y]","(?<y>" + floatregex + ")");
			fullregex = fullregex.Replace("[z]","(?<z>" + floatregex + ")");
			fullregex = fullregex.Replace("[w]","(?<w>" + floatregex + ")");	
			fullregex = "^\\s*" + fullregex;
			
			Regex r = new Regex(fullregex);
   			Match m = r.Match(source);
			
			if ( m.Groups["x"].Value!="" && m.Groups["y"].Value!="" && m.Groups["z"].Value!="" && m.Groups["z"].Value!="")
			{
				return new Vector4(
									float.Parse(m.Groups["x"].Value),
									float.Parse(m.Groups["y"].Value),
									float.Parse(m.Groups["z"].Value),
									float.Parse(m.Groups["w"].Value) 
							);
			}
			
			return Vector4.zero;
		}
		if (source.StartsWith("rect("))
		{
			string fullregex = "rect\\([x],[y],[w],[h]\\)";
			
			string floatregex = "[-+]?[0-9]*\\.?[0-9]+([eE][-+]?[0-9]+)?";
			
			// could be improved with some more regex to match x y z and inject in the replace... but let's not go there...
			fullregex = fullregex.Replace("[x]","(?<x>" + floatregex + ")");
			fullregex = fullregex.Replace("[y]","(?<y>" + floatregex + ")");
			fullregex = fullregex.Replace("[w]","(?<w>" + floatregex + ")");
			fullregex = fullregex.Replace("[h]","(?<h>" + floatregex + ")");	
			fullregex = "^\\s*" + fullregex;
			
			Regex r = new Regex(fullregex);
   			Match m = r.Match(source);
			
			if ( m.Groups["x"].Value!="" && m.Groups["y"].Value!="" && m.Groups["w"].Value!="" && m.Groups["h"].Value!="")
			{
				return new Rect(
									float.Parse(m.Groups["x"].Value),
									float.Parse(m.Groups["y"].Value),
									float.Parse(m.Groups["w"].Value),
									float.Parse(m.Groups["h"].Value) 
							);
			}
			
			return new Rect(0,0,0,0);
		}
		if (source.StartsWith("quaternion("))
		{
			string fullregex = "quaternion\\([x],[y],[z],[w]\\)";
			
			string floatregex = "[-+]?[0-9]*\\.?[0-9]+([eE][-+]?[0-9]+)?";
			
			// could be improved with some more regex to match x y z and inject in the replace... but let's not go there...
			fullregex = fullregex.Replace("[x]","(?<x>" + floatregex + ")");
			fullregex = fullregex.Replace("[y]","(?<y>" + floatregex + ")");
			fullregex = fullregex.Replace("[z]","(?<z>" + floatregex + ")");
			fullregex = fullregex.Replace("[w]","(?<w>" + floatregex + ")");	
			fullregex = "^\\s*" + fullregex;
			
			Regex r = new Regex(fullregex);
   			Match m = r.Match(source);
			
			if ( m.Groups["x"].Value!="" && m.Groups["y"].Value!="" && m.Groups["z"].Value!="" && m.Groups["z"].Value!="")
			{
				return new Quaternion(
									float.Parse(m.Groups["x"].Value),
									float.Parse(m.Groups["y"].Value),
									float.Parse(m.Groups["z"].Value),
									float.Parse(m.Groups["w"].Value) 
							);
			}
			
			return Quaternion.identity;
		}
		
		if (source.StartsWith("color("))
		{
			string fullregex = "color\\([r],[g],[b],[a]\\)";
			
			string floatregex = "[-+]?[0-9]*\\.?[0-9]+([eE][-+]?[0-9]+)?";
			
			// could be improved with some more regex to match x y z and inject in the replace... but let's not go there...
			fullregex = fullregex.Replace("[r]","(?<r>" + floatregex + ")");
			fullregex = fullregex.Replace("[g]","(?<g>" + floatregex + ")");
			fullregex = fullregex.Replace("[b]","(?<b>" + floatregex + ")");
			fullregex = fullregex.Replace("[a]","(?<a>" + floatregex + ")");	
			
			fullregex = "^\\s*" + fullregex;
			
			Regex r = new Regex(fullregex);
   			Match m = r.Match(source);
			
			if ( m.Groups["r"].Value!="" && m.Groups["g"].Value!="" && m.Groups["b"].Value!="" && m.Groups["a"].Value!="")
			{
				return new Color(
									float.Parse(m.Groups["r"].Value),
									float.Parse(m.Groups["g"].Value),
									float.Parse(m.Groups["b"].Value),
									float.Parse(m.Groups["a"].Value) 
							);
			}
			
			return Color.black;
		}
		
		if (source.StartsWith("gameObject("))
		{
			source =  source.Substring(11,source.Length-12);
			GameObject go = GameObject.Find(source);
			return go;
		}
		
		Debug.LogWarning("ParseValueFromString failed for "+source);
		
		return null;
	}
	
}
