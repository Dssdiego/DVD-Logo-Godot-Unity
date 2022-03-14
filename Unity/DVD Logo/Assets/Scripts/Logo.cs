using UnityEngine;

public class Logo : MonoBehaviour
{
    public Color[] colors = {Color.blue, Color.green, Color.magenta, Color.cyan, Color.red};
    public float initialSpeed = 0.05f;
    public float speed = 0;
    public bool canMove = false;
    public Vector3 direction = Vector3.zero;

    //
    // Unity Methods
    //
    
    void Start()
    {
        StartMoving();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartMoving();
        }

        if (Input.GetKeyDown(KeyCode.Plus) || Input.GetKeyDown(KeyCode.KeypadPlus) || Input.GetKeyDown(KeyCode.Equals))
        {
            IncreaseSpeed();
        }
        
        if (Input.GetKeyDown(KeyCode.Minus) || Input.GetKeyDown(KeyCode.KeypadMinus))
        {
            DecreaseSpeed();
        }

        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            ResetSpeed();
        }

        if (canMove)
        {
            CalcScreenBorderCollision();
        }
    }

    void FixedUpdate()
    {
        transform.position += direction.normalized * speed * Time.deltaTime;
    }
    
    //
    // Game Logic
    //

    void StartMoving()
    {
        PositionLogoAtCenter();
        ChangeColor();
        speed = initialSpeed;
        canMove = true;
        direction = new Vector3(1,1,0);
    }
    
    void CalcScreenBorderCollision()
    {
        Vector2 screenSize = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        
        var collisionRight = transform.position.x * 1.05f;
        var collisionLeft = transform.position.x * 1.05f;
        var collisionDown = transform.position.y * 1.05f;
        var collisionUp = transform.position.y * 1.05f;

        if (collisionRight > screenSize.x)
        {
            direction = ReflectXDirection(direction);
            ChangeColor();
        }

        if (collisionLeft < -screenSize.x)
        {
            direction = ReflectXDirection(direction);
            ChangeColor();
        }

        if (collisionDown < -screenSize.y)
        {
            direction = ReflectYDirection(direction);
            ChangeColor();
        }
        
        if (collisionUp > screenSize.y)
        {
            direction = ReflectYDirection(direction);
            ChangeColor();
        }
    }

    Vector3 ReflectXDirection(Vector3 dir)
    {
        dir.x *= -1;
        return dir;
    }

    Vector3 ReflectYDirection(Vector3 dir)
    {
        dir.y *= -1;
        return dir;
    }

    void ChangeColor()
    {
        Color color = colors[Random.Range(0, colors.Length)];
        GetComponent<SpriteRenderer>().color = color;
    }

    void RandomizeColor()
    {
        
    }

    void PositionLogoAtCenter()
    {
        Vector2 halfScreen = Camera.main.ScreenToWorldPoint(new Vector2((float) Screen.width/2, (float) Screen.height/2));
        transform.position = new Vector3(halfScreen.x, halfScreen.y);
    }

    void IncreaseSpeed()
    {
        speed += 1f;
    }

    void DecreaseSpeed()
    {
        speed -= 1f;
    }

    void ResetSpeed()
    {
        speed = initialSpeed;
    }
}
