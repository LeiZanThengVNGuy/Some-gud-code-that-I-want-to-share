using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D rb;
    public Camera Cam;
    [SerializeField] private float MoveSpeed;
    Vector2 MoveDir;
    Vector2 MousePos;
    [SerializeField] float MaxHP = 100f;
    float HP;
    static public float CurrentAngle;
    private void Start() {
        HP = MaxHP;
    }
    void Update()
    {
        PlayerInput();
        MousePos = Cam.ScreenToWorldPoint(Input.mousePosition);
        //kill player
        if(HP <= 0)
        {
            Destroy(gameObject);
        }
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
    public void GetHurt(float Damage)
    {
        HP -= Damage;
    }
}
