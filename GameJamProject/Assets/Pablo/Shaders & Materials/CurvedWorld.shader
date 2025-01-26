Shader "Custom/CurvedWorld"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _Glossiness ("Smoothness", Range(0,1)) = 0.5
        _Metallic ("Metallic", Range(0,1)) = 0.0
        _CurveIntensity ("Curve Intensity", float) = 0.0
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Standard fullforwardshadows vertex:vert

        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0

        sampler2D _MainTex;

        struct Input
        {
            float2 uv_MainTex;
        };

        half _Glossiness;
        half _Metallic;
        fixed4 _Color;
        float _CurveIntensity;

        // Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
        // See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
        // #pragma instancing_options assumeuniformscaling
        UNITY_INSTANCING_BUFFER_START(Props)
            // put more per-instance properties here
        UNITY_INSTANCING_BUFFER_END(Props)

        
        void vert(inout appdata_full v)
        {
            float4 distance = mul(unity_ObjectToWorld, v.vertex);

            distance.xyz -= _WorldSpaceCameraPos;

            //distance = float4(0.0f, (distance.z * distance.z * -0.1 * _CurveIntensity) ,0.0f, 0.0f); // Uncomment for curve on Z
            distance = float4(0.0f, (distance.x * distance.x * -0.1 * _CurveIntensity) ,0.0f, 0.0f); // Uncomment for curve on X
            //distance = float4(0.0f, ((distance.x * distance.x + distance.z * distance.z) * -0.1 * _CurveIntensity) ,0.0f, 0.0f); // Uncomment for curve on X & Z

            v.vertex += mul(unity_WorldToObject, distance);
        }

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            // Albedo comes from a texture tinted by color
            fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
            o.Albedo = c.rgb;
            // Metallic and smoothness come from slider variables
            o.Metallic = _Metallic;
            o.Smoothness = _Glossiness;
            o.Alpha = c.a;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
