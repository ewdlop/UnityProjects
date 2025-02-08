Shader "Custom/DarkEffect" {
	Properties{
		_MainTex("Base (RGB)", 2D) = "white" {}
		_VignetteTex("Vignette Texture", 2D) = "white"{}
		_NightVisionColor("Night Vision Color", Color) = (1,1,1,1)
		_Contrast("Contrast", Range(0,4)) = 1
		_Brightness("Brightness", Range(0,2)) = 1
		_RandomValue("Random Value", Float) = 0
		_distortion("Distortion", Float) = 0
		_scale("Scale (Zoom)", Float) =1
		_marioX("MarioX", Float) = 0
		_marioY("MarioY", Float) = 0
	}
	SubShader {

		Pass{
			CGPROGRAM
			#pragma vertex vert_img
			#pragma fragment frag
			#pragma fragmentoption ARB_precision_hint_fastest
			#include "UnityCG.cginc"
			uniform sampler2D _MainTex;
			uniform sampler2D _VignetteTex;
			fixed _Contrast;
			fixed4 _NightVisionColor;
			fixed _Brightness;
			fixed _RandomValue;
			fixed _distortion;
			fixed _scale;
			fixed _marioX;
			fixed _marioY;

		float2 barrelDistortion(float2 coord)
		{
			// lens distortion algorithm
			// See http://www.ssontech.com/content/lensalg.htm
			float2 h = coord - float2(0.5, 0.5);
			float r2 = h.x * h.x + h.y * h.y;
			float f = 1.0 + r2 * (_distortion * sqrt(r2));
			return f * _scale * h + 0.5;
		}
		fixed4 frag(v2f_img i) : COLOR
		{

			//Get the colors from the RenderTexture and the uv's
			//from the v2f_img struct
			half2 distortedUV = barrelDistortion(i.uv);
			fixed4 renderTex = tex2D(_MainTex, distortedUV);
			fixed4 vignetteTex = tex2D(_VignetteTex, i.uv+float2(-1*_marioX,-1*_marioY));
			// get the luminosity values from the render texture using the YIQ values.//
			fixed lum = dot(fixed3(0.299, 0.587, 0.114), renderTex.
					rgb);
			lum += _Brightness;
			fixed4 finalColor = (lum * 2) + _NightVisionColor;
			//Final output
			finalColor = pow(finalColor, _Contrast);
			finalColor *= vignetteTex;
			return finalColor;
		}
			ENDCG
		}
	

		
	}
	FallBack "Diffuse"
}
