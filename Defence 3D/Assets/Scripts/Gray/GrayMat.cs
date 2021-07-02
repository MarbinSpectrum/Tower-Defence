using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class GrayMat : MonoBehaviour
{
    private MaterialPropertyBlock mpb;

    public Image image;

    [Range(0,1)]
    public float grayValue;
    private float nowGrayValue;

    private void Update() => UpdateMat(grayValue);
    public void UpdateMat(float grayValue)
    {
        if (image == null)
            return;
        if (image.material == null)
            return;
        if (image.material.name != "GrayMat")
        {
            Material material = Instantiate(image.material);
            material.name = "GrayMat";
            image.material = material;
        }
        if (nowGrayValue == grayValue)
            return;
        nowGrayValue = grayValue;

        image.material.SetFloat("_Gray", nowGrayValue);
    }
}
