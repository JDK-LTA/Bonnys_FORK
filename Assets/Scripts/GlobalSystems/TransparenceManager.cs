using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Walls
{
    public GameObject frontWallsParent, backWallsParent, leftWallsParent, rightWallsParent;
}
[System.Serializable]
public struct OutWalls
{
    public GameObject frontWallsParent, frontLeftWallsParent, frontLeftPropsParent, frontRightWallsParent, frontRightPropsParent,
        backWallsParent, backLeftWallsParent, backLeftPropsParent, backRightWallsParent, backRightPropsParent, leftWallsParent, rightWallsParent;
}
[System.Serializable]
public struct Top
{
    public List<GameObject> topParent;
}

public class TransparenceManager : Singleton<TransparenceManager>
{
    [SerializeField] private List<Walls> floorsWalls;
    [SerializeField] private List<OutWalls> outsideWalls;
    [SerializeField] private List<Top> topMeshesParents;
    [SerializeField] private GameObject flag, chimney;

    [SerializeField] private Material semiTransparentMaterialForMeshes;
    [SerializeField] private Material transparentMaterialForMeshes;
    [SerializeField] private float alphaLevelForSpritesTranparency = 0.08f;

    [SerializeField] private Transform floorMidPropsParent;

    private MeshRenderer[] frontWallMeshes, backWallMeshes, leftWallMeshes, rightWallMeshes;
    private MeshRenderer[] outFrontWallMeshes, outFLWallMeshes, outFRWallMeshes, outBackWallMeshes, outBLWallMeshes, outBRWallMeshes, outLeftWallMeshes, outRightWallMeshes;
    private MeshRenderer[] outFLPropsMeshes, outFRPropsMeshes, outBLPropsMeshes, outBRPropsMeshes;
    private MeshRenderer[] topMeshes;
    private SpriteRenderer[] frontWallSprites, backWallSprites, leftWallSprites, rightWallSprites, topSprites;

    private bool topTrans;
    private bool frontTrans, backTrans, leftTrans, rightTrans;
    private bool oFrontTrans, oFLTrans, oFRTrans, oBackTrans, oBLTrans, oBRTrans, oLeftTrans, oRightTrans;
    private bool propsFLTrans, propsFRTrans, propsBLTrans, propsBRTrans;

    private Material[][] frontMats, backMats, leftMats, rightMats;
    private Material[][] propsFLMats, propsFRMats, propsBLMats, propsBRMats;

    private int[] frontMeshLayers, backMeshLayers, leftMeshLayers, rightMeshLayers/*, frontSprLayers, backSprLayers, leftSprLayers, rightSprLayers*/;

    private Collider[] outFrontColliders, outFLColliders, outFRColliders, outBackColliders, outBLColliders, outBRColliders, outLeftColliders, outRightColliders;
    private Collider[] outFLPropsColls, outFRPropsColls, outBLPropsColls, outBRPropsColls;
    private Collider[] topColliders;

    public Transform FloorMidPropsParent { get => floorMidPropsParent; }

    private void Start()
    {
        UpdateMeshesSpritesAndMats();
    }

    public void UpdateMeshesSpritesAndMats()
    {
        UpdateTop();
        UpdateInside();
        UpdateOutside();
    }

    public void UpdateInside()
    {
        UpdateFront();
        UpdateBack();
        UpdateLeft();
        UpdateRight();
    }

    public void UpdateOutside()
    {
        UpdateOutFront();
        UpdateOutFL();
        UpdateOutFR();
        UpdateOutBack();
        UpdateOutBL();
        UpdateOutBR();
        UpdateOutLeft();
        UpdateOutRight();
    }

    #region References Updaters
    public void UpdateOutRight()
    {
        outRightWallMeshes = outsideWalls[GameManager.Instance.Floor].rightWallsParent.GetComponentsInChildren<MeshRenderer>();
        outRightColliders = outsideWalls[GameManager.Instance.Floor].rightWallsParent.GetComponentsInChildren<Collider>();
    }

    public void UpdateOutLeft()
    {
        outLeftWallMeshes = outsideWalls[GameManager.Instance.Floor].leftWallsParent.GetComponentsInChildren<MeshRenderer>();
        outLeftColliders = outsideWalls[GameManager.Instance.Floor].leftWallsParent.GetComponentsInChildren<Collider>();
    }

