using System.Collections;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    Transform PlayerTranform;
    private SpriteRenderer CurrentSprite;
    public GunData CurrentGunData;
    private Transform firePoint;
    bool Shooting = false;
    bool AllowShoot;
    void Start()
    {
        AllowShoot = true;
        firePoint = gameObject.GetComponent<Transform>();
        CurrentSprite = GetComponent<SpriteRenderer>();
        PlayerTranform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }
    void Update()
    {
        Shooting = Input.GetButton("Fire1");
        if(Shooting && AllowShoot)
        {
            Fire();
        }
        //flip Weapon
        float Rotation;
        if(PlayerTranform.eulerAngles.z <= 180f)
        {
            Rotation = PlayerTranform.eulerAngles.z;
        }
        else
        {
            Rotation = PlayerTranform.eulerAngles.z - 360f;
        }
        if(Rotation > 0f && !(gameObject.name == "Bow"))
        {
            CurrentSprite.flipY = true;
        }
        else
        {
            CurrentSprite.flipY = false;
        }
    }
    //BulletPerShot must be more than 1 and SpreadAngle must be more than 0 to perform multishot
    void Fire()
    {
        //perform multishot
        if(CurrentGunData.SpreadAngle != 0 && CurrentGunData.BulletPerShot > 1)
        {
            float StartRotation = PlayerMovement.CurrentAngle + CurrentGunData.SpreadAngle / 2f; 
            float AngleIncrease = CurrentGunData.SpreadAngle / ((float)CurrentGunData.BulletPerShot - 1f);
            for(int i = 0; i < CurrentGunData.BulletPerShot; i++)
            {
                float TempRotation = StartRotation - AngleIncrease * i;
                GameObject Projectile = Instantiate(CurrentGunData.Bullet, firePoint.position, Quaternion.Euler(0f,0f,TempRotation));
                Projectile.GetComponent<Rigidbody2D>().AddForce(Projectile.transform.up * CurrentGunData.fireForce * Time.fixedDeltaTime, ForceMode2D.Impulse);
            }
        }
        //perform normal shot
        else
        {
            GameObject ProjectileWithoutSpread = Instantiate(CurrentGunData.Bullet, firePoint.position, firePoint.rotation);
            if(!CurrentGunData.FixShootingIssue)
            {
                ProjectileWithoutSpread.GetComponent<Rigidbody2D>().AddForce(ProjectileWithoutSpread.transform.right * CurrentGunData.fireForce * Time.fixedDeltaTime, ForceMode2D.Impulse);
            }
            if(CurrentGunData.FixShootingIssue)
            {
                ProjectileWithoutSpread.GetComponent<Rigidbody2D>().AddForce(ProjectileWithoutSpread.transform.up * CurrentGunData.fireForce * Time.fixedDeltaTime, ForceMode2D.Impulse);
            }
        }
        AllowShoot = false;
        Invoke("ResetShot", CurrentGunData.TimeBetweenShots);
    }
    void ResetShot()
    {
        AllowShoot = true;
    }
}
