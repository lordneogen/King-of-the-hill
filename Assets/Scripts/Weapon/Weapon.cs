using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

public class Weapon : MonoBehaviour
{
    private Vector3 MouseWorldPosition;
    private Player Player;
    private Renderer RendererEnable;
    [FormerlySerializedAs("shooting")] public bool IsShooting;
    [FormerlySerializedAs("objectPrefab")] public GameObject Bullet;
    [FormerlySerializedAs("bullets_per")] public int BulletsPerShut=1;
    [FormerlySerializedAs("reloadtime")] public float ReloadTime = 1f;
    [FormerlySerializedAs("num_shoot")] public int NumShoot=5;
    [FormerlySerializedAs("isshoot")] public bool Reload = true;
    private void Start()
    {
        Player = GameObject.FindObjectOfType<Player>();
        RendererEnable = GetComponent<Renderer>();
    }

    IEnumerator Shoot()
    {
        
        Reload = false;
        for (int i = 0; i < NumShoot; i++)
        {
            Vector3 ObjectPosition = transform.position;
            Vector3 MousePosition = Input.mousePosition;
            MousePosition = Camera.main.ScreenToWorldPoint(MousePosition);
            Vector3 direction = MousePosition - ObjectPosition;
            float Angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90;
            transform.rotation = Quaternion.Euler(0, 0, Angle);
            if (Input.GetMouseButton(0) && IsShooting)
            {

                GameObject BulletCenter = Instantiate(Bullet);
                BulletCenter.transform.position = transform.position;
                BulletCenter.transform.rotation = transform.rotation;


                for (int j = 0; j < BulletsPerShut; j++)
                {
                    GameObject BulletAngleLeft = Instantiate(Bullet);
                    BulletAngleLeft.transform.position = transform.position;
                    BulletAngleLeft.transform.rotation = Quaternion.Euler(0, 0, Angle + j * 3);
                }

                for (int j = 0; j < BulletsPerShut; j++)
                {
                    GameObject BulletAngleRight = Instantiate(Bullet);
                    BulletAngleRight.transform.position = transform.position;
                    BulletAngleRight.transform.rotation = Quaternion.Euler(0, 0, Angle - j * 3);
                }
            }

            yield return new WaitForSeconds(0.01f);
        }

        yield return new WaitForSeconds(ReloadTime);
        Reload = true;
    }
    void Update()
    {
        try
        {
            if (Player.SelectCeil.Item.IsGun)
            {
                Vector3 ObjectPosition = transform.position;
                Vector3 MousePosition = Input.mousePosition;
                MousePosition = Camera.main.ScreenToWorldPoint(MousePosition);
                Vector3 Direction = MousePosition - ObjectPosition;
                float Angle = Mathf.Atan2(Direction.y, Direction.x) * Mathf.Rad2Deg - 90;
                transform.rotation = Quaternion.Euler(0, 0, Angle);
                if (Reload)
                {
                    StartCoroutine(Shoot());
                }

                RendererEnable.enabled = true;
            }
            else
            {
                RendererEnable.enabled = false;
            }
        }
        catch
        {
            RendererEnable.enabled = false;
        }
    }
}
