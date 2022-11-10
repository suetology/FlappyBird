using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pipe
{
    private Transform pipeHead;
    private Transform pipeBody;

    public Pipe(Transform head, Transform body)
    {
        pipeBody = body;
        pipeHead = head;
    }
    public void MovePipe()
    {
        pipeHead.position -= new Vector3(1, 0, 0) * GameManager.gameSpeed * Time.deltaTime;
        pipeBody.position -= new Vector3(1, 0, 0) * GameManager.gameSpeed * Time.deltaTime;
    }
    public float GetPipeXpos() => pipeHead.position.x;
    public void DestroySelf()
    {
        MonoBehaviour.Destroy(pipeHead.gameObject);
        MonoBehaviour.Destroy(pipeBody.gameObject);
    }
}
