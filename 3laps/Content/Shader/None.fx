//sampler inputTexture;
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



sampler2D SpriteTextureSampler = sampler_state
{
	Texture = <SpriteTexture>;
};


struct VertexShaderOutput
{
	float4 Position : SV_POSITION;
	float4 Color : COLOR0;
	float2 TextureCoordinates : TEXCOORD0;
};

float4 MainPS(VertexShaderOutput input) : COLOR
{
	float4 output = tex2D(SpriteTextureSampler,input.TextureCoordinates);
	return output * input.Color;
}

technique SpriteDrawing
{
	pass P0
	{
		PixelShader = compile PS_SHADERMODEL MainPS();
	}
}