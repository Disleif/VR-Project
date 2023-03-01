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
    [SerializeField] private bool isSpeedMode;

    private int currentTargetCount = 0;
    private int currentTargetDestroyed = 0;
    private int lastTargetDestroyed = 0;
    private bool initiated = false;
    private bool finished = false;
    private float startTime;
    private float endTime;
    private GameObject currentTarget;

    void Update() {
        if (isSpeedMode) {
            if (initiated) {
                timerText.GetComponent<TextMeshPro>().text = "Cibles détruites\n" + currentTargetDestroyed + " / " + targetCount;
            }
            else if (finished) {
                timerText.GetComponent<TextMeshPro>().text = "Cibles détruites\n" + lastTargetDestroyed + " / " + targetCount;
            } else {
                timerText.GetComponent<TextMeshPro>().text = "Cibles détruites\n0 / " + targetCount;
            }
        }
        else {
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
            // Wait for .5 seconds
            yield return new WaitForSeconds(.5f);

            // Instantiate target
            currentTarget = SpawnTarget();

            // Increment target count
            currentTargetCount++;
            targetText.GetComponent<TextMeshPro>().text = "Cibles restantes\n" + (targetCount - currentTargetCount + 1);
            
            if (isSpeedMode) {
                // Wait for 1 second or  until target is destroyed from scene
                float currentTime = Time.time;
                yield return new WaitUntil(() => currentTarget == null || Time.time - currentTime >= 2);
                if (currentTarget != null) Destroy(currentTarget);
                else currentTargetDestroyed++;
            } else {
                // Wait for target to be destroyed from scene
                yield return new WaitUntil(() => currentTarget == null);
            }
        }

        initiated = false;
        finished = true;
        endTime = Time.time;
        currentTargetCount = 0;
        lastTargetDestroyed = currentTargetDestroyed;
        currentTargetDestroyed = 0;
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
        GameObject target = Instantiate(targetPrefab, randomCoords, spawnZone.rotation * Quaternion.Euler(0, 90, 0));

        return target;
    }

    public void SetTargetCount(int count) {
        if (!initiated) {
            targetCount = count;
            targetText.GetComponent<TextMeshPro>().text = "Cibles restantes\n" + count;
        }
    }

    public void EndGame() {
        if (initiated) {
            StopAllCoroutines();
            initiated = false;
            finished = false;
            endTime = Time.time;
            currentTargetCount = 0;
            currentTargetDestroyed = 0;
            targetText.GetComponent<TextMeshPro>().text = "Cibles restantes\n0";
            if (currentTarget != null) Destroy(currentTarget);
        }
    }
}
