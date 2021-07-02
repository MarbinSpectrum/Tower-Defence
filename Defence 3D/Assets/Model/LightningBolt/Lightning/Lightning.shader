Shader "Custom/Lightning"
{
    Properties
    {
        _MainTex("Albedo (RGB)", 2D) = "white" {}
        [HDR]_Color("Color",Color) = (1,1,1,1)
        _Alpha("Alpha",Range(0,1)) = 0
        [Toggle] _HasAlpha("HasAlpha", Float) = 0
        _CutOut("CutOut",Range(0,1)) = 0.1
        _Cel("Cel",int) = 1
        _Speed("Speed",Float) = 1
        _Offset("Offset",Int) = 1
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue"="Transparent" }
        LOD 200

        CGPROGRAM
        #pragma surface surf Lambert alpha:fade 

        sampler2D _MainTex;
        half _CutOut;
        float _Alpha;
        int _Cel;
        float _Speed;
        fixed4 _Color;
        int _Offset;
        float _HasAlpha;

        struct Input
        {
            float2 uv_MainTex;
        };

        void surf (Input IN, inout SurfaceOutput o)
        {
            float2 v = IN.uv_MainTex;
            v.y += (int)(_Time.y * _Speed) + _Offset;
            v.y /= _Cel;

            fixed4 c = tex2D (_MainTex, v) * _Color;

            o.Emission = c.rgb;
            
            if(_HasAlpha)
                o.Alpha = c.a;
            else
                o.Alpha = ((c.r + c.g + c.b) < _CutOut) ? 0 : 1;
            o.Alpha *= _Alpha;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
