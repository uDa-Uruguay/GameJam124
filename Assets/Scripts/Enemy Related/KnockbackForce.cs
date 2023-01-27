using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class KnockbackForce : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb2d;
    private GameObject player;

    [SerializeField] private float strength = 15;
    [SerializeField] private float delay = 0.5f;

    [SerializeField] private float timeForNextKB;

    [SerializeField] public bool canApply = true;

    public UnityEvent OnBegin, OnDone;

    public void PlayFeedback(GameObject sender)
    {
        if (canApply)
        {
            StopAllCoroutines();
            OnBegin?.Invoke();
            Vector2 direction = (transform.position - sender.transform.position).normalized;
            rb2d.AddForce(direction * strength, ForceMode2D.Impulse);
            StartCoroutine(Reset());
        }
    }

    public void PlayFeedbackFromPlayer()
    {
        if (canApply)
        {
            canApply = false;
            player = GameObject.FindGameObjectWithTag("Player");
            StopAllCoroutines();
            OnBegin?.Invoke();
            Vector2 direction = (transform.position - player.transform.position).normalized;
            rb2d.AddForce(direction * strength, ForceMode2D.Impulse);
            StartCoroutine(Reset());
            StartCoroutine(DelayTime());
        }
    }
    private IEnumerator Reset()
    {
        yield return new WaitForSeconds(delay);
        rb2d.velocity = Vector3.zero;
        OnDone?.Invoke();
    }

    private IEnumerator DelayTime()
    {
        yield return new WaitForSeconds(timeForNextKB);
        canApply = true;
    }
}
