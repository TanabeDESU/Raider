#define VS_SHADERMODEL vs_4_0_level_9_1
#define PS_SHADERMODEL ps_4_0_level_9_1

Texture2D SpriteTexture;

Texture2D warp;
float2 circlePosition ;
float radius ; float thickness;
string name:CircleWarp;
//st_circle()
//{
//	float radius = 0.2;
//	float opacity = 0.5;
//
//	if (asin(s / radius - (1 / radius / 2)) < acos(t / radius - (1 / radius / 2)) &&
//		asin((1 - s) / radius - (1 / radius / 2)) < acos(t / radius - (1 / radius / 2)) &&
//		asin((1 - s) / radius - (1 / radius / 2)) < acos((1 - t) / radius - (1 / radius / 2)) &&
//		asin(s / radius - (1 / radius / 2)) < acos((1 - t) / radius - (1 / radius / 2))) {
//		opacity = 1;
//	}
//
//
//	Oi = Os * opacity;
//	Ci = Cs * Oi;
//}
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
	float4 nOutput = tex2D(SpriteTextureSampler, input.TextureCoordinates);
	if (input.TextureCoordinates.x <= circlePosition.x - radius || input.TextureCoordinates.x >= circlePosition.x + radius||input.TextureCoordinates.y <= circlePosition.y - radius || input.TextureCoordinates.y >= circlePosition.y + radius)
	{
		return nOutput * input.Color;
	}
	//float4 colorWarpSample = tex2D(SpriteTextureSampler, input.TextureCoordinates);
	float dis =  (input.TextureCoordinates.x - circlePosition.x)*(input.TextureCoordinates.x - circlePosition.x) / 0.5625f / 0.5625f + (input.TextureCoordinates.y - circlePosition.y)*(input.TextureCoordinates.y - circlePosition.y);
	dis = sqrt(dis) ;
	if (dis <= radius&&dis>=radius-thickness) {
		//input.TextureCoordinates.x += (0.5f-colorWarpSample.a)*0.05f;
		//input.TextureCoordinates.y += (0.5f - colorWarpSample.b)*0.02f;
		float xDelta = input.TextureCoordinates.x - circlePosition.x;
		float yDelta = input.TextureCoordinates.y - circlePosition.y;

		input.TextureCoordinates.x += sin(radians(input.TextureCoordinates.y * 360-0.5f-input.TextureCoordinates.y )) *(xDelta);
		input.TextureCoordinates.y += sin(radians(input.TextureCoordinates.x * 360-0.5f -input.TextureCoordinates.x)) *yDelta;
		float4 output = tex2D(SpriteTextureSampler, input.TextureCoordinates);

		return output * input.Color;
	}
	return nOutput * input.Color;
}

technique SpriteDrawing
{
	pass P0
	{
		PixelShader = compile PS_SHADERMODEL MainPS();
	}
}