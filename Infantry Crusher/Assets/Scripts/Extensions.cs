using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Extensions
{
    public static void Timer(this MonoBehaviour mono, float delay, Action action)
    {
        mono.StartCoroutine(Delay(delay, action));
    }

    static IEnumerator Delay(float delay, Action action)
    {
        yield return new WaitForSeconds(delay);
        action?.Invoke();
    }

    public static Bounds GetMeshRenderersBounds(List<MeshRenderer> meshRenderers)
    {
        Bounds combinedBounds = new Bounds();

        foreach (MeshRenderer renderer in meshRenderers)
        {
            combinedBounds.Encapsulate(renderer.bounds);
        }

        return combinedBounds;
    }

    public static Bounds GetMeshRenderersBounds(MeshRenderer[] meshRenderers)
    {
        Bounds combinedBounds = meshRenderers[0].bounds;

        foreach (MeshRenderer renderer in meshRenderers)
        {
            combinedBounds.Encapsulate(renderer.bounds);
        }

        return combinedBounds;
    }

    public static Bounds GetMeshRenderersLocalBounds(MeshRenderer[] meshRenderers)
    {
        Bounds combinedBounds = new Bounds();

        foreach (MeshRenderer renderer in meshRenderers)
        {
            combinedBounds.Encapsulate(renderer.localBounds);
        }

        return combinedBounds;
    }

    public static GameObject[] GetChildGameObjects(Transform parent)
    {
        GameObject[] childGOs = new GameObject[parent.childCount + 1];

        childGOs[0] = parent.gameObject;

        for(int i = 0; i < parent.childCount; i++)
        {
            childGOs[i] = parent.GetChild(i).gameObject;
        }

        return childGOs;
    }

    public static Material[] CreateMeshRendererMaterialInstances(ref MeshRenderer[] meshRenderers)
    {
        List<Material> materials = new List<Material>();

        foreach (MeshRenderer renderer in meshRenderers)
        {
            for(int i = 0; i < renderer.materials.Length; i++)
            {
                Material mat = new Material(renderer.materials[i]);
                renderer.materials[i] = mat;
                materials.Add(mat);
            }
        }

        return materials.ToArray();
    }

    public static Material[] CreateMeshRendererMaterialInstances(ref List<MeshRenderer> meshRenderers)
    {
        List<Material> materials = new List<Material>();

        foreach (MeshRenderer renderer in meshRenderers)
        {
            Material mat = new Material(renderer.material);
            renderer.material = mat;
            materials.Add(mat);
        }

        return materials.ToArray();
    }
}
