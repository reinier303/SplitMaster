using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_ANDROID
using UnityStandardAssets.CrossPlatformInput;
#endif

public class PlayerShootSingle : MonoBehaviour
{
    private ObjectPooler objectPooler;
    private UIManager uIManager;

    [SerializeField]
    private float fireCooldown;
    private bool canFire;
    private bool tripleFire;
    private float spreadAngle;

    private float baseFireCooldown;

    public GameObject PickUpEffect, Muzzle;

    public Coroutine runningCoroutine;

    private void Start()
    {
        objectPooler = InstanceManager<ObjectPooler>.GetInstance("ObjectPooler");
        uIManager = UIManager.Instance;
        canFire = true;
        baseFireCooldown = fireCooldown;
    }

    // Update is called once per frame
    private void Update()
    {
#if UNITY_ANDROID
        if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
        {
            StartCoroutine(Fire());
        }
#endif
#if UNITY_STANDALONE_WIN || UNITY_EDITOR || UNITY_WEBGL
        if (Input.GetMouseButton(0) || Input.GetKey(KeyCode.Space))
        {
            StartCoroutine(Fire());
        }
#endif
    }

    private IEnumerator Fire()
    {
        if(canFire)
        {
            canFire = false;
            Muzzle.SetActive(true);
            GameObject pop = objectPooler.SpawnFromPool("BulletPop", Muzzle.transform.position, Quaternion.identity);
            pop.transform.localScale *= 0.3f;
            if(tripleFire)
            {
                for(int i = 0; i < 3; i++)
                {
                    objectPooler.SpawnFromPool("Bullet", transform.position + transform.up * 0.75f, Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, (transform.eulerAngles.z - spreadAngle + (i * spreadAngle))));
                }
            }
            else
            {
                objectPooler.SpawnFromPool("Bullet", transform.position + transform.up * 0.75f, Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z));
            }
            yield return new WaitForSeconds(fireCooldown);
            canFire = true;
        }
    }

    public IEnumerator FireRateUp(float duration, float multiplier)
    {
        if(uIManager.runningRoutine != null)
        {
            StopCoroutine(uIManager.runningRoutine);
            uIManager.runningRoutine = null;
        }

        uIManager.runningRoutine = StartCoroutine(uIManager.StartPowerUpTimer(duration, "Fire Rate Up"));

        PickUpEffect.SetActive(false);
        PickUpEffect.SetActive(true);
        fireCooldown = baseFireCooldown / multiplier;
        yield return new WaitForSeconds(duration);
        fireCooldown = baseFireCooldown;
    }

    public IEnumerator TripleFire(float duration, float spread)
    {
        if (uIManager.runningRoutine == null)
        {
            uIManager.runningRoutine = StartCoroutine(uIManager.StartPowerUpTimer(duration, "Triple Shot"));
        }
        else
        {
            StopCoroutine(uIManager.runningRoutine);
            uIManager.runningRoutine = null;
        }
        StartCoroutine(uIManager.StartPowerUpTimer(duration, "Triple Shot"));
        PickUpEffect.SetActive(false);
        PickUpEffect.SetActive(true);
        spreadAngle = spread;
        tripleFire = true;
        yield return new WaitForSeconds(duration);
        tripleFire = false;
    }
}
