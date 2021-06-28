using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using cakeslice;

namespace cakeslice
{
    public class OutlineAnimation : MonoBehaviour
    {
        bool pingPong = false;

        public int color;

        OutlineEffect outlineEffect;

        void Update()
        {
            Color c = new Color(0,0,0);

            if (outlineEffect == null)
                outlineEffect = GetComponent<OutlineEffect>();

            if (color == 0)
                c = outlineEffect.lineColor0;
            else if (color == 1)
                c = outlineEffect.lineColor1;
            else if (color == 2)
                c = outlineEffect.lineColor2;

            if (pingPong)
            {
                c.a += Time.deltaTime;

                if(c.a >= 1)
                    pingPong = false;
            }
            else
            {
                c.a -= Time.deltaTime;

                if(c.a <= 0)
                    pingPong = true;
            }

            c.a = Mathf.Clamp01(c.a);

            if (color == 0)
                outlineEffect.lineColor0 = c;
            else if (color == 1)
                outlineEffect.lineColor1 = c;
            else if (color == 2)
                outlineEffect.lineColor2 = c;

            outlineEffect.UpdateMaterialsPublicProperties();
        }
    }
}