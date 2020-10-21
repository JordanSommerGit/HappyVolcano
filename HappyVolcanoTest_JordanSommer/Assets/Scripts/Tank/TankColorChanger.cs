using UnityEngine;

public class TankColorChanger : MonoBehaviour
{

    MeshRenderer[] meshRenderer = null;

    private void Awake()
    {
        meshRenderer = GetComponentsInChildren<MeshRenderer>();
    }

    public void ChangeColor(Color color)
    {
        for (int i = 0; i < meshRenderer.Length; i++)
        {
            if (meshRenderer[i])
                meshRenderer[i].material.color = color;
        }
    }
}
