using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : PostEffect
{
    private const string _Texture = "MainTex";

    public Texture texture;

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        if (Material != null)
        {
            Material.SetTexture(_Texture, texture);
            Graphics.Blit(source, destination, Material);
        }
        else
            Graphics.Blit(source, destination);
    }

}
 
