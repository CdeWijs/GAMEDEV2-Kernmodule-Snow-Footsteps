using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowNoise : MonoBehaviour {

    public Shader snowFallShader;
    [Range(0.001f, 0.1f)]
    public float flakeAmount;
    [Range(0, 1)]
    public float flakeOpacity;

    private Material snowFallMaterial;
    private MeshRenderer meshRenderer;
    
	void Start () {
        meshRenderer = GetComponent<MeshRenderer>();
        snowFallMaterial = new Material(snowFallShader);
	}
	
	void Update () {
        snowFallMaterial.SetFloat("_FlakeAmount", flakeAmount);
        snowFallMaterial.SetFloat("_FlakeOpacity", flakeOpacity);
        RenderTexture _snow = (RenderTexture)meshRenderer.material.GetTexture("_Splat");
        RenderTexture _temp = RenderTexture.GetTemporary(_snow.width, _snow.height, 0, RenderTextureFormat.ARGBFloat);
        Graphics.Blit(_snow, _temp, snowFallMaterial);
        Graphics.Blit(_temp, _snow);
        meshRenderer.material.SetTexture("_Splat", _snow);
        RenderTexture.ReleaseTemporary(_temp);
	}
}
