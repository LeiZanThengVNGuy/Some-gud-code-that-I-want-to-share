using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D rb;
    public Camera Cam;
    [SerializeField] private float MoveSpeed;
    Vector2 MoveDir;
    Vector2 MousePos;
    static public float CurrentAngle;
    void Update()
    {
        PlayerInput();
        MousePos = Cam.ScreenToWorldPoint(Input.mousePosition);
    }
    void PlayerInput()
    {
        float MoveX = Input.GetAxisRaw("Horizontal");
        float MoveY = Input.GetAxisRaw("Vertical");
        MoveDir = new Vector2(MoveX, MoveY).normalized;
    }
    void FixedUpdate() 
    {
        Move();
        Vector2 LookDir = MousePos - rb.position;
        float Angle = Mathf.Atan2(LookDir.y, LookDir.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = Angle;
        CurrentAngle = Angle;
    }
    void Move()
    {
        rb.velocity = new Vector2(MoveDir.x * MoveSpeed * Time.fixedDeltaTime, MoveDir.y * MoveSpeed * Time.fixedDeltaTime);
    }
}
