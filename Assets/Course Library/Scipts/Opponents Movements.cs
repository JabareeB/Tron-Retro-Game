using System.Collections;
using UnityEngine;

public class OpponentLightCycleAI : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float turnSpeed = 90f; // Degrees per second
    [SerializeField] private float maxSpeed = 10f;
    [SerializeField] private float acceleration = 0.5f;
    [SerializeField] private float directionChangeInterval = 3f; // How often the cycle changes direction

    [Header("AI Grid Bounds")]
    [SerializeField] private Vector3 gridTopLeft = new Vector3(-39.13f, 0f, 58.57f);
    [SerializeField] private Vector3 gridBottomRight = new Vector3(39.27f, 0f, -28.67f);

    [Header("Effects")]
    [SerializeField] private GameObject destructionEffectPrefab; // Assign this in the inspector
    [SerializeField] private AudioClip speedUpSound; // Assign in the inspector
    [SerializeField] private AudioClip destructionSound; // Assign in the inspector

    private AudioSource audioSource;
    private Rigidbody rb;
    private float currentSpeed;
    private float timeSinceLastChange = 0f;
    private int direction = 1; // 1 for left turn, -1 for right turn

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    private void Start()
    {
        currentSpeed = moveSpeed;
        StartCoroutine(SmoothStartAcceleration());
    }

    private IEnumerator SmoothStartAcceleration()
    {
        audioSource.clip = speedUpSound;
        audioSource.loop = true;
        audioSource.Play();

        while (currentSpeed < maxSpeed)
        {
            currentSpeed = Mathf.Lerp(currentSpeed, maxSpeed, acceleration * Time.deltaTime);
            yield return null;
        }

        audioSource.Stop();
    }

    private void Update()
    {
        timeSinceLastChange += Time.deltaTime;
        if (timeSinceLastChange >= directionChangeInterval)
        {
            ChangeDirection();
            timeSinceLastChange = 0f;
        }

        MoveForward();
        Turn();
    }

    private void MoveForward()
    {
        Vector3 newPosition = rb.position + transform.forward * currentSpeed * Time.deltaTime;
        newPosition.x = Mathf.Clamp(newPosition.x, gridTopLeft.x, gridBottomRight.x);
        newPosition.z = Mathf.Clamp(newPosition.z, gridBottomRight.z, gridTopLeft.z);
        rb.MovePosition(newPosition);
    }

    private void Turn()
    {
        float turnAmount = turnSpeed * Time.deltaTime * direction;
        rb.MoveRotation(rb.rotation * Quaternion.Euler(0, turnAmount, 0));
    }

    private void ChangeDirection()
    {
        direction = (Random.value > 0.5f) ? 1 : -1;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("WallBarrier") ||
            collision.gameObject.CompareTag("TrailCube") ||
            collision.gameObject.CompareTag("TrailLine") ||
            collision.gameObject.CompareTag("OpponentsTrail"))
        {
            Explode();
        }
    }

    private void Explode()
    {
        audioSource.PlayOneShot(destructionSound);
        if (destructionEffectPrefab)
        {
            Instantiate(destructionEffectPrefab, transform.position, Quaternion.identity);
        }
        Destroy(gameObject);
    }
}
