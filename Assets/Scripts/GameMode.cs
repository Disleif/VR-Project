using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameMode : MonoBehaviour
{
    [SerializeField] private Transform spawnZone;
    [SerializeField] private GameObject targetPrefab;
    public int targetCount;

    [SerializeField] private GameObject timerText;
    [SerializeField] private GameObject targetText;

    private int currentTargetCount = 0;
    private bool initiated = false;
    private bool finished = false;
    private float startTime;
    private float endTime;

    void Update() {
        if (initiated) {
            float time = Time.time - startTime;
            timerText.GetComponent<TextMeshPro>().text = "Temps\n" + time.ToString("F2");
        }
        else if (finished) {
            timerText.GetComponent<TextMeshPro>().text = "Temps\n" + (endTime - startTime).ToString("F2");
        }
        else {
            timerText.GetComponent<TextMeshPro>().text = "Temps\n0.00";
        }
    }

    // Start is called before the first frame update
    public void StartGame()
    {
        if (!initiated) {
            StartCoroutine(Initiate());
        }
    }

    public IEnumerator Initiate() {
        initiated = true;
        finished = false;
        startTime = Time.time;
        while (currentTargetCount < targetCount) {
            // Wait for a bit
            yield return new WaitForSeconds(.5f);

            // Instantiate target
            GameObject target = SpawnTarget();

            // Increment target count
            currentTargetCount++;
            targetText.GetComponent<TextMeshPro>().text = "Cibles restantes\n" + (targetCount - currentTargetCount + 1);
            
            // Wait for target to be destroyed from scene
            yield return new WaitUntil(() => target == null);
        }

        initiated = false;
        finished = true;
        endTime = Time.time;
        currentTargetCount = 0;
        targetText.GetComponent<TextMeshPro>().text = "Cibles restantes\n0";
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
            targetText.GetComponent<TextMeshPro>().text = "Cibles restantes\n" + count;
        }
    }
}
