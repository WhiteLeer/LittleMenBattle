using System;
using UnityEngine;

public class Sobel : MonoBehaviour
{
    public ComputeShader sobel;
    public Color edgeColor;

    public Material nMaterial;
    public Material dMaterial;

    [Range(0, 3)] public float size;
    [Range(0, 1)] public float filter;
    [Range(0, 1)] public float bright;

    [Range(0, 1)] public float scaleN;
    [Range(0, 1)] public float scaleD;

    // [Range(0, 100)] public float dScale;

    private RenderTexture _tempTexture;
    private RenderTexture _nTexture;
    private RenderTexture _dTexture;

    private int _targetKernelIndex;

    private void Start()
    {
        _tempTexture = new RenderTexture(Screen.width, Screen.height, 0);
        _tempTexture.enableRandomWrite = true;
        _tempTexture.Create();

        _nTexture = new RenderTexture(Screen.width, Screen.height, 0);
        _nTexture.enableRandomWrite = true;
        _nTexture.Create();

        _dTexture = new RenderTexture(Screen.width, Screen.height, 0);
        _dTexture.enableRandomWrite = true;
        _dTexture.Create();

        Camera mainCamera = Camera.main;
        mainCamera!.depthTextureMode |= DepthTextureMode.DepthNormals;
        mainCamera!.depthTextureMode |= DepthTextureMode.Depth;

        // 由于深度差太小，需要乘一个系数

        _targetKernelIndex = sobel.FindKernel("EdgeDetect");
        sobel.SetTexture(_targetKernelIndex, "nTexture", _nTexture);
        sobel.SetTexture(_targetKernelIndex, "dTexture", _dTexture);
        sobel.SetTexture(_targetKernelIndex, "tempTexture", _tempTexture);

        sobel.SetVector("_color", edgeColor);

        sobel.SetFloat("_size", size);
        sobel.SetFloat("_filter", filter);
        sobel.SetFloat("_bright", bright);

        sobel.SetFloat("_scaleN", scaleN);
        sobel.SetFloat("_scaleD", scaleD * 10);

        sobel.SetInt("_width", Screen.width);
        sobel.SetInt("_height", Screen.height);
    }

    private void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        Graphics.Blit(_tempTexture, _nTexture, nMaterial);
        Graphics.Blit(_tempTexture, _dTexture, dMaterial);

        Graphics.Blit(src, _tempTexture);

        sobel.SetVector("_color", edgeColor);

        sobel.SetFloat("_size", size);
        sobel.SetFloat("_filter", filter);
        sobel.SetFloat("_bright", bright);

        sobel.SetFloat("_scaleN", scaleN);
        sobel.SetFloat("_scaleD", scaleD * 10);

        sobel.Dispatch(_targetKernelIndex, Screen.width / 8, Screen.height / 8, 1);

        Graphics.Blit(_tempTexture, dest);
    }
}