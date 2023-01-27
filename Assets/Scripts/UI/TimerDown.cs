using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimerDown : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timerText;
    private EnemySpawner spawnerData;

    private float counter;

    private void Start()
    {
        spawnerData = this.gameObject.GetComponent<EnemySpawner>();
        newTimer();
    }

    private void FixedUpdate()
    {
        if (spawnerData.stopSpawning) 
        { 
            timerText.text = "......";
            return;
        }

        string newTime = $"New wave: {counter.ToString("0")}";
        timerText.text = newTime;
        counter = counter - 1 * Time.deltaTime;

        if (counter > 0) return;
        else StartCoroutine(refreshTime());
     }

    private void newTimer()
    {
        if (spawnerData) counter = spawnerData.currentwave.TimeSpawning;
    }

    private IEnumerator refreshTime()
    {
        yield return new WaitForSeconds(0.05f);
        newTimer();
    }
}
