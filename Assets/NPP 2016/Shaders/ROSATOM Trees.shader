Shader "ROSATOM Trees" {
Properties 
{
	_Color ("Main Color", Color) = (1,1,1,1)
    _MainTex ("Base (RGB)", 2D)     = "white" {}
    _Lightmap ("Lightmap", 2D)     = "white" {}
    _Brightness ("Brightness", Float) = 1.0
    _CutOff("Cut off", float) = 0.1
}
SubShader 
{
  //  Tags { "Queue" = "Geometry" "RenderType" = "Opaque" }
  //  LOD 100
    Pass
    {
        Name "FORWARD"
        Tags { "LIGHTMODE"="Always" "RenderType"="Opaque" }
        CGPROGRAM
        #pragma vertex vert
        #pragma fragment frag

        sampler2D   _MainTex;
        sampler2D   _Lightmap;
        float _Brightness;
        fixed4 _Color;
        float  _CutOff;
        struct appdata 
        {
            float4 vertex : POSITION;
            half2 texcoord   : TEXCOORD0;
            half2 texcoord1  : TEXCOORD1;

        };
        struct v2f {
            float4 pos : SV_POSITION;
            half2 uv : TEXCOORD0;
            half2 lightmapuv : TEXCOORD1;
        };
        v2f vert (appdata v) 
        {
            v2f o;
            o.pos = mul( UNITY_MATRIX_MVP, v.vertex );
            o.uv = v.texcoord;
            o.lightmapuv = v.texcoord1;
            return o;
        }
        fixed4 frag (v2f i) : COLOR
        {
       	  fixed4 lm = tex2D(_Lightmap, i.lightmapuv);
            fixed4 color = tex2D(_MainTex, i.uv) * lm  * _Brightness  * _Color;
       		if(color.a < _CutOff) discard;
            return color;
        }
        ENDCG
    } 
}
}