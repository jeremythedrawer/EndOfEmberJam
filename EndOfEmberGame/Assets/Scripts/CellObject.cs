using UnityEngine;

public class CellObject : MonoBehaviour
{
    public MaterialPropertyBlock matPropBlock;
    public MeshRenderer meshRenderer;
    public void Init()
    {
        matPropBlock = new MaterialPropertyBlock();
    }
    public void SetPropBlock(float normID)
    {
        matPropBlock.SetFloat("_TimeOffset", normID * 100);
        meshRenderer.SetPropertyBlock(matPropBlock);


    }
}
