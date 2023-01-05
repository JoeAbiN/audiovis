using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dispatcher : MonoBehaviour {
    public ComputeShader computeShader;
    private RenderTexture renderTexture;

    private void OnRenderImage(RenderTexture source, RenderTexture destination) {
        InitRenderTexture();
        
        SetShaderParams();

        computeShader.SetTexture(0, "res", renderTexture);
        int threadGroupsX = Mathf.CeilToInt(Screen.width / 8.0f);
        int threadGroupsY = Mathf.CeilToInt(Screen.height / 8.0f);
        computeShader.Dispatch(0, threadGroupsX, threadGroupsY, 1);

        Graphics.Blit(renderTexture, destination);
    }

    private void InitRenderTexture() {
        if (renderTexture == null || renderTexture.width != Screen.width || renderTexture.height != Screen.height) {
            if (renderTexture != null)
                renderTexture.Release();
                
            renderTexture = new RenderTexture(Screen.width, Screen.height, 0,
                RenderTextureFormat.ARGBFloat, RenderTextureReadWrite.Linear);
            renderTexture.enableRandomWrite = true;
            renderTexture.Create();
        }
    }

    private void SetShaderParams() {
        computeShader.SetMatrix("camToWorld", Camera.main.cameraToWorldMatrix);
        computeShader.SetMatrix("camInvProj", Camera.main.projectionMatrix.inverse);

        computeShader.SetInt("shapeIndex", (int)VisualManager.instance.shape);
        computeShader.SetFloat("resolution", renderTexture.width);
        computeShader.SetFloat("time", Time.time);
        computeShader.SetVector("light", VisualManager.instance.directionalLight.forward);
        computeShader.SetFloat("freq", VisualManager.instance.wobbleFrequency);
        computeShader.SetFloat("spectrumMax", AudioManager.instance.isAudioOn ? CSCoreTester.instance.spectrumMax : 0);
    }
}
