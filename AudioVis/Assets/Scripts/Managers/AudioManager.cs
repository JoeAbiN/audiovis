using UnityEngine;

public class AudioManager : MonoBehaviour {
    public static AudioManager instance { get; private set; }

    [Range(0, 1)] public float spectrumMax;

    // Debug
    public bool isAudioOn = true;

    void Awake() {
        if (instance && instance != this)
            Destroy(this.gameObject);
        else
            instance = this;
    }

    private void OnAudioFilterRead(float[] data, int channels) {
        spectrumMax = Mathf.Clamp01(max(data));
    }

    private float max(float[] array) {
        float res = array[0];
        for (int i = 0; i < array.Length; i++) {
            if (array[i] > res)
                res = array[i];
        }

        return res;
    }

    void Update(){
        // Debug
        if (Input.GetKey(KeyCode.Space))
            isAudioOn = !isAudioOn;
    }
}