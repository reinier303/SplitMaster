using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_ANDROID
using UnityStandardAssets.CrossPlatformInput;
#endif

public class PlayerShootSingle : MonoBehaviour
{
    ObjectPooler objectPooler;

    [SerializeField]
    private float fireCooldown;
    private bool canFire;
    private bool tripleFire;
    private float spreadAngle;


    private float baseFireCooldown;

    private void Start()
    {
        objectPooler = InstanceManager<ObjectPooler>.GetInstance("ObjectPooler");
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
        fireCooldown = baseFireCooldown / multiplier;
        yield return new WaitForSeconds(duration);
        fireCooldown = baseFireCooldown;
    }

    public IEnumerator TripleFire(float duration, float spread)
    {
        spreadAngle = spread;
        tripleFire = true;
        yield return new WaitForSeconds(duration);
        tripleFire = false;
    }
}
