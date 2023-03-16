using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BirdController : MonoBehaviour
{
    [field:SerializeField] public int score { get; private set; }
    [field:SerializeField] public int bestScore { get; private set; }
    [SerializeField] private float forceY = 2;
    [SerializeField] private float rotationLerp = 2;

    [Header("Components")]
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Transform rotationRoot;
    [SerializeField] private Animator anim;

    private bool allowControlls = false;
    private Vector3 initPosition;
    private int flyHashAnimator;
    public static BirdController Instance;
    private void Awake()
    {
        rb.isKinematic = true;
        allowControlls = false;
        initPosition = transform.position;
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        flyHashAnimator = Animator.StringToHash("Fly");

    }
    private void Update()
    {
        HandleAnimation();
        if (allowControlls == false)
            return;
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            rb.velocity = Vector2.up * forceY;
        }
        Rotation();
    }
    private void Rotation()
    {
        if (rb.velocity.y > 0)
        {
            rotationRoot.localEulerAngles = new Vector3(-120, -90f, 0f);
        }
        else
        {
            float t = Mathf.InverseLerp(2, 5, Mathf.Abs(rb.velocity.y));
            float targetAngle = Mathf.Lerp(-120, -50, t);
            rotationRoot.localEulerAngles = new Vector3(targetAngle, -90f, 0f);
        }
    }
    private void HandleAnimation()
    {
        anim.SetBool(flyHashAnimator, !rb.isKinematic);
    }
    private void OnValidate()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        var checkPipeHit = other.GetComponent<PipeStrcuture>();
        if (checkPipeHit)
        {
            OnDead();
        }
        var CheckPassedTrigger = other.GetComponent<PassedTrigger>();
        if (CheckPassedTrigger)
        {
            score++;
            if (score > bestScore)
            {
                bestScore = score;
                PlayerPrefs.SetInt("bestScore", bestScore);
                CanvasOnGameController.Instance.UpdateBestScore(bestScore);
            }
            CanvasOnGameController.Instance.UpdateCurrentScore(score);
        }
    }
    public void Revive()
    {
        score = 0;
        transform.position = initPosition;
        CanvasOnGameController.Instance.UpdateCurrentScore(0);
        LevelController.Instance.SetAllowUpdatePosition(true);
        LevelController.Instance.ResetPipes();
        rb.isKinematic = false;
        allowControlls = true;
    }
    private void OnDead()
    {
        LevelController.Instance.SetAllowUpdatePosition(false);
        CanvasOnGameController.Instance.deathPanel.SetDeathPanel(score, bestScore);
        rb.isKinematic = true;
        allowControlls = false;

    }
    public void SetPause(bool value)
    {
        if (value)
        {
            rb.isKinematic = true;
            allowControlls = false;
        }
        else
        {
            rb.isKinematic = false;
            allowControlls = true;
        }
    }
}
