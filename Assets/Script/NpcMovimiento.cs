using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using TMPro;

public class NPCMovement : MonoBehaviour
{
    public Transform[] movePoints;
    public float timeAtEachPoint = 2f;
    public int currentPointIndex = 0;
    public Canvas dialogCanvas;
    public TMP_Text dialogText;

    private NavMeshAgent agent;
    private bool isPaused;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        if (movePoints.Length > 0)
        {
            StartCoroutine(MoveBetweenPoints());
        }
    }

    private IEnumerator MoveBetweenPoints()
    {
        while (true)
        {
            if (!isPaused)
            {
                Transform targetPoint = movePoints[currentPointIndex];
                agent.SetDestination(targetPoint.position);


                while (!agent.pathPending && agent.remainingDistance > agent.stoppingDistance)
                {
                    yield return null;
                }


                yield return new WaitForSeconds(timeAtEachPoint);


                currentPointIndex = (currentPointIndex + 1) % movePoints.Length;
            }

            yield return null;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {

            dialogCanvas.gameObject.SetActive(true);
            dialogText.text = "Hola, ¿cómo estás?";


            isPaused = true;
            agent.isStopped = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {

            dialogCanvas.gameObject.SetActive(false);

            isPaused = false;
            agent.isStopped = false;
        }
    }
}
