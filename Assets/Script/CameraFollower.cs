using System.Collections;
using UnityEngine;

public class CameraFollower : Singleton<CameraFollower>
{
    public Transform TF;
    [SerializeField] Vector3 offset;
    [SerializeField] private Transform weaponTF;
    [SerializeField] private Transform playerTF;
    private void LateUpdate()
    {
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        worldPosition.z = 0;
        Vector3 direction = worldPosition - playerTF.position;
        float distance = Mathf.Sqrt(Mathf.Pow(direction.x, 2) + Mathf.Pow(direction.y, 2));
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.Euler(0, 0, angle);
        if (rotation.eulerAngles.z > 90 && rotation.eulerAngles.z < 270)
        {
            weaponTF.localScale = new Vector3(1, -1, 1);
        }
        else
        {
            weaponTF.localScale = Vector3.one;
        }
        weaponTF.rotation = rotation;
        if (distance < 2.4f)
        {
            TF.position = Vector3.Lerp(TF.position, worldPosition + offset, Time.deltaTime * 0.6f);
        }
        if (worldPosition.x < playerTF.position.x)
        {
            playerTF.rotation = Quaternion.Euler(0, 180, 0);
        }
        else
        {
            playerTF.rotation = Quaternion.Euler(0, 0, 0);
        }
    }
    public void CameraShake(float x, float y)
    {
        StartCoroutine(Shake(x, y));
    }
    IEnumerator Shake(float x, float y)
    {
        Vector3 prevpos = TF.position;
        TF.position = Vector3.Lerp(TF.position, TF.position + new Vector3(x, y), 1);
        yield return new WaitForSeconds(0.1f);
        TF.position = Vector3.Lerp(TF.position, prevpos, 0.2f);
    }

}
