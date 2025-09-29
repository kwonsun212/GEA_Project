using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public enum WeaponType { Light, Heavy }

    public GameObject projectilePrefab;     //projectile 프리펩펩

    public Transform firePoint;             //발사 위치 (총구)

    Camera cam;

    public WeaponType currentWeapon = WeaponType.Light;

    public float lightSpeed = 20f;

    public int lightDamage = 1;

    public float heavySpeed = 12f;

    public int heavyDamage = 3;
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main; //메인 카메라 가져오기 
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Z))
        {
            currentWeapon = (currentWeapon == WeaponType.Light) ? WeaponType.Heavy : WeaponType.Light;
        }
        if(Input.GetMouseButtonDown(0)) //좌클릭 발사
        {
            Shoot();
        }
    }
    void Shoot()
    {
        //화면에서 마우스 광선 쏘기
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        Vector3 targetPoint;
        targetPoint = ray.GetPoint(50f);
        Vector3 direction = (targetPoint - firePoint.position).normalized; //방향 벡터

        float speed = (currentWeapon == WeaponType.Light) ? lightSpeed : heavySpeed;
        int damage = (currentWeapon == WeaponType.Light) ? lightDamage : heavyDamage;


        //Projectile 생성
        GameObject proj = Instantiate(projectilePrefab, firePoint.position, Quaternion.LookRotation(direction));
        Projectile projectile = proj.GetComponent<Projectile>();
        if (projectile != null)
        {
            projectile.Init(direction, speed, damage);
        }
    }
}
