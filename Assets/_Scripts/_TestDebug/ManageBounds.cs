using UnityEngine;

public class ManageBounds : MonoBehaviour
{
    private MeshRenderer[] meshes;
    private Collider[] colliders;

    private bool prevDisableCollides;
    private bool prevDisableMeshes;

    [SerializeField] private bool disableColliders;
    [SerializeField] private bool disableMeshes;

    private void OnEnable()
    {
        // NON FUNGE OnEnable
         
        Debug.Log("Script riattivato");


        prevDisableCollides = disableColliders;
        prevDisableMeshes = disableMeshes;

        PopulateArrayis();
    }

    private void OnValidate()
    {
        if(meshes == null || colliders == null || meshes.Length <= 0 ||  colliders.Length <= 0)
        {
            PopulateArrayis();
        }

        // Da rimuovere in caso di fix alla chiamata OnEnable
        PopulateArrayis();

        if(prevDisableCollides != disableColliders)
        {
            for (int i = 0; i < colliders.Length; i++)
            {
                colliders[i].enabled = !disableColliders;
            }

            prevDisableCollides = disableColliders;
        }
        else if(prevDisableMeshes != disableMeshes)
        {
            for (int i = 0; i < meshes.Length; i++)
            {
                meshes[i].enabled = !disableMeshes;
            }

            prevDisableMeshes = disableMeshes;
        }
    }

    private void PopulateArrayis()
    {
        colliders = GetComponentsInChildren<Collider>();
        meshes = GetComponentsInChildren<MeshRenderer>();
    }
}
