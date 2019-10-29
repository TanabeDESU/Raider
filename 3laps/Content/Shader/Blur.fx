﻿//sampler inputTexture;
//
//float4 MainPS(float2 textureCoordinates: TEXCOORD0) : COLOR0
//{
//	float4 color = tex2D(inputTexture, textureCoordinates);
//	return color;
//}
//
//technique Techninque1
//{
//	pass Pass1
//	{
//		PixelShader = compile ps_4_0_level_9_1 MainPS();
//		AlphaBlendEnable = TRUE;
//		DestBlend = INVSRCALPHA;
//		SrcBlend = SRCALPHA;
//	}
//};

#define VS_SHADERMODEL vs_4_0_level_9_1
#define PS_SHADERMODEL ps_4_0_level_9_1

Texture2D SpriteTexture;
Texture2D warp;
float alpha ;
float radius;
/*
1.四角を定義する
2.四角の外側を中心からの距離だけ引き伸ばす
*/

sampler2D SpriteTextureSampler = sampler_state
{
	Texture = <SpriteTexture>;
};
sampler2D WarpTextureSampler = sampler_state {
	Texture = <warp>;
};


struct VertexShaderOutput
{
	float4 Position : SV_POSITION;
	float4 Color : COLOR0;
	float2 TextureCoordinates : TEXCOORD0;
};

float4 MainPS(VertexShaderOutput input) : COLOR
{
	float4 output = tex2D(SpriteTextureSampler, input.TextureCoordinates);
	float4 effect = tex2D(WarpTextureSampler, input.TextureCoordinates);

	float dis = (input.TextureCoordinates.x - 0.5f)*(input.TextureCoordinates.x - 0.5f)  + (input.TextureCoordinates.y -0.5f)*(input.TextureCoordinates.y - 0.5f);
	dis = sqrt(dis);
	if (dis > radius)
	{
		output -= effect * alpha;
	}
	return output * input.Color;
}

technique SpriteDrawing
{
	pass P0
	{
		PixelShader = compile PS_SHADERMODEL MainPS();
	}
}