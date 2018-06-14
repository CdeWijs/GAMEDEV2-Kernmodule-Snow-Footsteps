using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowTracks: MonoBehaviour {

    public Shader drawShader;
    public GameObject terrain;
    public Transform[] feet;
    [Range(0, 1)]
    public float brushSize;
    [Range(0, 1)]
    public float brushStrength;

    private RaycastHit groundHit;
    private int layerMask;
    private Material snowMaterial, drawMaterial;
    private RenderTexture splatMap;

    // Use this for initialization
    void Start () {
        layerMask = LayerMask.GetMask("Ground");
        drawMaterial = new Material(drawShader);
        snowMaterial = terrain.GetComponent<MeshRenderer>().material;
        splatMap = new RenderTexture(1024, 1024, 0, RenderTextureFormat.ARGBFloat);
        snowMaterial.SetTexture("_Splat", splatMap);
    }
	
	// Update is called once per frame
	void Update () {
		for (int i = 0; i < feet.Length; i++) {
            if (Physics.Raycast(feet[i].position, -Vector3.up, out groundHit, 0.1f, layerMask)) {
                drawMaterial.SetVector("_Coordinate", new Vector4(groundHit.textureCoord.x, groundHit.textureCoord.y, 0, 0));
                drawMaterial.SetFloat("_Strength", brushStrength);
                drawMaterial.SetFloat("_Size", brushSize);
                RenderTexture temp = RenderTexture.GetTemporary(splatMap.width, splatMap.height, 0, RenderTextureFormat.ARGBFloat);
                Graphics.Blit(splatMap, temp);
                Graphics.Blit(temp, splatMap, drawMaterial);
                RenderTexture.ReleaseTemporary(temp);
            }
        }
	}
}
