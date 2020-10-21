using UnityEngine;

public class TankProjectile : MonoBehaviour
{
    [SerializeField]
    private float speed = 50.0f;

    [SerializeField]
    private float maxLifeTime = 3.0f;
    private float currentLifeTime = 0.0f;

    private TankColorChanger colorChanger = null;
    private Rigidbody rigid = null;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
    }

    public void Init(TankColorChanger colorChanger)
    {
        this.colorChanger = colorChanger;
    }

    private void Update()
    {
        currentLifeTime += Time.deltaTime;
        if (currentLifeTime >= maxLifeTime)
            Destroy(gameObject);

        if (rigid)
            rigid.velocity = transform.forward * speed;
    }

    private void OnCollisionEnter(Collision collision)
    {
        ColoredCube hitCube = collision.gameObject.GetComponent<ColoredCube>();
        if (hitCube)
        {
            hitCube.Hit();
            if (colorChanger)
                colorChanger.ChangeColor(hitCube.MeshColor);
            Destroy(gameObject);
        }
    }
}
