Shader "Unlit/CelOutline"
{
    Properties
    {
        _Thickness ("Thickness", Float) = 0.4
        _Color("Color", Color) = (1,1,1,1)
        _MainTex("Color (RGB) Alpha (A)", 2D) = "white" {}


    }
    SubShader
    {
        Tags { "Queue" = "Transparent" "RenderType" = "Transparent" "RenderPipeline"="UniversalPipeline"}

        Pass
        {
            HLSLPROGRAM

            #pragma vertex vert
            #pragma fragment frag
           

            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
  

            struct App 
            {
                float4 positionOS : POSITION;
                half3 normal      : NORMAL;
                float4 color      : COLOR;

            };

            struct v2f
            {
                float4 positionHCS  : SV_POSITION;
                half3 normal        : TEXCOORD0;
                half3 worldPos      : TEXCOORD1;
                half3 viewDir       : TEXCOORD2;
                float2 texcoord     : TEXCOORD3;

            };

            float _Thickness;
            float4 _Color;
            sampler2D _MainTex;
            float4 _MainText_ST;

            v2f vert(App IN)
            {
                v2f OUT;

                OUT.positionHCS = TransformObjectToHClip(IN.positionOS.xyz);
                OUT.normal = TransformObjectToWorldNormal(IN.normal);
                OUT.worldPos = mul(unity_ObjectToWorld, IN.positionOS);
                OUT.viewDir = normalize(GetWorldSpaceViewDir(OUT.worldPos));
                o.texcoord = TRANSFORM_TEX(IN.texcoord, _MainTex);

                return OUT;
            }

            half4 frag(v2f IN) : SV_TARGET
            {

                float dotProduct = dot(IN.normal, IN.viewDir);
                dotProduct = step(_Thickness, dotProduct);

                float4 col = tex2D(_MainTex, IN.texcoord).rgb;
                col.Alpha = tex2D(_MainTex, IN.texcoord).a;

                float4 finalColour = col * dotProduct;

                return float4(finalColour, 1.0);
            }


            ENDHLSL
        }
    }
}
