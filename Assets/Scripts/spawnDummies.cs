using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnDummies : MonoBehaviour
{
    private List<GameObject> dummies = new List<GameObject>();
    [SerializeField] public GameObject target;
    [SerializeField, Range(1,5)] private int targetNumber = 3;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < targetNumber; i++)
        {
            CreateTarget();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CreateTarget() {
        // Generate random coordinates
        float x = Random.Range(-35, 35);
        float z = Random.Range(-35, 35);
        
        // Checks if there is any object in the coordinates
        while (Physics.CheckSphere(new Vector3(x, 0.5f, z), 0.4f)){
            x = Random.Range(-35, 35);
            z = Random.Range(-35, 35);
        };

        GameObject go = Instantiate(target, new Vector3(x, 0, z), Quaternion.identity);
        // Add it to the list
        dummies.Add(go);
    }

    public void then()
    {
        CreateTarget();
    }

}
