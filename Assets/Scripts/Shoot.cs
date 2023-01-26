using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Shoot : MonoBehaviour
{
    public Transform bulletSpawn;
    public XRGrabInteractable grabbable;
    
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
            GameObject target = hit.collider.gameObject;
            if (target != null)
            {
                Debug.Log("Hit !");
                // Destroy the object if it is a prefab named Dummie
                if (target.name == "Dummie")
                {
                    Destroy(target);
                }
            }
        }
    }

    // Draw a ray in the Scene view
    /* void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(bulletSpawn.position, bulletSpawn.forward * 100);
    } */

    void Update() {
        // Draw a line renderer
        LineRenderer lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.SetPosition(0, bulletSpawn.position);
        lineRenderer.SetPosition(1, bulletSpawn.position + bulletSpawn.forward * 100);
    }
}
