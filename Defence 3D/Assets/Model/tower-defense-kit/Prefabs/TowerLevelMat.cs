using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Custom;

[ExecuteInEditMode]
public class TowerLevelMat : MonoBehaviour
{
    private MaterialPropertyBlock mpb;

    public MeshRenderer meshRenderer;

    [ColorUsage(false)]
    public List<Color> lvColor;
    public List<Texture> lvTx;

    public int level;
    public int IdxCount;

    private void Update()
    {
        UpdateOutline();
    }
    private void UpdateOutline()
    {
        if (meshRenderer == null)
            return;

        if (mpb == null)
            mpb = new MaterialPropertyBlock();

        meshRenderer.GetPropertyBlock(mpb, IdxCount);
        if (Exception.IndexOutRange(level, lvColor))
            mpb.SetColor("_Color", lvColor[level]);
        else
            mpb.SetColor("_Color", Color.white);
        meshRenderer.SetPropertyBlock(mpb, IdxCount);


        meshRenderer.GetPropertyBlock(mpb, IdxCount);
        if(Exception.IndexOutRange(level, lvTx))
            mpb.SetTexture("_MainTex", lvTx[level]);
        meshRenderer.SetPropertyBlock(mpb, IdxCount);

    }
}
