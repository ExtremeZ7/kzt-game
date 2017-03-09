using UnityEngine;
using AssemblyCSharp;

public class SpawnWindParticles : MonoBehaviour
{

    public GameObject windParticlePrefab;
    public Transform spawnPoint;

    [Space(10)]
    public float particlesPerSecond;
    public float randomExtraDelay;

    [Space(10)]
    public float particleSpeed;
    public float randomExtraParticleSpeed;

    private float timer;
    private float delay;

    void Start()
    {
        if (spawnPoint == null)
            spawnPoint = transform;

        delay = 1f / particlesPerSecond;

        ResetTimer();
    }

    void Update()
    {
        if (Help.UseAsTimer(ref timer))
        {
            GameObject newParticle = Instantiate(windParticlePrefab, spawnPoint.position, Quaternion.identity) as GameObject;
            Rigidbody2D rb2d = newParticle.GetComponent<Rigidbody2D>();
            rb2d.velocity = Trigo.GetRotatedVector(transform.rotation.eulerAngles.z + 90f, particleSpeed + Random.Range(0f, randomExtraParticleSpeed));

            ResetTimer();
        }
    }

    void ResetTimer()
    {
        timer = delay + Random.Range(0f, randomExtraDelay);
    }
}
