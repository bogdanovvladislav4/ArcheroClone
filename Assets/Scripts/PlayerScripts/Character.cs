
using System.Collections.Generic;
using Guns;
using Interfeces;
using UnityEngine;
using UnityEngine.UI;


public class Character : MonoBehaviour, ITakeDamage, IAttack
{
    [SerializeField] private Animator characterAnimator;
    [SerializeField] private int health;
    [SerializeField] private CharacterMovement characterMovement;
    [SerializeField] private GameObject equipGunHand;
    [SerializeField] private GameObject riflePrefab;
    [SerializeField] private GameObject shotgunPrefab;
    [SerializeField] private float sphereEquipGunPlace;
    [SerializeField] private float shotSpeed;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private GameObject aim;
    [SerializeField] private int damage;
    [SerializeField] private float shotDelayForRifle;
    [SerializeField] private float shotDelayShotgun;
    [SerializeField] private int maxNumberOfShots;
    [SerializeField] private float speedRotation;
    [SerializeField] private LayerMask layerMask;

    [SerializeField] private int typeOfGun;
    
    private GameObject currentEquipmentGun;
    private IState.State state;
    private IGun iGun;
    private Queue<GameObject> numberOfShots = new Queue<GameObject>();
    private float lastTimeShot;
    private int currentType;
    private float shotDelay;


    public int GetHealth()
    {
        return health;
    }

    internal GroundedEnemy groundedEnemy;
    
    private static readonly int Vertical = Animator.StringToHash("Vertical");
    private static readonly int Horizontal = Animator.StringToHash("Horizontal");
    private static readonly int State = Animator.StringToHash("State");
    private static readonly int Move = Animator.StringToHash("Move");
    private static readonly int Die = Animator.StringToHash("Die");
    private static readonly int Attack1 = Animator.StringToHash("Attack");
    private static readonly int Damage = Animator.StringToHash("TakeDamage");


    private void Start()
    {
        state = IState.State.Stand;
        GunEquipment(riflePrefab);
    }

    void Update()
    {
        Debug.Log(numberOfShots.Count);
        SetState();
        CheckEquipGun();
        if (groundedEnemy != null)
        {
            Attack(groundedEnemy.transform.position);
        }

        else
        {
            iGun.StopShotEffects();
            state = IState.State.Stand;
            transform.rotation = new Quaternion(0, 0, 0, 0);
        }
    }

    public void ChangeWeapon(int state)
    {
        typeOfGun = state;
    }
    
    public void TakeDamage(int damageValue)
    {
        health -= damageValue;
        characterAnimator.SetTrigger(Damage);
    }

    public void Attack(Vector3 enemyPosition)
    {
        if (!Physics.Linecast(transform.position, enemyPosition, layerMask))
        {
            SmoothTurnToTarget(enemyPosition, transform.position);
            Shot();
        }
    }

    private void SmoothTurnToTarget(Vector3 target, Vector3 pos)
    {
        Vector3 aimDirection = target - pos;
        Quaternion rotation = Quaternion.LookRotation(aimDirection);
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, speedRotation * Time.deltaTime);
    }

    private void Shot()
    {
        characterAnimator.SetTrigger(Attack1);
        if (numberOfShots.Count == maxNumberOfShots || Time.time - lastTimeShot < shotDelay)
        {
            state = IState.State.Stand;
            iGun.StopShotEffects();
            return;
        }

        iGun.ShotEffects();
        GameObject shot = Instantiate(bulletPrefab, iGun.GetStartBulletPos().transform.position, Quaternion.identity);
        numberOfShots.Enqueue(shot);
        lastTimeShot = Time.time;
        Bullet bullet = shot.GetComponent<Bullet>();
        var position = aim.transform.position;
        shot.transform.LookAt(position);
        bullet.setDamage(damage);
        bullet.setOwnerBullet(this);
        Vector3 heading = position - iGun.GetStartBulletPos().transform.position;
        Rigidbody bulletRb = shot.GetComponent<Rigidbody>();
        bulletRb.AddForce(heading.normalized * shotSpeed, ForceMode.Force);
        Destroy(numberOfShots.Dequeue(), 3);
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(equipGunHand.transform.position, sphereEquipGunPlace);
    }
    

    public void SetState()
    {
        if (health <= 0)
        {
            characterAnimator.SetTrigger(Die);
        }

        if (groundedEnemy != null)
        {
            state = IState.State.Attack;
        }
        if (characterMovement.isMove())
        {
            characterAnimator.SetFloat(Vertical, characterMovement.GetMove().z * 1000);
            characterAnimator.SetFloat(Horizontal, characterMovement.GetMove().x  * 1000);
            characterAnimator.SetTrigger(Move);
        }
        else
        {
            characterAnimator.SetFloat(Vertical, 0);
            characterAnimator.SetFloat(Horizontal, 0);
        }
    }

    private void CheckEquipGun()
    {
        characterAnimator.SetInteger(State, typeOfGun);

        if (typeOfGun == 0)
        {
            GunEquipment(riflePrefab);
            shotDelay = shotDelayForRifle;
        }
        else
        {
            GunEquipment(shotgunPrefab);
            shotDelay = shotDelayShotgun;
        }
    }

    private void GunEquipment(GameObject newEquipmentGun)
    {
        Destroy(currentEquipmentGun);
        currentEquipmentGun = Instantiate(newEquipmentGun, equipGunHand.transform);
        currentEquipmentGun.transform.rotation = new Quaternion(0,0,0,0);
        iGun = currentEquipmentGun.GetComponent<IGun>();
    }
}