    public void UpdateOutBR()
    {
        outBRWallMeshes = outsideWalls[GameManager.Instance.Floor].backRightWallsParent.GetComponentsInChildren<MeshRenderer>();
        outBRColliders = outsideWalls[GameManager.Instance.Floor].backRightWallsParent.GetComponentsInChildren<Collider>();

        outBRPropsMeshes = outsideWalls[GameManager.Instance.Floor].backRightPropsParent.GetComponentsInChildren<MeshRenderer>();
        outBRPropsColls = outsideWalls[GameManager.Instance.Floor].backRightPropsParent.GetComponentsInChildren<Collider>();
        propsBRMats = GetMaterials(outBRPropsMeshes);
    }

    public void UpdateOutBL()
    {
        outBLWallMeshes = outsideWalls[GameManager.Instance.Floor].backLeftWallsParent.GetComponentsInChildren<MeshRenderer>();
        outBLColliders = outsideWalls[GameManager.Instance.Floor].backLeftWallsParent.GetComponentsInChildren<Collider>();

        outBLPropsMeshes = outsideWalls[GameManager.Instance.Floor].backLeftPropsParent.GetComponentsInChildren<MeshRenderer>();
        outBLPropsColls = outsideWalls[GameManager.Instance.Floor].backLeftPropsParent.GetComponentsInChildren<Collider>();
        propsBLMats = GetMaterials(outBLPropsMeshes);
    }

    public void UpdateOutBack()
    {
        outBackWallMeshes = outsideWalls[GameManager.Instance.Floor].backWallsParent.GetComponentsInChildren<MeshRenderer>();
        outBackColliders = outsideWalls[GameManager.Instance.Floor].backWallsParent.GetComponentsInChildren<Collider>();
    }

    public void UpdateOutFR()
    {
        outFRWallMeshes = outsideWalls[GameManager.Instance.Floor].frontRightWallsParent.GetComponentsInChildren<MeshRenderer>();
        outFRColliders = outsideWalls[GameManager.Instance.Floor].frontRightWallsParent.GetComponentsInChildren<Collider>();

        outFRPropsMeshes = outsideWalls[GameManager.Instance.Floor].frontRightPropsParent.GetComponentsInChildren<MeshRenderer>();
        outFRPropsColls = outsideWalls[GameManager.Instance.Floor].frontRightPropsParent.GetComponentsInChildren<Collider>();
        propsFRMats = GetMaterials(outFRPropsMeshes);
    }

    public void UpdateOutFL()
    {
        outFLWallMeshes = outsideWalls[GameManager.Instance.Floor].frontLeftWallsParent.GetComponentsInChildren<MeshRenderer>();
        outFLColliders = outsideWalls[GameManager.Instance.Floor].frontLeftWallsParent.GetComponentsInChildren<Collider>();

        outFLPropsMeshes = outsideWalls[GameManager.Instance.Floor].frontLeftPropsParent.GetComponentsInChildren<MeshRenderer>();
        outFLPropsColls = outsideWalls[GameManager.Instance.Floor].frontLeftPropsParent.GetComponentsInChildren<Collider>();
        propsFLMats = GetMaterials(outFLPropsMeshes);
    }

    public void UpdateOutFront()
    {
        outFrontWallMeshes = outsideWalls[GameManager.Instance.Floor].frontWallsParent.GetComponentsInChildren<MeshRenderer>();
        outFrontColliders = outsideWalls[GameManager.Instance.Floor].frontWallsParent.GetComponentsInChildren<Collider>();
    }

    public void UpdateRight()
    {
        rightWallMeshes = floorsWalls[GameManager.Instance.Floor].rightWallsParent.GetComponentsInChildren<MeshRenderer>();
        rightMeshLayers = GetLayers(rightWallMeshes);
        rightWallSprites = floorsWalls[GameManager.Instance.Floor].rightWallsParent.GetComponentsInChildren<SpriteRenderer>();
        rightMats = GetMaterials(rightWallMeshes);
    }

    public void UpdateLeft()
    {
        leftWallMeshes = floorsWalls[GameManager.Instance.Floor].leftWallsParent.GetComponentsInChildren<MeshRenderer>();
        leftMeshLayers = GetLayers(leftWallMeshes);
        leftWallSprites = floorsWalls[GameManager.Instance.Floor].leftWallsParent.GetComponentsInChildren<SpriteRenderer>();
        leftMats = GetMaterials(leftWallMeshes);
    }

    public void UpdateBack()
    {
        backWallMeshes = floorsWalls[GameManager.Instance.Floor].backWallsParent.GetComponentsInChildren<MeshRenderer>();
        backMeshLayers = GetLayers(backWallMeshes);
        backWallSprites = floorsWalls[GameManager.Instance.Floor].backWallsParent.GetComponentsInChildren<SpriteRenderer>();
        backMats = GetMaterials(backWallMeshes);
    }

