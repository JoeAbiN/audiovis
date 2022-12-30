using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualManager : MonoBehaviour {
    public static VisualManager instance { get; private set; }

    public enum Shape {
        Sphere,
        Box,
        Torus
    }

    public Shape shape = Shape.Sphere;

    public Transform directionalLight;
    public float wobbleFrequency = 3;

    void Awake() {
        if (instance && instance != this)
            Destroy(this.gameObject);
        else
            instance = this;
    }

    void Update(){
        if (Input.GetKey(KeyCode.Alpha1) || Input.GetKey(KeyCode.Keypad1))
            shape = Shape.Sphere;
        else if (Input.GetKey(KeyCode.Alpha2) || Input.GetKey(KeyCode.Keypad2))
            shape = Shape.Box;
        else if (Input.GetKey(KeyCode.Alpha3) || Input.GetKey(KeyCode.Keypad3))
            shape = Shape.Torus;
    }
}
