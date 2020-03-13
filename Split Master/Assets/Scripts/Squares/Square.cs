using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Square : MonoBehaviour
{
    //General
    [SerializeField]
    private float rotationSpeed;
    [SerializeField]
    private float speed;
    [SerializeField]
    private float boundsSize;

    private float speedX;
    private float speedY;

    private List<Vector2> Directions = new List<Vector2>();
    public bool check;

    Collider2D collider2D;

    //Script References
    ObjectPooler objectPooler;
    GameManager gameManager;
    PowerUpManager powerUpManager;

    //Difficulty related
    public float SplitAmount;
    public float SplitCount;

    //Checks
    [SerializeField]
    private bool firstSquare;
    private bool movingStarted;
    private bool checkRunning;

    //ParticleEffectColors
    public Vector4 newColor;
    public Color32 newColor2;

    //PowerUps
    public float powerUpChance;

    //ScreenShake
    CameraShake shakeScript;

    private void Start()
    {
        gameManager = GameManager.Instance;
        objectPooler = InstanceManager<ObjectPooler>.GetInstance("ObjectPooler");
        powerUpManager = PowerUpManager.Instance;
        shakeScript = gameManager.transform.GetComponent<CameraShake>();
        collider2D = GetComponent<Collider2D>();
        AddDirections();
        if(firstSquare)
        {
            RandomDirection();
        }
        AdjustParticleSystemSize();
        movingStarted = false;
        checkRunning = false;
        StartCoroutine(WaitToMove());
    }
    // Update is called once per frame
    private void Update()
    {
        Rotate();
        if(movingStarted)
        {
            Move();
        }
        CheckBounds();
    }

    private void AdjustParticleSystemSize()
    {
        ParticleSystem particleSystem = transform.GetChild(0).GetComponent<ParticleSystem>();
        var particleSystemShape = particleSystem.shape;
        particleSystemShape.scale = transform.localScale * 2.5f;
        var particleSystemMain = particleSystem.main;
        particleSystemMain.startSize = new ParticleSystem.MinMaxCurve(transform.localScale.x / 2, transform.localScale.x);
    }

    private void AddDirections()
    {
        Directions.Add(new Vector2(speed, speed));
        Directions.Add(new Vector2(-speed, speed));
        Directions.Add(new Vector2(speed, -speed));
        Directions.Add(new Vector2(-speed, -speed));
    }

    private IEnumerator WaitToMove()
    {
        if(firstSquare)
        {
            yield return new WaitForSeconds(1f);
        }
        movingStarted = true;
    }

    void RandomDirection()
    {
        Vector2 currentSpeed = Directions[Random.Range(0,Directions.Count)];
        speedX = currentSpeed.x;
        speedY = currentSpeed.y;
    }

    private void Rotate()
    {
        transform.Rotate(new Vector3(0, 0, rotationSpeed));
    }

    private void Move()
    {
        if (transform.position.x > 25 - (transform.localScale.x) || transform.position.x < -25 + (transform.localScale.x))
        {
            speedX *= -1;
        }
        if (transform.position.y > 25 - (transform.localScale.x ) || transform.position.y < -25 + (transform.localScale.x))
        {
            speedY *= -1;
        }
        transform.position += new Vector3(speedX * Time.deltaTime, speedY * Time.deltaTime);
    }

    private void CheckBounds()
    {
        if (transform.position.x > 25 || transform.position.x < -25 || transform.position.y > 25 || transform.position.y < -25)
        {
            if(!checkRunning)
            {
                StartCoroutine(PushBack());
            }
        }
    }

    private IEnumerator PushBack()
    {
        checkRunning = true;
        yield return new WaitForSeconds(1f);
        if (transform.position.x > 25)
        {
            transform.position = new Vector2(transform.position.x - 1, transform.position.y);
        }
        if (transform.position.x < -25)
        {
            transform.position = new Vector2(transform.position.x + 1, transform.position.y);
            print("Pushed");

        }
        if (transform.position.y > 25)
        {
            transform.position = new Vector2(transform.position.x, transform.position.y - 1);
            print("Pushed");

        }
        if (transform.position.y < -25)
        {
            transform.position = new Vector2(transform.position.x, transform.position.y + 1);
        }
        checkRunning = false;
    }

    public void Die()
    {
        //PartiicleEffect
        GameObject particleObject =  objectPooler.SpawnFromPool("Explosion", transform.position, Quaternion.identity);
        particleObject.transform.localScale = transform.localScale * 1.25f;
        Material particleSystemMaterial = particleObject.GetComponent<ParticleSystem>().GetComponent<ParticleSystemRenderer>().material;
        particleSystemMaterial.SetColor("_Color", newColor2);
        particleSystemMaterial.SetVector("_EmissionColor", newColor * 0.017f);

        //Screenshake
        shakeScript.StartCoroutine(shakeScript.Shake(0.8f * transform.localScale.x, 3 * transform.localScale.x));

        //PowerUp
        powerUpManager.SpawnPowerUp(powerUpChance, transform.position);

        gameManager.AddScore();
        if(SplitCount > 0)
        {
            SpawnCubes();
        }
        gameManager.SquaresAlive--;
        gameObject.SetActive(false);
    }

    private void SpawnCubes()
    {
        Debug.Log("Spawned");
        for(int i = 0; i < SplitAmount; i++)
        {
            gameManager.SquaresAlive++;
            Square square = objectPooler.SpawnFromPool("Squares", transform.position, Quaternion.identity).GetComponent<Square>();
            square.firstSquare = false;
            Vector2 direction = GetRandomDirection();
            square.speedX = direction.x;
            square.speedY = direction.y;
            square.SplitCount = SplitCount-1;
            square.SplitAmount = SplitAmount;
            square.transform.localScale = new Vector2(transform.localScale.x / 1.35f, transform.localScale.y / 1.35f);
        }
        AddDirections();
    }

    private Vector2 GetRandomDirection()
    {
        int index = Random.Range(0, Directions.Count);
        Vector2 direction = Directions[index];
        Directions.RemoveAt(index);
        return direction;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        collider2D.enabled = false;
        Bullet bullet = collision.GetComponent<Bullet>();
        if (bullet != null)
        {
            Die();
            collision.gameObject.SetActive(false);
        }
        collider2D.enabled = true;
    }
}
