using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Shoot : MonoBehaviour
{
    public Transform bulletSpawn;
    public XRGrabInteractable grabbable;
    public spawnDummies dummiesScript;
    public GameObject muzzleFlash;
    public Transform muzzleFlashSpawn;
    public GameObject projectile;

    void Start()
    {
        grabbable.activated.AddListener(RaycastShoot);
    }

    void RaycastShoot(ActivateEventArgs args)
    {
        Debug.Log("Shoot !");

        // Raycast
        RaycastHit hit;
        if (Physics.Raycast(bulletSpawn.position, bulletSpawn.forward, out hit))
        {
            // Check if we hit something
            GameObject target = hit.collider.gameObject.transform.parent.gameObject;
            if (target != null)
            {
                Debug.Log("Hit !");
                // Destroy the object if it has tag Enemy
                if (target.transform.CompareTag("Enemy"))
                {
                    Destroy(target);
                    dummiesScript.then(); //Act according to kill
                }
            }
        }

        // Play the muzzle flash as child of the muzzle flash spawn
        GameObject flash = Instantiate(muzzleFlash, muzzleFlashSpawn.transform.position, muzzleFlashSpawn.transform.rotation);
        flash.transform.parent = muzzleFlashSpawn.transform;
        flash.transform.localScale = new Vector3(0.02f, 0.02f, 0.02f);
        // Rotate the muzzle flash to point forward
        flash.transform.Rotate(Random.Range(0, 360), -90, 0);
        // Translate the muzzle flash forward
        flash.transform.Translate(0.13f, 0, 0f);

        // Destroy the muzzle flash after 0.5 seconds
        Destroy(flash, 0.1f);

        // Quickly create and translate the projectile from the muzzle flash spawn to the hit point
        GameObject projectileObject = Instantiate(projectile, muzzleFlashSpawn.transform.position, Camera.main.transform.rotation);
        projectileObject.transform.Rotate(0, 0, 90);
        // Give the projectile a rigidbody
        Rigidbody projectileRigidbody = projectileObject.AddComponent<Rigidbody>();
        projectileRigidbody.useGravity = false;

        // Add force to the projectile
        projectileRigidbody.AddForce(bulletSpawn.forward * 4000);
        Destroy(projectileObject, 0.2f);
    }

    // Draw a ray in the Scene view
    /* void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(bulletSpawn.position, bulletSpawn.forward * 100);
    } */

    void Update()
    {
        // Draw a line renderer
        LineRenderer lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.SetPosition(0, bulletSpawn.position);
        lineRenderer.SetPosition(1, bulletSpawn.position + bulletSpawn.forward * 100);

        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            RaycastShoot(new ActivateEventArgs());
        }
    }
}
