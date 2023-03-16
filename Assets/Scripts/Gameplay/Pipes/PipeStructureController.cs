using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PipeStructureController : MonoBehaviour
{
    [SerializeField] private List<Transform> pipies = new List<Transform>();
    [SerializeField] private float movePipesSpeed;
    [SerializeField] private Camera cam;
    [Header("Reposition")]
    [SerializeField] private float offsetReposition = 2;
    [SerializeField] private float minY = -2;
    [SerializeField] private float maxY = 2;
    private int pipesCount;
  
    private bool allowUpdatePosition = false;
    private void Awake()
    {
       
        pipesCount = pipies.Count;
        InitialReposition();
    } 

    private void Update()
    {
        if (allowUpdatePosition == false)
            return;
        for (int i = 0; i < pipesCount; i++)
        {
            var currentPipe = pipies[i];
            currentPipe.position -= new Vector3(movePipesSpeed * Time.deltaTime, 0, 0);
            if (IsOutsideScreen(currentPipe))
            {
                Resposicion(currentPipe);
            }
        }
    }
    public void SetAllowUpdatePosition(bool value)
    {
        allowUpdatePosition = value;
    }
    public void InitialReposition()
    {
        Vector3 position = cam.ViewportToWorldPoint(new Vector3(1f, 0.5f,0)) + offsetReposition * cam.transform.right;
        position.y = Random.Range(minY, maxY);
        position.z = 0;
        for (int i = 0; i < pipesCount; i++)
        {
            pipies[i].transform.position = position;
            position += new Vector3(offsetReposition,0,0);
            position.y = Random.Range(minY, maxY);
            position.z = 0;
        }
    }
    private void Resposicion(Transform tr)
    {
        pipies.Remove(tr);
        Vector3 newPos = pipies.Last().position;
        newPos += new Vector3(offsetReposition, 0, 0);
        newPos.y = Random.Range(minY, maxY);
        newPos.z = 0;
        tr.transform.position = newPos;
        pipies.Add(tr);
    }
    private bool IsOutsideScreen(Transform tr)
    {
        var offsetPosition = tr.position + new Vector3(1, 0, 0);
        Vector3 viewportPos = cam.WorldToViewportPoint(offsetPosition) ;
        if (viewportPos.x < 0)
        {
            return true;
        }
        return false;
    }
}
