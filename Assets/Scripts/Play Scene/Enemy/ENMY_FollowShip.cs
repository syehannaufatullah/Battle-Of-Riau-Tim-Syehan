// using UnityEngine;

// public class ENMY_FollowShip : MonoBehaviour
// {
//     // Objek yang akan diikuti (drag & drop di Inspector)
//     public Transform target;

//     // Kecepatan mengikuti
//     public float followSpeed = 5f;

//     // Kecepatan rotasi
//     public float rotationSpeed = 10f;

//     // Jarak offset di belakang target
//     public float followDistance = 2f;

//     // Tinggi offset (opsional)
//     public float heightOffset = 1f;

//     void Update()
//     {
//         if (target != null)
//         {
//             // Hitung posisi di belakang target
//             Vector3 targetPosition = target.position - target.forward * followDistance + Vector3.up * heightOffset;

//             // Gerakan halus ke posisi target
//             transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);

//             // Rotasi menghadap target
//             Quaternion targetRotation = Quaternion.LookRotation(target.forward);
//             transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
//         }
//     }
// }
using UnityEngine;

public class ENMY_FollowShip : MonoBehaviour
{
    public Transform target;

    public float followSpeed = 5f;
    public float rotationSpeed = 10f;
    public float followDistance = 2f;
    public float heightOffset = 1f;

    public GameObject Barrel;
    public GameObject[] Explosions;
    public GameObject SmokeEffectPrefab;

    private AudioSource source;
    private bool hasExploded = false;
    private bool isWaitingToExplode = false;

    public float explosionDelay = 3f; // Delay ledakan dalam detik

    void Awake()
    {
        Barrel.SetActive(true);
        foreach (var explosion in Explosions)
        {
            explosion.SetActive(false);
        }

        source = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (target != null)
        {
            Vector3 targetPosition = target.position - target.forward * followDistance + Vector3.up * heightOffset;
            transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);

            Quaternion targetRotation = Quaternion.LookRotation(target.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
        else if (!hasExploded && !isWaitingToExplode)
        {
            isWaitingToExplode = true;
            Invoke(nameof(Explode), explosionDelay); // Delay sebelum ledakan
        }
    }

    void Explode()
    {
        hasExploded = true;

        if (Barrel != null) Barrel.SetActive(false);

        foreach (var explosion in Explosions)
        {
            if (explosion != null) explosion.SetActive(true);
        }

        if (source != null) source.Play();

        if (SmokeEffectPrefab != null)
        {
            Instantiate(SmokeEffectPrefab, transform.position, Quaternion.identity);
        }

        Invoke(nameof(DestroyBoom), 2f); // Tunggu 2 detik sebelum destroy
    }

    void DestroyBoom()
    {
        Destroy(gameObject);
    }
}
