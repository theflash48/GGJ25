Shader "Custom/BubbleShader"
{
    Properties
    {
        _Color("Bubble Color", Color) = (1, 1, 1, 0.5)
        _Smoothness("Smoothness", Range(0, 1)) = 0.8
        _Distortion("Distortion Amount", Range(0, 1)) = 0.1
        _FresnelPower("Fresnel Power", Range(1, 10)) = 5.0
        _MainTex("Texture", 2D) = "white" {}
    }

    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue"="Transparent" }
        LOD 200

        Pass
        {
            Blend SrcAlpha OneMinusSrcAlpha
            Cull Back
            ZWrite Off

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float4 pos : SV_POSITION;
                float2 uv : TEXCOORD0;
                float3 worldNormal : TEXCOORD1;
                float3 worldPos : TEXCOORD2;
            };

            sampler2D _MainTex;
            float4 _Color;
            float _Smoothness;
            float _Distortion;
            float _FresnelPower;

            v2f vert(appdata v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                o.worldNormal = normalize(UnityObjectToWorldNormal(v.normal));
                o.worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                // Fresnel effect
                float3 viewDir = normalize(_WorldSpaceCameraPos - i.worldPos);
                float fresnel = pow(1.0 - dot(viewDir, i.worldNormal), _FresnelPower);

                // Distortion effect
                float2 distortedUV = i.uv + i.worldNormal.xy * _Distortion;
                float4 texColor = tex2D(_MainTex, distortedUV);

                // Combine fresnel and texture color
                float4 bubbleColor = _Color * fresnel;
                bubbleColor.rgb += texColor.rgb * fresnel;

                bubbleColor.a = _Color.a * fresnel;
                return bubbleColor;
            }
            ENDCG
        }
    }

    FallBack "Transparent/Diffuse"
}
