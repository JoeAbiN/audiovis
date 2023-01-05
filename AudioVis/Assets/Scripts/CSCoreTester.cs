using UnityEngine;
using CSCore;
using CSCore.SoundIn;
using System;

public class CSCoreTester : MonoBehaviour {
	public static CSCoreTester instance { get; private set; }

	WasapiLoopbackCapture capture;
	WaveIn waveIn = new WaveIn();

	private float[] data;

	[Range(0, 1)]
	public float spectrumMax;

	private void Awake() {
		if (instance && instance != this)
			Destroy(instance.gameObject);
		else
			instance = this;
	}

	private void Start() {
		capture = new WasapiLoopbackCapture();
		capture.Initialize();

		capture.DataAvailable += (s, e) => {
			data = new float[e.Data.Length / 4];
			Buffer.BlockCopy(e.Data, 0, data, 0, e.Data.Length);
			spectrumMax = Mathf.Clamp01(max(data));
		};

		capture.Start();
	}

	private float max(float[] array) {
		float res = array[0];
		for (int i = 0; i < array.Length; i++) {
			if (array[i] > res)
				res = array[i];
		}

		return res;
	}
}
