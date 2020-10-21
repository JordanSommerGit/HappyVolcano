using UnityEngine;

public class ColoredCube : MonoBehaviour
{
    private float hitTime = 1.0f;
    private float hitTimeCurrent = 0.0f;
    private bool IsHit { get => hitTimeCurrent >= hitTime; }

    private Color meshColor = Color.white;
    public Color MeshColor { get => meshColor; }

    private MeshRenderer meshRenderer = null;
    private CubeSpawner cubeSpawner = null;

    private void Awake()
    {
        HUDInterface hud = FindObjectOfType<HUDInterface>();
        if (hud)
            hud.OnHitTimeChanged += SetHitTime;
        hitTimeCurrent = hitTime;
        meshRenderer = GetComponent<MeshRenderer>();
        cubeSpawner = FindObjectOfType<CubeSpawner>();
        SetColor(Color.red);
    }

    private void Update()
    {
        if (hitTimeCurrent < hitTime)
        {
            hitTimeCurrent += Time.deltaTime;
            if (hitTimeCurrent >= hitTime)
            {
                meshRenderer.material.color = meshColor;
            }
        }
    }

    private void OnDisable()
    {
        hitTimeCurrent = hitTime;
    }

    public void SetColor(Color color)
    {
        meshColor = color;
        if (meshRenderer)
            meshRenderer.material.color = meshColor;
    }

    public void Hit()
    {
        cubeSpawner.NotifyColorHit(meshColor);
        TurnBlack();
    }

    public void TurnBlack()
    {
        hitTimeCurrent = 0.0f;
        meshRenderer.material.color = Color.black;
    }

    public void SetHitTime(float value)
    {
        hitTime = value;
    }
}
