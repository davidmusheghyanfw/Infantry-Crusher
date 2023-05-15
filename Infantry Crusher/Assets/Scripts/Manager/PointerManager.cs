using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointerManager : MonoBehaviour
{
    [SerializeField] Point pointPrefab;
    [SerializeField] Dictionary<Enemy, Point> _dictionary = new Dictionary<Enemy, Point>();
    [SerializeField] Transform _playerTransform;
    

    public static PointerManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    public void InitPointerManager()
    {
        // if is not empty key and value gameObject will be destroyed
        foreach (var i in _dictionary)
        {
            Destroy(i.Key.gameObject);
            Destroy(i.Value.gameObject);
        }
        _dictionary.Clear();
    }


    public void AddToList(Enemy enemy)
    {
        Point newPointer = Instantiate(pointPrefab, transform);
        _dictionary.Add(enemy, newPointer);
    }

    public void RemoveFromList(Enemy enemy)
    {


        Destroy(_dictionary[enemy].gameObject);
        _dictionary.Remove(enemy);


    }

    void LateUpdate()
    {

        // Left, Right, Down, Up
        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(CameraController.instance.Main);
        foreach (var kvp in _dictionary)
        {

            Enemy enemy = kvp.Key;
            Point pointerIcon = kvp.Value;

            Vector3 toEnemy = enemy.transform.position - _playerTransform.position;
          
            Ray ray = new Ray(_playerTransform.position, toEnemy);
           




            float rayMinDistance = Mathf.Infinity;
            //float rayMinDistance = 0;

            int index = 0;



            for (int p = 0; p < 4; p++)
            {
                if (planes[p].Raycast(ray, out float distance))
                {
                    Debug.DrawRay(_playerTransform.position, toEnemy, Color.red);

                    if (distance < rayMinDistance)
                    {
                        rayMinDistance = distance;
                        index = p;
                    }

                }
            }
            //RaycastHit hit;

            //Physics.Raycast(ray, out hit);



      
            rayMinDistance = Mathf.Clamp(rayMinDistance, 0, toEnemy.magnitude);
            Vector3 worldPosition = ray.GetPoint(rayMinDistance);
            //GameObject go = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            //go.transform.position = worldPosition;
           

            Vector3 position = CameraController.instance.Main.WorldToScreenPoint(worldPosition);

            Quaternion rotation = GetIconRotation(index);



            if (toEnemy.magnitude > rayMinDistance)
            {
                //Debug.Log("Show!!");

                pointerIcon.Show();
            }
            else
            {
                pointerIcon.Hide();
            }

            pointerIcon.SetIconPosition(position, rotation);



        }

    }

    Quaternion GetIconRotation(int planeIndex)
    {
        if (planeIndex == 0)
        {
            return Quaternion.Euler(0f, 0f, 90f);
        }
        else if (planeIndex == 1)
        {
            return Quaternion.Euler(0f, 0f, -90f);
        }
        else if (planeIndex == 2)
        {
            return Quaternion.Euler(0f, 0f, 180);
        }
        else if (planeIndex == 3)
        {
            return Quaternion.Euler(0f, 0f, 0f);
        }
        return Quaternion.identity;
    }
}
