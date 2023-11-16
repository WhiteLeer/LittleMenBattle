Shader "Custom/Test"
{
    Properties
    {
        _Alpha("Alpha",range(0,1))=0.5
    }
    SubShader
    {
        CGINCLUDE
        #include "UnityCG.cginc"

        float _Alpha;

        struct a2v
        {
            float4 posOS:POSITION;
        };

        struct v2f
        {
            float4 posCS:SV_POSITION;
        };

        v2f vert(a2v v)
        {
            v2f o;
            o.posCS = UnityObjectToClipPos(v.posOS);
            return o;
        }

        float4 frag(v2f i):SV_Target
        {
            return float4(1, 1, 1, _Alpha);
        }
        ENDCG

        Tags
        {
            "Queue"="Transparent"
        }
        pass
        {
            Blend SrcAlpha OneMinusSrcAlpha
            Tags
            {
                "LightMode"="ForwardBase"
            }

            CGPROGRAM
            #pragma multi_compile_fwdbase

            #pragma vertex vert
            #pragma fragment frag
            ENDCG
        }
    }
}