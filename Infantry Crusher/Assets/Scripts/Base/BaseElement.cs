using System.Collections;
using System.Collections.Generic;
using System.Linq;
using AmazingAssets.AdvancedDissolve;
using UnityEngine;

public class BaseElement : MonoBehaviour
{
    [SerializeField] GameObject filledObject;
    [Range(0f, 1f)] [SerializeField] float progress = 0f;
    public float Progress{get{return progress;}}
    [SerializeField] Transform fillMask;

    [SerializeField] Transform filledContainer;
    [SerializeField] Transform emptyContainer;

    [SerializeField] AdvancedDissolveGeometricCutoutController filledGeometricCutoutController;
    [SerializeField] AdvancedDissolveGeometricCutoutController emptyGeometricCutoutController;
    [SerializeField] AdvancedDissolvePropertiesController propertiesController;
    [SerializeField] AdvancedDissolveKeywordsController keywordsController;
    [SerializeField] Material emptyMaterial;

    MeshRenderer[] filledMeshRenderers;
    Material[] filledMaterials;
    Vector2 progressRange;
    Bounds meshBounds;
    Bounds localMeshBounds;

    MeshRenderer[] emptyMeshRenderers;
    Material[] emptyMaterials;

    GameObject emptyObject;


    private void Start() {
        Initalize();
    }
    public void Initalize()
    {
        emptyObject = Instantiate(filledObject, emptyContainer);

        filledMeshRenderers = filledObject.GetComponentsInChildren<MeshRenderer>();
        emptyMeshRenderers = emptyObject.GetComponentsInChildren<MeshRenderer>();

        for(int i = 0; i < emptyMeshRenderers.Length; i++)
        {
            emptyMaterials = emptyMeshRenderers[i].materials;

            for (int j = 0; j < emptyMaterials.Length; j++)
            {
                emptyMaterials[j] = emptyMaterial;
            }

            emptyMeshRenderers[i].materials = emptyMaterials;
        }

        Extensions.CreateMeshRendererMaterialInstances(ref filledMeshRenderers);
        Extensions.CreateMeshRendererMaterialInstances(ref emptyMeshRenderers);

        meshBounds = Extensions.GetMeshRenderersBounds(filledMeshRenderers);
        localMeshBounds = Extensions.GetMeshRenderersLocalBounds(filledMeshRenderers);

        GameObject[] filledGameObjects = Extensions.GetChildGameObjects(filledObject.transform);
        GameObject[] emptyGameObjects = Extensions.GetChildGameObjects(emptyObject.transform);

        keywordsController.AddMaterialsFromSelection(filledGameObjects.Concat(emptyGameObjects).ToArray());
        propertiesController.AddMaterialsFromSelection(filledGameObjects.Concat(emptyGameObjects).ToArray());

        filledGeometricCutoutController.AddMaterialsFromSelection(filledGameObjects);
        emptyGeometricCutoutController.AddMaterialsFromSelection(emptyGameObjects);

    }


    public void SetFillProgress(float _progress, float time)
    {
        
        progress = _progress;
        print(progress);
        fillMask.transform.localPosition = transform.position - Vector3.up * Mathf.Lerp(progressRange.x, progressRange.y, progress);
    }
}
