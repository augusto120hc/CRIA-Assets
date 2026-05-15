Shader "Custom/HairColorShader"
{
    Properties
    {
        _MainTex ("Sprite Texture", 2D) = "white" {}
        _HairColor ("Hair Color (Original)", Color) = (0,0.6667,0.6471,1) // #00AAA5
        _TargetColor ("Target Color", Color) = (1,0,0,1)
        _Tolerance ("Tolerance", Range(0,0.5)) = 0.05
    }

    SubShader
    {
        Tags 
        { 
            "RenderType"="Transparent"
            "Queue"="Transparent"
            "RenderPipeline"="UniversalPipeline"
        }

        Blend SrcAlpha OneMinusSrcAlpha
        Cull Off
        ZWrite Off

        Pass
        {
            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

            struct Attributes
            {
                float4 positionOS : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct Varyings
            {
                float4 positionHCS : SV_POSITION;
                float2 uv : TEXCOORD0;
            };

            TEXTURE2D(_MainTex);
            SAMPLER(sampler_MainTex);

            float4 _MainTex_ST;
            float4 _HairColor;
            float4 _TargetColor;
            float _Tolerance;

            Varyings vert (Attributes v)
            {
                Varyings o;
                o.positionHCS = TransformObjectToHClip(v.positionOS.xyz);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            half4 frag (Varyings i) : SV_Target
            {
                half4 col = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, i.uv);

                // distância entre a cor do pixel e a cor do cabelo
                float dist = distance(col.rgb, _HairColor.rgb);

                // cria máscara suave
                float mask = 1.0 - smoothstep(0.0, _Tolerance, dist);

                // mistura a cor original com a nova
                col.rgb = lerp(col.rgb, _TargetColor.rgb, mask);

                return col;
            }
            ENDHLSL
        }
    }
}