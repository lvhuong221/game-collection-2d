using ScriptableObjectArchitecture;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

[RequireComponent(typeof(STM_CharacterAudio))]
public class STM_PlayerController : MonoBehaviour
{
    [SerializeField] private ShootToMoveInputReader _inputReader;
    [SerializeField] Rigidbody2D _body2D;
    [SerializeField] GameObjectVariable playerObject;
    [SerializeField] IntVariable ammoCountVar;

    [SerializeField] STM_PlayerStatsSO _playerStatsSO;
    [SerializeField] AssetReferenceGameObject bulletAsset;
    [SerializeField] Transform bulletSpawnPos;

    [Header("Child component")]
    [SerializeField] ParticleSystem deathParticle;
    [SerializeField] GameObject playerVisual;

    private STM_CharacterAudio _characterAudio;

    private List<STM_Bullet> poolBullet = new List<STM_Bullet>();
    private STM_Bullet bulletPrefab;
    private Vector2 itinialPos;

    private bool _canShoot = true;
    private bool _isDead = false;

    private void Awake()
    {
        _characterAudio = GetComponent<STM_CharacterAudio>();
        bulletAsset.LoadAssetAsync().Completed += LoadBulletAssetCompleted; ;
    }

    private void LoadBulletAssetCompleted(AsyncOperationHandle<GameObject> obj)
    {
        if(obj.Status == AsyncOperationStatus.Succeeded)
        {
            bulletPrefab = obj.Result.GetComponent<STM_Bullet>();
        }
    }

    private void Start()
    {
        SaveInitialState();
        playerObject.Value = gameObject;
    }

    private void OnEnable()
    {
        _inputReader.shootEvent += OnShootEvent;
        _inputReader.resetEvent += Reset;
    }

    private void OnDisable()
    {
        _inputReader.shootEvent -= OnShootEvent;
        _inputReader.resetEvent -= Reset;
    }

    private void Update()
    {

    }

    private void OnShootEvent()
    {
        if (_isDead || _canShoot == false || _playerStatsSO.currentAmmoCount <= 0)
        {
            return;
        }
        Vector2 shootDirection = -(Vector2)(Camera.main.ScreenToWorldPoint(Input.mousePosition + new Vector3(0f, 0f, 1f)) - transform.position);
        //Debug.DrawLine(Camera.main.ScreenToWorldPoint(Input.mousePosition + new Vector3(0f, 0f, 1f)), transform.position, Color.black, 20);

        _body2D.AddForce(shootDirection.normalized * _playerStatsSO.jumpForce, ForceMode2D.Impulse);
        _characterAudio.PlayShoot();
        _playerStatsSO.currentAmmoCount--;
        _playerStatsSO.Raise();
        
        STM_Bullet bullet = GetBulletFromPool();
        bullet.transform.position = bulletSpawnPos.position;
        bullet.transform.up = -shootDirection;
        bullet.SetDirection(-shootDirection);

        StartCoroutine("ShootCooldown");
    }

    private STM_Bullet GetBulletFromPool()
    {
        if (bulletPrefab == null)
            return null;

        STM_Bullet result = null;

        foreach(var bullet in poolBullet)
        {
            if(bullet.isActiveAndEnabled == false)
            {
                result = bullet;
                break;
            }
        }

        // no bullet or no inactive bullet
        if (result == null)
        {
            result = Instantiate(bulletPrefab);
            poolBullet.Add(result);
        }
        result.gameObject.SetActive(true);
        return result;
    }

    private IEnumerator ShootCooldown()
    {
        _canShoot = false;
        yield return new WaitForSecondsRealtime(_playerStatsSO.shootCooldown);
        _canShoot = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<GoalController>() != null)
        {
            Debug.Log("Player hit goal");
            _body2D.isKinematic = true;
            _body2D.velocity = Vector2.zero;
            _characterAudio.PlayHitGoal();
        } else if (collision.GetComponent<DeathZoneTrigger2D>())
        {
            OnDeath();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        _characterAudio.PlayLand();
    }

    private void SaveInitialState()
    {
        itinialPos = transform.position;
        _playerStatsSO.currentAmmoCount = ammoCountVar.Value;
        _playerStatsSO.Raise();
    }

    public void Reset()
    {
        transform.position = itinialPos;
        _playerStatsSO.currentAmmoCount = ammoCountVar.Value;
        _playerStatsSO.Raise();
        _body2D.velocity = Vector2.zero;

        playerVisual.SetActive(true);
        deathParticle.Stop();
        _isDead = false;
    }

    private void OnDeath()
    {
        _isDead = true;
        deathParticle.Play();
        _characterAudio.PlayDie();
        playerVisual.SetActive(false);

        _body2D.velocity = Vector2.zero;

        Invoke("Reset", 1.5f);
    }
}
