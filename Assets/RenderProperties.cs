using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[ExecuteInEditMode]
public class RenderProperties : MonoBehaviour
{
    [ColorUsage(true, true)]
    public Color color = Color.white;
    [ColorUsage(true, true)]
    public Color[] extraColors = new Color[0];
    public Texture texture;

    public Gradient gradient;

    private MaterialPropertyBlock mpb;
    private SkinnedMeshRenderer mr;

    // Start is called before the first frame update
    void Start()
    {
        OnValidate();
    }

    private void OnValidate()
    {

        mr = GetComponentInChildren<SkinnedMeshRenderer>();
        if (mr)
        {
            SetPropertyBlocks();
        }

    }

    public Color GetRandomColor()
    {
        return gradient.Evaluate(Random.value);
    }

    //do I need to make a new block every time?
    void SetPropertyBlocks()
    {
        if (extraColors.Length > 0)
        {
            mpb = new MaterialPropertyBlock();
            mpb.SetColor("_Color", GetRandomColor());
            if (texture != null)
            {
                mpb.SetTexture("_MainTex", texture);
            }
            mr.SetPropertyBlock(mpb, 0);

            for (int i = 0; i < extraColors.Length; i++)
            {
                mpb.SetColor("_Color", extraColors[i]);
                if (texture != null)
                {
                    mpb.SetTexture("_MainTex", texture);
                }
                mr.SetPropertyBlock(mpb, i + 1);
            }
        }
        else
        {
            mpb = new MaterialPropertyBlock();
            mpb.SetColor("_Color", GetRandomColor());
            if (texture != null)
            {
                mpb.SetTexture("_MainTex", texture);
            }
            mr.SetPropertyBlock(mpb);
        }
    }
}