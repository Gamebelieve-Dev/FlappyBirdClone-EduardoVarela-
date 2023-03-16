using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class VisualController : MonoBehaviour
{
    [SerializeField] private List<Transform> visualController = new List<Transform>();
    [SerializeField] private float movePipesSpeed;
    [SerializeField] private Camera cam;
    [Header("Reposition")]
    [SerializeField] private float offsetReposition = 2;
    private int pipesCount;

    private bool allowUpdatePosition = false;
    private void Awake()
    {       
        pipesCount = visualController.Count;
    }

    private void Update()
    {
        if (allowUpdatePosition == false)
            return;
        for (int i = 0; i < pipesCount; i++)
        {
            var currentPipe = visualController[i];
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
   
    private void Resposicion(Transform tr)
    {
        visualController.Remove(tr);
        Vector3 newPos = visualController.Last().position;
        newPos += new Vector3(offsetReposition, 0, 0);
        newPos.z = 10;
        newPos.y = tr.transform.position.y;
        tr.transform.position = newPos;
        visualController.Add(tr);
    }
    private bool IsOutsideScreen(Transform tr)
    {
        var offsetPosition = tr.position + new Vector3(offsetReposition, 0, 0);
        Vector3 viewportPos = cam.WorldToViewportPoint(offsetPosition);
        if (viewportPos.x < 0)
        {
            return true;
        }
        return false;
    }
}
