using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMode : MonoBehaviour
{
    [SerializeField] private Transform spawnZone;
    [SerializeField] private GameObject targetPrefab;
    public int targetCount;

    private int currentTargetCount = 0;
    private bool initiated;

    // Start is called before the first frame update
    public void StartGame()
    {
        if (!initiated) {
            StartCoroutine(Initiate());
        }
    }

    public IEnumerator Initiate() {
        initiated = true;
        while (currentTargetCount < targetCount) {
            // Instantiate target
            GameObject target = SpawnTarget();

            // Increment target count
            currentTargetCount++;
            
            // Wait for target to be destroyed from scene
            yield return new WaitUntil(() => target == null);

            // Wait for a bit
            yield return new WaitForSeconds(.5f);
        }

        Debug.Log("Game Over");
        initiated = false;
        currentTargetCount = 0;
    }

    private GameObject SpawnTarget() {
        // Get random coords inside the spawn zone
        Vector3 randomCoords = spawnZone.position + new Vector3(
            Random.Range(-spawnZone.localScale.x / 2, spawnZone.localScale.x / 2),
            Random.Range(-spawnZone.localScale.y / 2, spawnZone.localScale.y / 2),
            Random.Range(-spawnZone.localScale.z / 2, spawnZone.localScale.z / 2)
        );

        // Instantiate target
        GameObject target = Instantiate(targetPrefab, randomCoords, spawnZone.rotation);

        return target;
    }

    public void SetTargetCount(int count) {
        if (!initiated) {
            targetCount = count;
        }
    }
}
