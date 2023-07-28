using System.Collections;
using System.Collections.Generic;
using Guns;
using Interfeces;
using Triggers;
using UnityEngine;
using UnityEngine.UI;

public class GroundedEnemy : MonoBehaviour, IAttack, ITakeDamage
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float health;
    [SerializeField] private int damage;
    [SerializeField] private int shotSpeed;
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject currentGun;
    [SerializeField] private GameObject aim;
    [SerializeField] private float shotDelay;
    [SerializeField] private int maxNumberOfShots;
    [SerializeField] private float speedRotation;
    [SerializeField] private GameBehavior gameBehavior;
    [SerializeField] private GroundEnemyAI groundEnemyAI;
    [SerializeField] private float resurrectionHealthValue;
    [SerializeField] private int minHealthToRun;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private Slider healthBar;
    [SerializeField] private Canvas canvas;


    internal Character character;
    internal SavingPositionTrigger SavingPositionTrigger;

    private IState.State state;
    private IGun gun;
    private Queue<GameObject> numberOfShots = new Queue<GameObject>();
    private float lastTimeShot;
    private bool coinAdded;


    void Start()
    {
        gun = currentGun.GetComponent<IGun>();
        state = IState.State.Stand;
        healthBar.maxValue = health;
        canvas.worldCamera = UnityEngine.Camera.main;
        if (UnityEngine.Camera.main != null) canvas.transform.LookAt(UnityEngine.Camera.main.transform);
        coinAdded = false;
    }
    
    
    private void HealthRecovery()
    {
        health += resurrectionHealthValue;
    }

    void Update()
    {
        healthBar.value = health;
        SetState();
        if (character != null)
        {
            Attack(character.transform.position);
            if (GetHealth() <= minHealthToRun)
            {
                groundEnemyAI.EscapeToSalvation();
            }
        }
        else
        {
            animator.SetTrigger("Move");
            gun.StopShotEffects();
            groundEnemyAI.Patrol();
        }

        if (SavingPositionTrigger != null && health <= minHealthToRun)
        {
            HealthRecovery();
        }
        
    }

    public IState.State GetState()
    {
        return state;
    }

    public float GetHealth()
    {
        return health;
    }

    public void Attack(Vector3 enemyPosition)
    {
        if (!Physics.Linecast(transform.position, enemyPosition, layerMask))
        {
            state = IState.State.Attack;
            SmoothTurnToTarget(enemyPosition, transform.position);
            Shot();
            Debug.Log("Enemy attack character");
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
        if (numberOfShots.Count == maxNumberOfShots || Time.time - lastTimeShot < shotDelay)
        {
            state = IState.State.Move;
            gun.StopShotEffects();
            return;
        }

        animator.SetTrigger("Attack");
        gun.ShotEffects();
        GameObject shot = Instantiate(bulletPrefab, gun.GetStartBulletPos().transform.position, Quaternion.identity);
        numberOfShots.Enqueue(shot);
        Bullet bullet = shot.GetComponent<Bullet>();
        shot.transform.LookAt(aim.transform.position);
        lastTimeShot = Time.time;
        bullet.setDamage(damage);
        bullet.setOwnerBullet(this);
        Vector3 heading = aim.transform.position - gun.GetStartBulletPos().transform.position;
        Rigidbody bulletRb = shot.GetComponent<Rigidbody>();
        bulletRb.AddForce(heading.normalized * shotSpeed, ForceMode.Force);
        Destroy(numberOfShots.Dequeue(), 3);
    }

    public void TakeDamage(int damageValue)
    {
        health -= damageValue;
        animator.SetTrigger("TakeDamage");
    }

    public void SetState()
    {
        if (health <= 0 && !coinAdded)
        {
            gameBehavior.AddScore(transform.position);
            animator.SetTrigger("Die");
            Destroy(gameObject, 1);
            coinAdded = true;
        }
    }
}