    public void UpdateFront()
    {
        frontWallMeshes = floorsWalls[GameManager.Instance.Floor].frontWallsParent.GetComponentsInChildren<MeshRenderer>();
        frontMeshLayers = GetLayers(frontWallMeshes);
        frontWallSprites = floorsWalls[GameManager.Instance.Floor].frontWallsParent.GetComponentsInChildren<SpriteRenderer>();
        frontMats = GetMaterials(frontWallMeshes);
    }

    public void UpdateTop()
    {
        List<MeshRenderer> auxTop = new List<MeshRenderer>();
        List<SpriteRenderer> auxSrTop = new List<SpriteRenderer>();
        List<Collider> auxCollsTop = new List<Collider>();
        for (int i = 0; i < topMeshesParents[GameManager.Instance.Floor].topParent.Count; i++)
        {
            auxTop.AddRange(topMeshesParents[GameManager.Instance.Floor].topParent[i].GetComponentsInChildren<MeshRenderer>());
            auxSrTop.AddRange(topMeshesParents[GameManager.Instance.Floor].topParent[i].GetComponentsInChildren<SpriteRenderer>());
            auxCollsTop.AddRange(topMeshesParents[GameManager.Instance.Floor].topParent[i].GetComponentsInChildren<Collider>());
        }
        topMeshes = auxTop.ToArray();
        topSprites = auxSrTop.ToArray();
        topColliders = auxCollsTop.ToArray();
    }

    private int[] GetLayers(Renderer[] renderers)
    {
        List<int> layers = new List<int>();
        for (int i = 0; i < renderers.Length; i++)
        {
            layers.Add(renderers[i].gameObject.layer);
        }

        return layers.ToArray();
    }
    private Material[][] GetMaterials(MeshRenderer[] renderers)
    {
        List<Material[]> mats = new List<Material[]>();
        for (int i = 0; i < renderers.Length; i++)
        {
            mats.Add(renderers[i].materials);
        }

        return mats.ToArray();
    }
    #endregion
    #region INSIDE Transparents
    public void TransparentTop(bool active)
    {
        SetMeshesActive(!active, ref topTrans, topMeshes, topColliders, topSprites);
        flag.SetActive(!active);
    }
    public void TransparentFront(bool active)
    {
        TransparentBase(active, semiTransparentMaterialForMeshes, frontMats, ref frontTrans, frontWallMeshes, frontWallSprites);
        SetMeshesActive(!active, ref oFrontTrans, outFrontWallMeshes, outFrontColliders);
        LayerChanger(active, frontWallMeshes, frontMeshLayers);
        chimney.SetActive(!active);
    }
    public void TransparentBack(bool active)
    {
        TransparentBase(active, semiTransparentMaterialForMeshes, backMats, ref backTrans, backWallMeshes, backWallSprites);
        SetMeshesActive(!active, ref oBackTrans, outBackWallMeshes, outBackColliders);
        LayerChanger(active, backWallMeshes, backMeshLayers);
    }
    public void TransparentLeft(bool active)
    {
        TransparentBase(active, semiTransparentMaterialForMeshes, leftMats, ref leftTrans, leftWallMeshes, leftWallSprites);
        SetMeshesActive(!active, ref oLeftTrans, outLeftWallMeshes, outLeftColliders);
        LayerChanger(active, leftWallMeshes, leftMeshLayers);
    }
    public void TransparentRight(bool active)
    {
        TransparentBase(active, semiTransparentMaterialForMeshes, rightMats, ref rightTrans, rightWallMeshes, rightWallSprites);
        SetMeshesActive(!active, ref oRightTrans, outRightWallMeshes, outRightColliders);
        LayerChanger(active, rightWallMeshes, rightMeshLayers);
    }
    #endregion
    #region OUTSIDE Transparents
    public void TransparentFL(bool active)
    {
        SetMeshesActive(!active, ref oFLTrans, outFLWallMeshes, outFLColliders);
    }
    public void TransparentFLProps(bool active)
    {
        TransparentBase(active, semiTransparentMaterialForMeshes, propsFLMats, ref propsFLTrans, outFLPropsMeshes);
    }
    public void TransparentFR(bool active)
    {
        SetMeshesActive(!active, ref oFRTrans, outFRWallMeshes, outFRColliders);
    }
    public void TransparentFRProps(bool active)
    {
        TransparentBase(active, semiTransparentMaterialForMeshes, propsFRMats, ref propsFRTrans, outFRPropsMeshes);
    }
    public void TransparentBL(bool active)
    {
        SetMeshesActive(!active, ref oBLTrans, outBLWallMeshes, outBLColliders);
    }
    public void TransparentBLProps(bool active)
    {
        TransparentBase(active, semiTransparentMaterialForMeshes, propsBLMats, ref propsBLTrans, outBLPropsMeshes);
    }
    public void TransparentBR(bool active)
    {
        SetMeshesActive(!active, ref oBRTrans, outBRWallMeshes, outBRColliders);
    }
    public void TransparentBRProps(bool active)
    {
        TransparentBase(active, semiTransparentMaterialForMeshes, propsBRMats, ref propsBRTrans, outBRPropsMeshes);
    }
    #endregion

