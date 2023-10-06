using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour
{
    public float moveSpd = 3f;
    public Collider moveArea;
    public Material allyMat;
    private Vector3 randLoc;
    private bool followingPlayer = false;
    private Transform playerTransform;

    private void Start()
    {
        SetRandLoc();
    }

    private void Update()
    {
        if (!followingPlayer)
        {
            MoveRand();
        }

        else
        {
            // follow player
            if (playerTransform != null)
            {
                transform.position = Vector3.MoveTowards(transform.position, playerTransform.position, moveSpd * Time.deltaTime);
            }
        }
    }
    
    // make the AI go everywhere it wants
    private void MoveRand()
    {
        transform.position = Vector3.MoveTowards(transform.position, randLoc, moveSpd * Time.deltaTime);

        if (Vector3.Distance(transform.position, randLoc) < 0.1f)
        {
            SetRandLoc();
        }
    }

    private void SetRandLoc()
    {
        float randX = Random.Range(moveArea.bounds.min.x, moveArea.bounds.max.x);
        float randZ = Random.Range(moveArea.bounds.min.z, moveArea.bounds.max.z);

        randLoc = new Vector3(randX, transform.position.y, randZ);
    }

    // change AI to green + store information about the transform post-contact
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Renderer renderAI = GetComponent<Renderer>();
            if (renderAI != null)
            {
                renderAI.material = allyMat;
            }

            followingPlayer = true;
            playerTransform = other.transform;
        }
    }
}