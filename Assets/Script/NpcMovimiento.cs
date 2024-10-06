using System.Collections;
using UnityEngine;

using TMPro;

public class NPCMovement : MonoBehaviour
{
    public Transform[] movePoints;
    public float timeAtEachPoint = 2f;
    public int currentPointIndex = 0;
    public Canvas dialogCanvas;
    public TMP_Text dialogText;

    private void Start()
    {
        if (movePoints.Length > 0)
        {
            StartCoroutine(MoveBetweenPoints());
        }
    }

    private IEnumerator MoveBetweenPoints()
    {
        while (true)
        {
            Transform targetPoint = movePoints[currentPointIndex];
            yield return StartCoroutine(MoveTo(targetPoint.position));

          
            yield return new WaitForSeconds(timeAtEachPoint);

           
            currentPointIndex = (currentPointIndex + 1) % movePoints.Length;
        }
    }

    private IEnumerator MoveTo(Vector3 targetPosition)
    {
        float journeyLength = Vector3.Distance(transform.position, targetPosition);
        float startTime = Time.time;

        while (transform.position != targetPosition)
        {
            float distCovered = (Time.time - startTime) * 1f;
            float fracJourney = distCovered / journeyLength;
            transform.position = Vector3.Lerp(transform.position, targetPosition, fracJourney);
            yield return null;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
          
            dialogCanvas.gameObject.SetActive(true);
            dialogText.text = "Hola, ¿cómo estás?";

            StopAllCoroutines();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
           
            dialogCanvas.gameObject.SetActive(false);

          
            StartCoroutine(MoveBetweenPoints());
        }
    }
}
