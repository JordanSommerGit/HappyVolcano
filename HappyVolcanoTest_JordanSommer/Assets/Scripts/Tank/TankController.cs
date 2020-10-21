using UnityEngine;
using UnityEngine.InputSystem;

public class TankController : MonoBehaviour
{
    [SerializeField]
    private GameObject barrel = null;
    [SerializeField]
    private GameObject bulletSocket = null;
    [SerializeField]
    private GameObject bulletTamplate = null;
    [SerializeField]
    private float aimSpeed = 10.0f;

    private Vector2 aimDispostion = Vector2.zero;
    private TankColorChanger tankColorChanger = null;

    private void Awake()
    {
        tankColorChanger = GetComponent<TankColorChanger>();
    }

    private void Update()
    {
        Vector3 rotationVector = new Vector3(-aimDispostion.y * Time.deltaTime * aimSpeed, aimDispostion.x * Time.deltaTime * aimSpeed, 0);
        barrel.transform.Rotate(rotationVector);
    }

    public void OnShoot(InputAction.CallbackContext callbackContext)
    {
        if (!callbackContext.performed)
            return;
        SpawnProjectile(bulletSocket.transform.position, barrel.transform.rotation);
    }
    public void OnAim(InputAction.CallbackContext callbackContext)
    {
        aimDispostion = callbackContext.ReadValue<Vector2>();
    }

    private void SpawnProjectile(Vector3 position, Quaternion rotation)
    {
        if (!bulletTamplate)
            return;
        GameObject spawnedBullet = Instantiate(bulletTamplate);
        spawnedBullet.transform.position = position;
        spawnedBullet.transform.rotation = rotation;
        spawnedBullet.GetComponent<TankProjectile>()?.Init(tankColorChanger);
    }
}
