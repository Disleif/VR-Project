using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Shoot : MonoBehaviour
{
    public Transform bulletSpawn;
    public XRGrabInteractable grabbable;
    public spawnDummies dummiesScript;
    
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
