
#define VS_SHADERMODEL vs_4_0_level_9_1
#define PS_SHADERMODEL ps_4_0_level_9_1

Texture2D SpriteTexture;

Texture2D warp;
float start;
float range;
string name:SinWarp;
float alpha;
sampler2D SpriteTextureSampler = sampler_state
{
	Texture = <SpriteTexture>;
};
sampler2D WarpTextureSampler = sampler_state
{
	Texture = <warp>;
	AddressU = WRAP;
	AddressV = WRAP;
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
	//output.r *= 0.5f;
	output.a *= 0.2;
	input.TextureCoordinates.x += sin(radians(input.TextureCoordinates.y * 360 - start)) *range;

	float4 effect = tex2D(WarpTextureSampler, input.TextureCoordinates);
	effect.a = 1;
	effect.rgb *= 4;
	output -= effect*alpha ;

	return output * input.Color;
}

technique SpriteDrawing
{
	pass P0
	{
		PixelShader = compile PS_SHADERMODEL MainPS();
	}
}
