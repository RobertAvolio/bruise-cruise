using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class BetterRetroCamera : MonoBehaviour
{
    [SerializeField] private Material effectMaterial;

    void OnRenderImage(RenderTexture src, RenderTexture dst)
    {
        src.antiAliasing = 0;
        dst.antiAliasing = 0;
        Graphics.Blit(src, dst, effectMaterial);
    }
}