    public void ResetTransparence()
    {
        TransparentFront(false);
        TransparentBack(false);
        TransparentLeft(false);
        TransparentRight(false);
        TransparentFL(false);
        TransparentFR(false);
        TransparentBL(false);
        TransparentBR(false);
    }

    //RECOGER LAYER ORIGINAL PARA NO PREDETERMINAR A DEFAULT;
    private void LayerChanger(bool active, MeshRenderer[] meshes, int[] originalLayers)
    {
        for (int i = 0; i < meshes.Length; i++)
        {
            meshes[i].gameObject.layer = active ? 2 : originalLayers[i];
        }
    }

    private void TransparentBase(bool active, Material newMat, Material[][] originalMats, ref bool trans, MeshRenderer[] meshes, SpriteRenderer[] sprites = null)
    {
        if (active)
        {
            if (!trans)
            {
                trans = true;

                for (int i = 0; i < meshes.Length; i++)
                {
                    List<Material> transMats = new List<Material>();
                    for (int j = 0; j < meshes[i].materials.Length; j++)
                    {
                        transMats.Add(newMat);
                    }
                    meshes[i].materials = transMats.ToArray();
                }

                if (sprites != null)
                {
                    for (int i = 0; i < sprites.Length; i++)
                    {
                        sprites[i].color = new Color(sprites[i].color.r, sprites[i].color.g, sprites[i].color.b, 0.08f);
                    }
                }
            }
        }
        else
        {
            if (trans)
            {
                trans = false;

                for (int i = 0; i < meshes.Length; i++)
                {
                    meshes[i].materials = originalMats[i];
                }

                if (sprites != null)
                {
                    for (int i = 0; i < sprites.Length; i++)
                    {
                        sprites[i].color = new Color(sprites[i].color.r, sprites[i].color.g, sprites[i].color.b, 1f);
                    }
                }
            }
        }
    }

    private void SetMeshesActive(bool active, ref bool trans, MeshRenderer[] meshes, Collider[] colls)
    {
        if (!active)
        {
            if (!trans)
            {
                trans = true;

                for (int i = 0; i < meshes.Length; i++)
                {
                    meshes[i].enabled = false;
                }
                for (int i = 0; i < colls.Length; i++)
                {
                    colls[i].enabled = false;
                }
            }
        }
        else
        {
            if (trans)
            {
                trans = false;

                for (int i = 0; i < meshes.Length; i++)
                {
                    meshes[i].enabled = true;
                }
                for (int i = 0; i < colls.Length; i++)
                {
                    colls[i].enabled = true;
                }
            }
        }
    }
    private void SetMeshesActive(bool active, ref bool trans, MeshRenderer[] meshes, Collider[] colls, SpriteRenderer[] sprites)
    {
        if (!active)
        {
            if (!trans)
            {
                trans = true;

                for (int i = 0; i < meshes.Length; i++)
                {
                    meshes[i].enabled = false;
                }
                for (int i = 0; i < colls.Length; i++)
                {
                    colls[i].enabled = false;
                }
                if (sprites.Length > 0)
                    for (int i = 0; i < sprites.Length; i++)
                    {
                        sprites[i].enabled = false;
                    }
            }
        }
        else
        {
            if (trans)
            {
                trans = false;

                for (int i = 0; i < meshes.Length; i++)
                {
                    meshes[i].enabled = true;
                }
                for (int i = 0; i < colls.Length; i++)
                {
                    colls[i].enabled = true;
                }
                if (sprites.Length > 0)
                    for (int i = 0; i < sprites.Length; i++)
                    {
                        sprites[i].enabled = true;
                    }
            }
        }
    }
}
