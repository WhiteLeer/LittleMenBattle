Shader "Custom/D"
{
    SubShader
    {
        CGINCLUDE
        #include "UnityCG.cginc"

        struct a2v
        {
            float2 uv:TEXCOORD0;
            float4 posOS:POSITION;
        };

        struct v2f
        {
            float2 uv:TEXCOORD0;
            float4 posCS:SV_POSITION;
        };

        sampler2D _CameraDepthTexture;

        v2f vert(a2v v)
        {
            v2f o;
            o.uv = v.uv;
            o.posCS = UnityObjectToClipPos(v.posOS);
            return o;
        }

        float4 frag(v2f i):SV_Target
        {
            return tex2D(_CameraDepthTexture, i.uv).r * 10;
        }
        ENDCG

        pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            ENDCG
        }
    }
    FallBack "Diffuse"
}