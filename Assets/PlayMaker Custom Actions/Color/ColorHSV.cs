//http://www.starscenesoftware.com/stuff/unity/ColorHSV.cs
using UnityEngine;

public struct ColorHSV {
	float _h;	// 0..360
	public float h {
		get {return _h;}
		set {_h = value % 360;}
	}
	float _s;	// 0..100
	public float s {
		get {return _s;}
		set {
			if (value < 0) _s = 0;
			else if (value > 100) _s = 100;
			else _s = value;
		}
	}
	float _v;	// 0..100
	public float v {
		get {return _v;}
		set {
			if (value < 0) _v = 0;
			else if (value > 100) _v = 100;
			else _v = value;
		}
	}
	float _a;	// 0..100
	public float a {
		get {return _a;}
		set {
			if (value < 0) _a = 0;
			else if (value > 100) _a = 100;
			else _a = value;
		}
	}

	public ColorHSV (float h, float s, float v) : this() {
		this.h = h;
		this.s = s;
		this.v = v;
		this.a = 100.0f;
	}
	
	public ColorHSV (float h, float s, float v, float a) : this() {
		this.h = h;
		this.s = s;
		this.v = v;
		this.a = a;
	}
	
	public ColorHSV (Color color) : this() {
		Color32 col32 = color;
		SetColorHSV (col32);
	}

	public ColorHSV (Color32 color) : this() {
		SetColorHSV (color);
	}
	
	private void SetColorHSV (Color32 color) {
		float div = 100.0f / 255.0f;
		_a = color.a * div;
		_h = 0.0f;
		float minRGB = Mathf.Min(Mathf.Min(color.r, color.g), color.b);
		float maxRGB = Mathf.Max(Mathf.Max(color.r, color.g), color.b);
		float delta = maxRGB - minRGB;
		_v = maxRGB;
		if (maxRGB != 0.0f) {
			_s = 255.0f * delta / maxRGB;
		}
		else {
			_s = 0.0f;
		}
		if (_s != 0.0f) {
			if (color.r == maxRGB) {
				_h = (color.g - color.b) / delta;
			}
			else if (color.g == maxRGB) {
				_h = 2.0f + (color.b - color.r) / delta;
			}
			else if (color.b == maxRGB) {
				_h = 4.0f + (color.r - color.g) / delta;
			}
		}
		else {
			_h = -1.0f;
		}
		_h *= 60.0f;
		if (_h < 0.0f) {
			_h += 360.0f;
		}
		_s *= div;
		_v *= div;
	}
	
	public Color ToColor () {
		Color color = ToColor32();
		return color;
	}
	
	public Color32 ToColor32 () {
		float saturation = _s * .01f;
		float value = _v * 2.55f;
		int hi = (int)(Mathf.Floor(_h / 60.0f)) % 6;
		float f = _h / 60.0f - Mathf.Floor(_h / 60.0f);
		byte v1 = (byte)Mathf.Round(value);
		byte p = (byte)Mathf.Round(value * (1.0f - saturation));
		byte q = (byte)Mathf.Round(value * (1.0f - f * saturation));
		byte t = (byte)Mathf.Round(value * (1.0f - (1.0f - f) * saturation));
		byte a1 = (byte)Mathf.Round(_a * 2.55f);
		
		if (hi == 0) {
			return new Color32(v1, t, p, a1);
		}
		else if (hi == 1) {
			return new Color32(q, v1, p, a1);
		}
		else if (hi == 2) {
			return new Color32(p, v1, t, a1);
		}
		else if (hi == 3) {
			return new Color32(p, q, v1, a1);
		}
		else if (hi == 4) {
			return new Color32(t, p, v1, a1);
		}
		else {
			return new Color32(v1, p, q, a1);
		}
	}
	
	public override string ToString () {
		return "HSVA(" + h.ToString("f2") + ", " + s.ToString("f2") + ", " + v.ToString("f2") + ", " + a.ToString("f2") + ")";
	}
}