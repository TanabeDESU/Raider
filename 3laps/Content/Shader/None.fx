
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
// ���o�͗p�̍\����
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
// �o�[�e�b�N�X�V�F�[�_�[
//VertexShaderOutput MainVS(in VertexShaderInput input)
//{
//	// �o�͗p�̒��_
//	VertexShaderOutput output = (VertexShaderOutput)0;
//
//	// ���_���J�������猩�����W�ɕϊ�
//	//output.Position = mul(input.Position, mul(myView, myProjection));
//
//	// �e�N�X�`�����W���R�s�[
//	output.TextureCoordinate = input.TextureCoordinate;
//
//	return output;
//}

// ----------------------------------------------------------------
//// �s�N�Z���V�F�[�_�[
//float4 MainPS(VertexShaderOutput input) : COLOR
//{
//	// �e�N�X�`���T���v���[����s�N�Z���ɑΉ�����F�𔲂��o��
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
// �e�N�j�b�N�A������G���g���[�|�C���g
technique MyTechnique
{
	pass MyPass
	{
		//VertexShader = compile VS_SHADERMODEL MainVS();
		PixelShader = compile PS_SHADERMODEL MainPS();
	}
};