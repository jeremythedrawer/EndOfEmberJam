Shader "Custom/s_preProcessing"
{
	Properties
    {
    }
	HLSLINCLUDE
		#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
		#include "Packages/com.unity.render-pipelines.core/Runtime/Utilities/Blit.hlsl"
		#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/DeclareDepthTexture.hlsl"
		
		TEXTURE2D(_SourceTex);
		SAMPLER(sampler_SourceTex);

		float3 _BlackColor;
		float3 _WhiteColor;


		half4 frag(Varyings input) : SV_TARGET
		{
			return half4(0,0,0,0);
		}
	ENDHLSL

	Subshader
	{
		Tags { "RenderType" = "Opaque" "RenderPipeline" = "UniversalPipeline" }
		LOD 100
		
		Pass
		{
			Name "PreProcessing"
			HLSLPROGRAM
			#pragma vertex Vert
			#pragma fragment frag
			ENDHLSL
		}
	}
}
