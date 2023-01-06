using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualManager : MonoBehaviour {
    public static VisualManager instance { get; private set; }

    public bool is3D = true;

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
            Destroy(gameObject);
        else
            instance = this;
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) is3D = !is3D;
        
        if (!is3D) return;

        if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Keypad1))
            shape = Shape.Sphere;
        else if (Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Keypad2))
            shape = Shape.Box;
        else if (Input.GetKeyDown(KeyCode.Alpha3) || Input.GetKeyDown(KeyCode.Keypad3))
            shape = Shape.Torus;
    }
}
