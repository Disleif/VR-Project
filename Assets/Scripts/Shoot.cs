using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Shoot : MonoBehaviour
{
    public Transform bulletSpawn;
    
    void Start()
    {
        XRGrabInteractable grabbable = GetComponent<XRGrabInteractable>();
        grabbable.activated.AddListener(RaycastShoot);
    }

    void RaycastShoot(ActivateEventArgs args)
    {
        Debug.Log("Shoot !");

        // Raycast
        RaycastHit hit;
        if (Physics.Raycast(bulletSpawn.position, bulletSpawn.forward, out hit))
        {
            // Check if we hit a cube
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
}
