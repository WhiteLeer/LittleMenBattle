Shader "Custom/Test"
{
    Properties
    {
        _MainTex("MainTex",2D)="white"{}
        _Cutoff("Cutoff",Range(0,1))=0.5
    }
    SubShader
    {
        Cull OFF

        CGPROGRAM
        #pragma surface surf Lambert

        sampler2D _MainTex;
        float _Cutoff;

        struct Input
        {
            float2 uv_MainTex;
        };

        void surf(Input IN, inout SurfaceOutput o)
        {
            fixed4 color = tex2D(_MainTex, IN.uv_MainTex);
            clip(color.a - _Cutoff);

            o.Albedo = color.rgb;
            o.Normal = float3(1, 1, 1);
        }
        ENDCG
    }
}