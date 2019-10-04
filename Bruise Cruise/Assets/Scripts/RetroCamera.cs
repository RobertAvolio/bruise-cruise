using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class RetroCamera : MonoBehaviour
{
    [SerializeField] private Material effectMaterial;

    void OnRenderImage(RenderTexture src, RenderTexture dst) {
        Graphics.Blit(src,dst,effectMaterial);
    }
}
