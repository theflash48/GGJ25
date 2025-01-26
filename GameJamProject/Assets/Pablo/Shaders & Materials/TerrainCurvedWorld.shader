Shader "Custom/TerrainCurvedWorld"
{
    Properties
    {
        _Color("Color", Color) = (1, 1, 1, 1)
        _CurveIntensity("Curve Intensity", float) = 0.0
    }
    SubShader
    {
        Tags { "RenderType" = "Opaque" }
        LOD 200

        CGPROGRAM
        // Terrain shader with vertex modification
        #pragma surface surf Standard fullforwardshadows vertex:vert addshadow

        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0

        // Terrain inputs
        struct Input
        {
            float2 uv_MainTex;
        };

        float _CurveIntensity;
        fixed4 _Color;

        // Unity's terrain splatmap control
        sampler2D _Control; // The splatmap
        sampler2D _Splat0;  // Texture for the first layer
        sampler2D _Splat1;  // Texture for the second layer
        sampler2D _Splat2;  // Texture for the third layer
        sampler2D _Splat3;  // Texture for the fourth layer

        void vert(inout appdata_full v)
        {
            float4 worldPos = mul(unity_ObjectToWorld, v.vertex);

            // Apply curvature
            float curve = worldPos.x * worldPos.x * -0.1 * _CurveIntensity;
            worldPos.y += curve;

            v.vertex = mul(unity_WorldToObject, worldPos);
        }

        void surf(Input IN, inout SurfaceOutputStandard o)
        {
            // Read the splatmap at the UV position
            fixed4 splatControl = tex2D(_Control, IN.uv_MainTex);

            // Sample each texture layer and blend them
            fixed4 col0 = tex2D(_Splat0, IN.uv_MainTex * 0.5) * splatControl.r;
            fixed4 col1 = tex2D(_Splat1, IN.uv_MainTex * 0.5) * splatControl.g;
            fixed4 col2 = tex2D(_Splat2, IN.uv_MainTex * 0.5) * splatControl.b;
            fixed4 col3 = tex2D(_Splat3, IN.uv_MainTex * 0.5) * splatControl.a;

            // Combine the colors based on the splatmap weights
            fixed4 col = col0 + col1 + col2 + col3;

            o.Albedo = col.rgb * _Color.rgb;
            o.Metallic = 0.0;
            o.Smoothness = 0.5;
            o.Alpha = col.a;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
