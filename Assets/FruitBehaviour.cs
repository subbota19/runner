using UnityEngine;

public class FruitBehaviour : MonoBehaviour
{
    private Vector3 initialPosition;

    public GameManager gameManager;
    public HumanBehaviour humanBehaviour;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            gameManager.FructCollected();
            humanBehaviour.UpdateBahaviour(gameObject.tag);
            Destroy(gameObject);
        }
    }

    private void Awake()
    {
        initialPosition = transform.position;
    }

    private void Update()
    {
        float y = initialPosition.y + 0.2f * Mathf.Cos(Mathf.PI * Time.time / 2f);
        transform.position = new Vector3(initialPosition.x, y, initialPosition.z);
        transform.Rotate(Vector3.up, 25.0f * Time.deltaTime);
    }
}
