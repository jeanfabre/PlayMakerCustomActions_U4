// https://forum.unity.com/threads/check-current-microphone-input-volume.133501/

// use action GetMicInputLoudness in PlayMaker to access the various data

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MicInput : MonoBehaviour
{
	#region SingleTon
	
	public static MicInput Instance { set; get; }
	
	#endregion
	
	public static float MicLoudness;
	public static float MicLoudnessInDecibels;
	
	private string _device;

	//mic initialization
	public void InitMic()
	{
		if (_device == null)
		{
			_device = Microphone.devices[0];
		}
		_clipRecord = Microphone.Start(_device, true, 999, 44100);
		_isInitialized = true;
	}
	
	public void StopMicrophone()
	{
		Microphone.End(_device);
		_isInitialized = false;
	}
	
	
	AudioClip _clipRecord = new AudioClip();
	AudioClip _recordedClip = new AudioClip();
	int _sampleWindow = 128;
	
	//get data from microphone into audioclip
	float MicrophoneLevelMax()
	{
		float levelMax = 0;
		float[] waveData = new float[_sampleWindow];
		int micPosition = Microphone.GetPosition(null) - (_sampleWindow + 1); // null means the first microphone
		if (micPosition < 0) return 0;
		_clipRecord.GetData(waveData, micPosition);
		// Getting a peak on the last 128 samples
		for (int i = 0; i < _sampleWindow; i++)
		{
			float wavePeak = waveData[i] * waveData[i];
			if (levelMax < wavePeak)
			{
				levelMax = wavePeak;
			}
		}
		return levelMax;
	}
	
	//get data from microphone into audioclip
	float MicrophoneLevelMaxDecibels()
	{
		
		float db = 20 * Mathf.Log10(Mathf.Abs(MicLoudness));
		
		return db;
	}
	
	public float FloatLinearOfClip(AudioClip clip)
	{
		StopMicrophone();
		
		_recordedClip = clip;
		
		float levelMax = 0;
		float[] waveData = new float[_recordedClip.samples];
		
		_recordedClip.GetData(waveData, 0);
		// Getting a peak on the last 128 samples
		for (int i = 0; i < _recordedClip.samples; i++)
		{
			float wavePeak = waveData[i] * waveData[i];
			if (levelMax < wavePeak)
			{
				levelMax = wavePeak;
			}
		}
		return levelMax;
	}
	
	public float DecibelsOfClip(AudioClip clip)
	{
		StopMicrophone();
		
		_recordedClip = clip;
		
		float levelMax = 0;
		float[] waveData = new float[_recordedClip.samples];
		
		_recordedClip.GetData(waveData, 0);
		// Getting a peak on the last 128 samples
		for (int i = 0; i < _recordedClip.samples; i++)
		{
			float wavePeak = waveData[i] * waveData[i];
			if (levelMax < wavePeak)
			{
				levelMax = wavePeak;
			}
		}
		
		float db = 20 * Mathf.Log10(Mathf.Abs(levelMax));
		
		return db;
	}
	
	
	
	void Update()
	{
		// levelMax equals to the highest normalized value power 2, a small number because < 1
		// pass the value to a static var so we can access it from anywhere
		MicLoudness = MicrophoneLevelMax();
		MicLoudnessInDecibels = MicrophoneLevelMaxDecibels();

	}
	
	bool _isInitialized;
	// start mic when scene starts
	void OnEnable()
	{
		InitMic();
		_isInitialized = true;
		Instance = this;
	}
	
	//stop mic when loading a new level or quit application
	void OnDisable()
	{
		StopMicrophone();
	}
	
	void OnDestroy()
	{
		StopMicrophone();
	}
	
	
	// make sure the mic gets started & stopped when application gets focused
	void OnApplicationFocus(bool focus)
	{
		if (focus)
		{
			//Debug.Log("Focus");
			
			if (!_isInitialized)
			{
				//Debug.Log("Init Mic");
				InitMic();
			}
		}
		if (!focus)
		{
			//Debug.Log("Pause");
			StopMicrophone();
			//Debug.Log("Stop Mic");
			
		}
	}
}