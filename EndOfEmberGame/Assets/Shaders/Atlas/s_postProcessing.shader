Shader "Custom/s_postProcessing"
{
	Properties
    {
		[NoScaleOffset] _NoiseTexture("Noise Texture", 2D) = "white" {}

		_VignetteDistance("Vignette Distance", Range(0,30)) = 0
		_VignetteDensity("Vignette Density", Range(0,1)) = 0

		_NoiseScale("Noise Scale", Range(0,1)) = 1
		_NoiseOffset("Noise Offset", Vector) = (0,0,0,0)

		_Fade("Fade", Range(0,1)) = 0

		_Focus("Focus", Range(0,0.005)) = 0

		_Invert("Invert", Float) = 0

    }

    HLSLINCLUDE
        #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
		#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/DeclareDepthTexture.hlsl"
		#include "Packages/com.unity.render-pipelines.core/Runtime/Utilities/Blit.hlsl"

		#include "Assets/Shaders/Atlas/HLSL/DitherShaderFunctions.hlsl"

        TEXTURE2D(_NoiseTexture);
        SAMPLER(sampler_NoiseTexture);

		CBUFFER_START(UnityPerMaterial)	
			float _VignetteDistance;
			float _VignetteDensity;

			float _NoiseScale;
			float4 _NoiseOffset;

			float _Fade;

			float _Focus;

			float _Invert;
		CBUFFER_END

		float2 _SnapDiff;
		float3 _DarkColor;
		float3 _BrightColor;

		static const float2 BOX_BLUR_OFFSET[4] =
		{
			float2 (-1, -1),
			float2 (1, -1),
			float2 (-1, 1),
			float2 (1, 1),
		};

		half4 frag(Varyings input) : SV_TARGET
		{
			float2 uvDiff = _SnapDiff / _ScreenParams.xy;

			half3 camCol;
			float2 snappedUV = input.texcoord - uvDiff;
			for (int i = 0; i < 4; i++)
			{
				float2 focusOffset = BOX_BLUR_OFFSET[i] * _Focus;

				half3 col = SAMPLE_TEXTURE2D_X(_BlitTexture, sampler_PointClamp, snappedUV + focusOffset).rgb;
				camCol += col;
			}
			camCol /= 4;

			float aspect = _ScreenParams.x / _ScreenParams.y;
			float2 aspectUV = input.texcoord;
			aspectUV.x *= aspect;
			half3 halfToneTex = SAMPLE_TEXTURE2D_X(_NoiseTexture, sampler_NoiseTexture, (aspectUV + (_NoiseOffset * 0.0125)) * _NoiseScale);
			float halfTone = max(halfToneTex.b, halfToneTex.g * 5) * 2 - 1; 
			halfTone *= 0.005;

			float2 squareUV = input.texcoord * (1 - input.texcoord.yx);
			float vignetteSDF  = squareUV.x * squareUV.y * _VignetteDistance;
			vignetteSDF  = pow(vignetteSDF , _VignetteDensity);
			vignetteSDF  = saturate(vignetteSDF);

			vignetteSDF += halfTone;
			vignetteSDF *= _Fade;
			camCol *= vignetteSDF;

			if (_Invert == 1) camCol = 1 - camCol;

			camCol = lerp(_DarkColor, _BrightColor, camCol);


			return half4(camCol, 1);
		}
	ENDHLSL

	SubShader
	{
        Tags { "RenderType" = "Opaque" "RenderPipeline" = "UniversalPipeline"}
        LOD 100
		Pass
		{
			Name "PostProcessing"
			HLSLPROGRAM
			#pragma vertex Vert
			#pragma fragment frag
			ENDHLSL
		}
	}
}
