Shader "ROSATOM Genplan" {
Properties {
	_Color ("Main Color", Color) = (1,1,1,1)
	_MainTex ("Base (RGB) Trans (A)", 2D) = "white" {}
	_Lightmap ("Lightmap", 2D) = "white" {}
    _Brightness ("Brightness", Float) = 1.0
    _Shift ("Shift", Float) = 1.0
}
SubShader {
	Tags {"Queue" = "Transparent" "IgnoreProjector"="True" "RenderType"="Transparent"}
	Fog { Mode Off }
	Pass {
        ColorMask 0
    }
    	Blend SrcAlpha OneMinusSrcAlpha
    	ZWrite Off
        Cull Back
        ColorMask RGB
CGPROGRAM
#pragma surface surf NoLighting alpha:blend noambient
#pragma fragmentoption ARB_precision_hint_fastest
#include "UnityCG.cginc"
sampler2D _MainTex;
sampler2D _Lightmap;
float _Shift;
float _Brightness;
fixed4 _Color;

struct Input {
	float2 uv2_Lightmap;
	float2 uv_MainTex;
};
     fixed4 LightingNoLighting(SurfaceOutput s, fixed3 lightDir, fixed atten)
     {
         fixed4 c;
         c.rgb = s.Albedo;
         c.a = s.Alpha;
         return c;
     }
void surf (Input IN, inout SurfaceOutput o) {
	fixed4 tex = tex2D(_MainTex, (IN.uv_MainTex * _Shift)) + tex2D(_MainTex, IN.uv_MainTex);
	fixed4 lm = tex2D(_Lightmap, IN.uv2_Lightmap);
	fixed4 c = tex * _Color;
	c *= lm  * _Brightness;;
	o.Albedo = c.rgb;
	o.Alpha = _Color.a;
}
ENDCG
}
FallBack "Legacy Shaders/Reflective/VertexLit"
}
