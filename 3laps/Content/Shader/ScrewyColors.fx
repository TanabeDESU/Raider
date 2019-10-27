sampler inputTexture;
int pixelation;

float4 MainPS(float2 originalUV: TEXCOORD0) : COLOR0
{
	originalUV *= float2(1920, 1080);

	float2 newUV;
	newUV.x = round(originalUV.x / pixelation) * pixelation;
	newUV.y = round(originalUV.y / pixelation) * pixelation;

	
	newUV /= float2(1920,1080);

	return tex2D(inputTexture, newUV);
}

technique Techninque1
{
	pass Pass1
	{
		PixelShader = compile ps_4_0_level_9_1 MainPS();
	}
};