Shader "Custom/Role"
{
    Properties
    {
        _MainTex("MainTex",2D)="white"{}
        _Cutoff("Cutoff",Range(0,1))=0.5
    }
    SubShader
    {
        Cull OFF

        CGINCLUDE
        #include "UnityCG.cginc"
        #include "AutoLight.cginc"

        sampler2D _MainTex;
        float _Cutoff;

        // Data Base
        struct a2vBase
        {
            float2 uv:TEXCOORD0;
            float3 norOS:NORMAL;
            float4 posOS:POSITION;
        };

        struct v2fBase
        {
            float2 uv:TEXCOORD0;
            float3 posWS:TEXCOORD1;
            float4 pos:SV_POSITION;

            UNITY_LIGHTING_COORDS(2, 3)
        };

        // Data Shadow 
        struct a2vShadow
        {
            float2 uv:TEXCOORD0;
            float3 normal:NORMAL;
            float4 vertex:POSITION;
        };

        struct v2fShadow
        {
            V2F_SHADOW_CASTER;
            float2 uv:TEXCOORD1;
        };

        // Realize Base
        v2fBase vertBase(a2vBase v)
        {
            v2fBase o;
            o.uv = v.uv;
            o.posWS = mul(UNITY_MATRIX_M, v.posOS);
            o.pos = UnityObjectToClipPos(v.posOS);

            UNITY_TRANSFER_LIGHTING(o, o._ShadowCoord);
            return o;
        }

        float4 fragBase(v2fBase i):SV_Target
        {
            float4 color = tex2D(_MainTex, i.uv);
            clip(color.a - _Cutoff);

            UNITY_LIGHT_ATTENUATION(atten, i, i.posWS);

            return float4(color.rgb * atten, 1);
        }

        // Realize Shadow
        v2fShadow vertShadow(a2vShadow v)
        {
            v2fShadow o;
            o.uv = v.uv;
            TRANSFER_SHADOW_CASTER_NORMALOFFSET(o)
            return o;
        }

        float4 fragShadow(v2fShadow i):SV_Target
        {
            float4 color = tex2D(_MainTex, i.uv);
            clip(color.a - _Cutoff);

            SHADOW_CASTER_FRAGMENT(i)
        }
        ENDCG

        Tags
        {
            "Queue"="AlphaTest"
        }
        pass
        {
            Name "ForwardBasePass"
            Tags
            {
                "LightMode" = "ForwardBase"
            }

            CGPROGRAM
            #pragma multi_compile_fwdbase

            #pragma vertex vertBase
            #pragma fragment fragBase
            ENDCG
        }
        Pass
        {
            Name "ShadowCasterPass"
            Tags
            {
                "LightMode" = "ShadowCaster"
            }
            CGPROGRAM
            #pragma multi_compile_shadowcaster

            #pragma vertex vertShadow
            #pragma fragment fragShadow
            ENDCG
        }
    }
    
    FallBack "Hidden/Internal-DepthNormalsTexture"
}