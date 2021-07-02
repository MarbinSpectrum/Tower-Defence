using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Custom;

public class TowerLevelMat : MonoBehaviour
{
    private MaterialPropertyBlock mpb;

    public MeshRenderer meshRenderer;

    [ColorUsage(false)]
    public List<Color> lvColor;
    public List<Texture> lvTx;

    private int nowLevel;

    public int IdxCount;

    public void UpdateMat(int level)
    {
        if (nowLevel == level)
            return;
        nowLevel = level;

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
