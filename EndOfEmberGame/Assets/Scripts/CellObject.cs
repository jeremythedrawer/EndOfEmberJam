using UnityEngine;

public class CellObject : MonoBehaviour
{
    public MaterialPropertyBlock matPropBlock;
    public MeshRenderer meshRenderer;
    private void Start()
    {
        matPropBlock = new MaterialPropertyBlock();
    }
    private void Update()
    {

    }
}
