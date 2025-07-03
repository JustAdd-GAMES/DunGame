using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerShooting : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject crosshairPrefab;
    
    [Header("Settings")]
    [SerializeField] private float crosshairDistance = 1.5f;
    [SerializeField] private float fireCooldown = 0.2f;
    
    private Camera mainCamera;
    private GameObject crosshair;
    private float nextFireTime;
    private InventoryManager inventoryManager;

    private void Start()
    {
        mainCamera = Camera.main;
        CreateCrosshair();
    //   Cursor.visible = false;
        inventoryManager = FindFirstObjectByType<InventoryManager>();
    }

    private void Update()
    {
        UpdateCrosshairPosition();

        // Block shooting if interacting with inventory or UI
        if ((inventoryManager != null && inventoryManager.IsMovingItem) ||
            (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject()))
            return;

        // Only allow shooting if not interacting with inventory/UI
        if (Input.GetButton("Fire1") && Time.time >= nextFireTime)
        {
            ShootProjectile();
            nextFireTime = Time.time + fireCooldown;
        }
    }

    private void CreateCrosshair()
    {
        crosshair = Instantiate(crosshairPrefab, Vector3.zero, Quaternion.identity);
    }

    private void UpdateCrosshairPosition()
    {
        Vector3 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;
        
        Vector3 shootingDirection = (mousePosition - transform.position).normalized;
        crosshair.transform.position = transform.position + shootingDirection * crosshairDistance;
    }

    private void ShootProjectile()
    {
        Vector2 fireDirection = (crosshair.transform.position - transform.position).normalized;
        Instantiate(projectilePrefab, crosshair.transform.position, Quaternion.identity)
            .GetComponent<Projectile>().Initialize(fireDirection);
    }

    private void OnDisable()
    {
        if (crosshair) crosshair.SetActive(false);
        Cursor.visible = true;
    }

    private void OnEnable()
    {
        if (crosshair) crosshair.SetActive(true);
        Cursor.visible = true;
    }

    public bool IsShootingEnabled()
    {
        return inventoryManager == null || !inventoryManager.IsMovingItem;
    }
}