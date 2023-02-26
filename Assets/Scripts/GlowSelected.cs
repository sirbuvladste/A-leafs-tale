using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlowSelected : MonoBehaviour
{
    Dictionary<Renderer, Material[]> dictGlowMaterial = new Dictionary<Renderer, Material[]>();
    Dictionary<Renderer, Material[]> dictOriginalMaterial = new Dictionary<Renderer, Material[]>();

    private bool isGlowing = false;

    public Material glowMaterial;

    //change the material of the object to the glow material when it is a neighbour of the selected hex
    // also glows all the material of the prefab
    //change the material of the object to the original material when it is no longer a neighbour of the selected hex

    public void Glow()
    {
        if (!isGlowing)
        {
            isGlowing = true;
            Renderer[] renderers = GetComponentsInChildren<Renderer>();
            foreach (Renderer renderer in renderers)
            {
                Material[] originalMaterials = renderer.materials;
                Material[] glowMaterials = new Material[originalMaterials.Length];
                for (int i = 0; i < originalMaterials.Length; i++)
                {
                    glowMaterials[i] = glowMaterial;
                }
                dictOriginalMaterial.Add(renderer, originalMaterials);
                dictGlowMaterial.Add(renderer, glowMaterials);
                renderer.materials = glowMaterials;
            }
        }
    }

    public void UnGlow()
    {
        if (isGlowing)
        {
            isGlowing = false;
            Renderer[] renderers = GetComponentsInChildren<Renderer>();
            foreach (Renderer renderer in renderers)
            {
                renderer.materials = dictOriginalMaterial[renderer];
            }
            dictGlowMaterial.Clear();
            dictOriginalMaterial.Clear();
        }
    }

    public bool IsGlowing()
    {
        return isGlowing;
    }


    
}
