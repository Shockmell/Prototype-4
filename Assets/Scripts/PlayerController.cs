using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 30.0f;
    public GameObject indicator;

    private Rigidbody playerRb;
    private GameObject focalPoint;
    private bool hasPower = false;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        focalPoint = GameObject.Find("Focal Point");
    }

    // Update is called once per frame
    void Update()
    {
        float forwardInput = Input.GetAxis("Vertical");
        playerRb.AddForce(forwardInput * speed * focalPoint.transform.forward);

        float horizontalInput = Input.GetAxis("Horizontal");
        playerRb.AddForce(horizontalInput * speed * focalPoint.transform.right);

        indicator.transform.position = transform.position - new Vector3(0, 0.75f, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Power Up")
        {
            hasPower = true;
            Destroy(other.gameObject);
            StartCoroutine(CountdownRoutine());
            indicator.gameObject.SetActive(true);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Enemy" && hasPower)
        {
            Rigidbody enemyRb = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 away = (collision.gameObject.transform.position - transform.position).normalized;

            enemyRb.AddForce(away * 15.0f, ForceMode.Impulse);
        }
    }

    IEnumerator CountdownRoutine()
    {
        yield return new WaitForSeconds(7);
        hasPower = false;
        indicator.gameObject.SetActive(false);
    }
}
