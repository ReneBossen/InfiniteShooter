using System.Collections;
using Unity.Mathematics;
using UnityEngine;

public class RangedWeapon : Weapon
{
    [SerializeField] private RangedWeaponDataSO weaponData;
    private int currentAmmo;
    private bool isReloading;
    private bool shotFired;
    private float roundsPerSec;
    private float timeBetweenShots;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        shotFired = false;
        isReloading = false;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        roundsPerSec = 60f / weaponData.fireRate / 60f;
        timeBetweenShots = roundsPerSec;
        currentAmmo = weaponData.maxAmmo;
        spriteRenderer.sprite = weaponData.sprite;
        weaponData.bulletSpawnPoint = gameObject.transform.GetChild(0).transform;
    }

    private void Update()
    {
        Attack();
        if (Input.GetKeyDown(KeyCode.R))
        {
            StartCoroutine(Reload());
        }
    }
    public override void Attack()
    {
        //Weapon has ammo & is not reloading
        if (currentAmmo > 0f && !isReloading)
        {
            //Check for firerate for auto weapons
            if (timeBetweenShots >= 0f)
            {
                timeBetweenShots -= Time.deltaTime;
            }
            if (Input.GetMouseButton(0))
            {
                //Weapon is auto
                if (weaponData.isAuto)
                {
                    if (timeBetweenShots < 0f)
                    {
                        Shoot();
                        timeBetweenShots = roundsPerSec;
                    }
                }
                //Weapon is semi auto
                else if (shotFired == false)
                {
                    Shoot();
                    weaponData.shootingParticle.Play();
                }
            }
            //Reset shotFired for semi auto weapons
            if (Input.GetMouseButtonUp(0))
            {
                shotFired = false;
            }
        }
    }

    private void Shoot()
    {
        int amountOfEnemiesToHitPerShot = weaponData.pierceAmount;
        Vector2 barrelPosition = new(weaponData.bulletSpawnPoint.position.x, weaponData.bulletSpawnPoint.position.y);
        Ray2D[] directionRays = GetDirectionRaysWithBulletSpread();

        foreach (Ray2D ray in directionRays)
        {
            RaycastHit2D[] hit = Physics2D.RaycastAll(barrelPosition, ray.direction, weaponData.range, weaponData.layerMask);

            //If there's less enemies hit than the pierceAmount, set the amount to hit = amount of enemies hit
            if (hit.Length < amountOfEnemiesToHitPerShot)
            {
                amountOfEnemiesToHitPerShot = hit.Length;
            }

            //For each enemy to hit - based on pierce amount - damage enemy and spawn bullet trail
            for (int i = 0; i <= amountOfEnemiesToHitPerShot - 1f; i++)
            {
                TrailRenderer trail = Instantiate(weaponData.bulletTrail, weaponData.bulletSpawnPoint.position, Quaternion.identity);
                StartCoroutine(SpawnTrail(trail, hit[i], ray));

                if (hit[i].transform.gameObject.GetComponent<EnemyHealth>() != null)
                {
                    hit[i].transform.gameObject.GetComponent<EnemyHealth>().TakeDamage(weaponData.damage);
                }
                /*
                //Currently out of the game (KNOCKBACK)
                if (hit[i].transform.gameObject.GetComponent<Rigidbody2D>() != null)
                {
                    Debug.Log("RB is not null");
                    KnockBackEnemy(hit[i].transform.gameObject.GetComponent<Rigidbody2D>());
                }
                */
            }
        }
        currentAmmo--;
        shotFired = true;

        //Trigger event to reduce ammo on UI
        WeaponEvents.TriggerOnWeaponShoot(this);
    }

    private IEnumerator Reload()
    {
        isReloading = true;
        WeaponEvents.TriggerOnWeaponReload(this);

        yield return new WaitForSecondsRealtime(weaponData.reloadTime);

        currentAmmo = weaponData.maxAmmo;
        isReloading = false;
        shotFired = false;
    }

    private IEnumerator SpawnTrail(TrailRenderer trail, RaycastHit2D hit, Ray2D directionRay)
    {
        float time = 0f;
        Vector3 startPosition = weaponData.bulletSpawnPoint.position;
        if (hit.collider != null)
        {
            while (time < 1f)
            {
                trail.transform.position = Vector3.Lerp(startPosition, hit.point, time);
                time += Time.deltaTime / trail.time;

                yield return null;
            }
            trail.transform.position = hit.point;
            Instantiate(weaponData.impactParticle, hit.point, quaternion.identity);
        }
        else
        {
            Vector3 endPoint = directionRay.origin + directionRay.direction * weaponData.range;
            while (time < 1f)
            {
                trail.transform.position = Vector3.Lerp(startPosition, endPoint, time);
                time += Time.deltaTime / trail.time;

                yield return null;
            }
            trail.transform.position = endPoint;
        }

        Destroy(trail.gameObject, trail.time);
    }

    private Ray2D[] GetDirectionRaysWithBulletSpread()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Vector2 shootOrigin = new(weaponData.bulletSpawnPoint.position.x, weaponData.bulletSpawnPoint.position.y);
        Vector2 shootDir = (mousePos - shootOrigin).normalized;

        //Array with all rays for each shot fired
        Ray2D[] spreadRays = new Ray2D[weaponData.bulletUsePerShot];

        //Spread for each bullet
        for (int i = 0; i < spreadRays.Length; i++)
        {
            Vector2 spreadDir = Quaternion.Euler(0f, 0f, UnityEngine.Random.Range(-weaponData.bulletSpread, weaponData.bulletSpread)) * shootDir;

            //Spread for each shot
            Vector2 spreadShootDir = spreadDir.normalized; //shootDir + spread;

            //Ray2D from origin towards the new direction with spread
            Ray2D ray = new(shootOrigin, spreadShootDir.normalized);

            //Add to list
            spreadRays[i] = ray;
        }
        return spreadRays;
    }

    //Currently out of the game (KNOCKBACK)
    /*
    private void KnockBackEnemy(Rigidbody2D rb)
    {
        Debug.Log("Force Added");
        Vector2 forceDirection = rb.position - new Vector2(transform.position.x, transform.position.y);
        forceDirection.Normalize();
        rb.AddForce(forceDirection * (weaponData.knockBackForce / 10), ForceMode2D.Impulse);
    }
    */
    public int GetCurrentAmmoCount()
    {
        return currentAmmo;
    }

    public override RangedWeaponDataSO GetRangedWeaponDataSO()
    {
        return weaponData;
    }
}
