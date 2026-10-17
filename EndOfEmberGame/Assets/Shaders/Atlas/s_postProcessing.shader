Shader "Custom/s_postProcessing"
{
	Properties
    {
		[NoScaleOffset] _NoiseTexture("Noise Texture", 2D) = "white" {}

		_VignetteDistance("Vignette Distance", Range(0,30)) = 0
		_VignetteDensity("Vignette Density", Range(0,1)) = 0
		_SquareToCircle("Square To Circle", Range(0,1)) = 0
		_NoiseScale("Noise Scale", Range(0,1)) = 1
    }

    HLSLINCLUDE
        #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
		#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/DeclareDepthTexture.hlsl"
		#include "Packages/com.unity.render-pipelines.core/Runtime/Utilities/Blit.hlsl"

		#include "Assets/Shaders/Atlas/HLSL/DitherShaderFunctions.hlsl"

        TEXTURE2D(_NoiseTexture);
        SAMPLER(sampler_NoiseTexture);

		CBUFFER_START(UnityPerMatieral)	
			float _VignetteDistance;
			float _VignetteDensity;
			float _SquareToCircle;
			float _NoiseScale;
		CBUFFER_END

		float2 _SnapDiff;
		half4 frag(Varyings input) : SV_TARGET
		{
			float2 uvDiff = _SnapDiff / _ScreenParams.xy;
			half3 camCol = SAMPLE_TEXTURE2D_X(_BlitTexture, sampler_PointClamp, input.texcoord - uvDiff).rgb;

			float aspect = _ScreenParams.x / _ScreenParams.y;
			float2 aspectUV = input.texcoord;
			aspectUV.x *= aspect;
			half3 halfToneTex = SAMPLE_TEXTURE2D_X(_NoiseTexture, sampler_NoiseTexture, aspectUV * _NoiseScale);
			float halfTone = max(halfToneTex.b, halfToneTex.g * 5) * 2 - 1; 
			halfTone *= 0.005;

			float2 squareUV = input.texcoord * (1 - input.texcoord.yx);
			float square = squareUV.x * squareUV.y * _VignetteDistance;
			square = saturate(pow(square, _VignetteDensity));
			square += halfTone;

			float2 centerUV = aspectUV * 2 - (1 * float2(aspect, 1));
			float circle = 1- length(centerUV);
			float vignetteSDF = lerp(square, circle, _SquareToCircle);


			return (vignetteSDF.xxxx);

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
