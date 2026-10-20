#define RED_BIT 1 << 0
#define GREEN_BIT 1 << 1
#define BLUE_BIT 1 << 2
#define INVERT_BIT 1 << 3
#include "Assets/Shaders/Atlas/HLSL/ColorSpace.hlsl"

static const float2 BOX_BLUR_OFFSET[4] =
{
    float2 (-1, -1),
    float2 (1, -1),
    float2 (-1, 1),
    float2 (1, 1),
};

struct AtlasSprite
{
    float4 position;
    float4 pivotAndSize;
    float4 uvSizeAndPos;
    float4 scaleAndFlip;
    float4 custom;
    int customBit;
};


half4 UIColor(int bitMask, half4 tex, float3 darkColor, float3 lightColor)
{

    int redMask = saturate(bitMask & RED_BIT);
    int greenMask = saturate(bitMask & GREEN_BIT);
    int blueMask = saturate(bitMask & BLUE_BIT);

    half whiteTex = tex.r * tex.g * tex.b;
   
    half greenTex = tex.g * greenMask;

    half fullMask = saturate((tex.r + greenMask) - tex.b);
    half invertTex = 1 - fullMask;
    int invertMask = saturate(bitMask & INVERT_BIT);
    half t = lerp(fullMask, invertTex, invertMask);
    
    half alpha = (tex.r + tex.b) + greenMask;
    alpha *= tex.a;
    return half4(fullMask.xxx, alpha);
}
float2 SpriteUV(float2 uv, float4 uvSizeAndPos, float4 scaleAndFlip)
{
    float2 uvSize = uvSizeAndPos.xy;
    float2 uvPos = uvSizeAndPos.zw;
                
    float2 scale = scaleAndFlip.xy;
    float2 flip = scaleAndFlip.zw;

    uv *= scale;
    uv = frac(uv);
    uv = (uv - 0.5) * flip + 0.5;
    uv *= uvSize;
    uv += uvPos;
    
    return uv;
}