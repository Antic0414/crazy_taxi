using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Driver : MonoBehaviour
{
    [SerializeField] float moveSpeed = 1f;
    [SerializeField] float rotationSpeed = 120f;

    [SerializeField] GameObject personGO;
    [SerializeField] GameObject finishGO;
    [SerializeField] GameObject speedGO;

    [SerializeField] SpriteRenderer occupiedTriangleSpriteRenderer;


    float xLeft = -2.5f, xRight = 9.5f, yUp = 4f, yDown = -2.5f;
    bool shouldSlowDown = false;
    bool shouldNitro = false;

    float decreaseSpeedStartTime = 5;
    float decreaseSpeedCurrentTime = 0;
    float nitroStartTime = 3;
    float nitroCurrentTime = 0;

    const string SPEED_TAG = "Speed";
    const string PERSON_TAG = "Person";
    const string FINISH_TAG = "Finish";

    // Start is called before the first frame update
    void Start()
    {
        occupiedTriangleSpriteRenderer.color = Color.green;
        // Application.targetFrameRate = 60;
        // Generisanje prve osobe
        instanciraj(PERSON_TAG);
        instanciraj(SPEED_TAG);
    }

    // Update is called once per frame
    void Update()
    {
        float currentSpeed = moveSpeed;

        if (shouldSlowDown)
        {
            currentSpeed /= 2;
            decreaseSpeedCurrentTime -= Time.deltaTime;
            if (decreaseSpeedCurrentTime <= 0)
            {
                shouldSlowDown = false;
            }
        }

        if (shouldNitro)
        {
            currentSpeed *= 2;
            nitroCurrentTime -= Time.deltaTime;
            if (nitroCurrentTime <= 0)
            {
                shouldNitro = false;
            }
        }

        float rotationAxis = Input.GetAxis("Horizontal");
        float moveAxis = Input.GetAxis("Vertical");
        if (moveAxis < 0)
        {
            rotationAxis *= -1; // invertovanje rotacije kada se krecemo u rikverc
        }
        transform.Translate(0, -1 * currentSpeed * moveAxis * Time.deltaTime, 0);
        transform.Rotate(0, 0, -1 * rotationAxis * rotationSpeed * Time.deltaTime);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        shouldSlowDown = true;
        decreaseSpeedCurrentTime = decreaseSpeedStartTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(SPEED_TAG))
        {
            Destroy(collision.gameObject);
            shouldNitro = true;
            nitroCurrentTime = nitroStartTime;
            instanciraj(SPEED_TAG);
        }
        else if (collision.gameObject.CompareTag(PERSON_TAG))
        {
            // 1. ukloni osobu sa scene
            Destroy(collision.gameObject);
            occupiedTriangleSpriteRenderer.color = new Color(255,220,0f);
            // 2. kreiramo cilj -> gde auto treba da odvede osobu
            instanciraj(FINISH_TAG); // gameobject koji kreiramo, Vector3 pozicija, rotacija
         }
        else if (collision.gameObject.CompareTag(FINISH_TAG))
        {
            // 1. ukloni finish
            Destroy(collision.gameObject);
            occupiedTriangleSpriteRenderer.color = Color.green;
            // 2. kreiramo novu osobu
            instanciraj(PERSON_TAG);
        }
    }

    private GameObject instanciraj(string tag)
    {
        float x = Random.Range(xLeft, xRight);
        float y = Random.Range(yDown, yUp);
        switch (tag)
        {
            case "Person":
                return Instantiate(personGO, new Vector3(x, y), Quaternion.identity);
            case "Finish":
                return Instantiate(finishGO, new Vector3(x, y), Quaternion.identity);
            case "Speed":
                return Instantiate(speedGO, new Vector3(x, y), Quaternion.identity);
        }
        return null;
    }
}
