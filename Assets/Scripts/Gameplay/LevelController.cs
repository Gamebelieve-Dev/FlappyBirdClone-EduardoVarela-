using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    [SerializeField] private VisualController skyVisual;
    [SerializeField] private VisualController buildingsVisual;
    [SerializeField] private PipeStructureController pipesController;

    public static LevelController Instance;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ResetPipes()
    {
        pipesController.InitialReposition();
    }

    public void SetAllowUpdatePosition(bool value)
    {
        skyVisual.SetAllowUpdatePosition(value);
        buildingsVisual.SetAllowUpdatePosition(value);
        pipesController.SetAllowUpdatePosition(value);
    }
}
