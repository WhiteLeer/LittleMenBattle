Shader "Custom/N"
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

        sampler2D _CameraDepthNormalsTexture;

        v2f vert(a2v v)
        {
            v2f o;
            o.uv = v.uv;
            o.posCS = UnityObjectToClipPos(v.posOS);
            return o;
        }

        float4 frag(v2f i):SV_Target
        {
            float4 depthNormal = tex2D(_CameraDepthNormalsTexture, i.uv);
            DecodeDepthNormal(depthNormal, depthNormal.a, depthNormal.rgb);

            // VS 下的 normal
            return float4(depthNormal.rgb, 1);
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
}