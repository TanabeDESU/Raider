
#if OPENGL
#define SV_POSITION POSITION
#define VS_SHADERMODEL vs_3_0
#define PS_SHADERMODEL ps_3_0
#else
#define VS_SHADERMODEL vs_4_0_level_9_1
#define PS_SHADERMODEL ps_4_0_level_9_1
#endif

texture myTexure;

float2 position;
sampler inputTexture;

float delta;



// ----------------------------------------------------------------
// 入出力用の構造体
//struct VertexShaderInput
//{
//	float4 Position : POSITION0;
//	float4 TextureCoordinate : TEXCOORD;
//};
//
//struct VertexShaderOutput
//{
//	float4 Position : SV_POSITION;
//	float4 TextureCoordinate : TEXCOORD;
//};

// ----------------------------------------------------------------
// バーテックスシェーダー
//VertexShaderOutput MainVS(in VertexShaderInput input)
//{
//	// 出力用の頂点
//	VertexShaderOutput output = (VertexShaderOutput)0;
//
//	// 頂点をカメラから見た座標に変換
//	//output.Position = mul(input.Position, mul(myView, myProjection));
//
//	// テクスチャ座標をコピー
//	output.TextureCoordinate = input.TextureCoordinate;
//
//	return output;
//}

// ----------------------------------------------------------------
//// ピクセルシェーダー
//float4 MainPS(VertexShaderOutput input) : COLOR
//{
//	// テクスチャサンプラーからピクセルに対応する色を抜き出す
//	return tex2D(inputTexture, input.TextureCoordinate);
//}
float4 MainPS(float2 input : TEXC0ORD0) : COLOR
{
    float4 output;
input.y += delta;
	input.x+=1.00f;
	input.y-=0.87f;
	input.x *= 16;
	input.y *= 8;
	output = tex2D(inputTexture, input);
    return output;
}
// ----------------------------------------------------------------
// テクニック、いわゆるエントリーポイント
technique MyTechnique
{
	pass MyPass
	{
		//VertexShader = compile VS_SHADERMODEL MainVS();
		PixelShader = compile PS_SHADERMODEL MainPS();
	}
};