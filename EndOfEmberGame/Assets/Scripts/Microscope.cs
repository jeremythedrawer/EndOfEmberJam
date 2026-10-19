using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using UnityEngine;

public class Microscope : MonoBehaviour
{
    public MeshRenderer meshRenderer;
    public CameraSO camData;
    public ActionSO actionData;

    [Header("Generated")]
    public PetriDishSO curPetriDish;

    public MaterialPropertyBlock matPropBlock;
    public void Start()
    {
        Init();
    }
    private void OnEnable()
    {
        actionData.onEnterMicroscopeView += Open;
        actionData.onExitMicroscopeView += Close;
    }
    private void OnDisable()
    {
        actionData.onEnterMicroscopeView -= Open;
        actionData.onExitMicroscopeView -= Close;
    }
    private void Init()
    {
        matPropBlock = new MaterialPropertyBlock();
    }

    private void Open()
    {
        matPropBlock.SetFloat("_Alpha", 1);
        meshRenderer.SetPropertyBlock(matPropBlock);
    }
    private void Close()
    {
        matPropBlock.SetFloat("_Alpha", 0);
        meshRenderer.SetPropertyBlock(matPropBlock);
    }
}
