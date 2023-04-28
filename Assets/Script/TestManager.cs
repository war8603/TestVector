using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;
using UnityEngine.UI;


public class TestManager : MonoBehaviour
{
    [SerializeReference] Image our;
    [SerializeReference] Image enemy;
    [SerializeField] Image item;
    [SerializeField] float speed = 10f;
    [SerializeField] float angle = 50f;
    [SerializeField] float angleSpeed = 0.1f;
    [SerializeField] TestSerializeReference test1;
    [SerializeField] TestSerializeReference test2;


    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log(string.Format("test1 : {0} // {1}", test1.value1, test1.value2));
        //Debug.Log(string.Format("test2 : {0} // {1}", test2.value1, test2.value2));

        var itemPos = Camera.main.ScreenToWorldPoint(item.transform.position);
        var enemyPos = Camera.main.ScreenToWorldPoint(enemy.transform.position);

        var vector2 = enemyPos - itemPos;
        float a = Mathf.Atan2(vector2.y, vector2.x) * Mathf.Rad2Deg;
        float b = Mathf.Atan2(vector2.y, vector2.x) * (float)(180 / Math.PI);
        Debug.Log(string.Format("a : {0} // b  : {1}", a, b));

        // 방향 벡터
        Vector2 direction = enemy.transform.position - item.transform.position;
        float x = direction.x * Mathf.Cos(angle) - direction.y * Mathf.Sin(angle);
        float y = direction.x * Mathf.Sin(angle) + direction.y * Mathf.Cos(angle);
        Vector2 newDir = new Vector2(x, y);
        newDir = newDir.normalized;
        itemPos = new Vector3(itemPos.x + newDir.x * Time.deltaTime * angleSpeed, itemPos.y + newDir.y * angleSpeed * Time.deltaTime, itemPos.z);
        item.transform.position = Camera.main.WorldToScreenPoint(itemPos);

    }

    // Update is called once per frame
    void Update()
    {
        var itemPos = Camera.main.ScreenToWorldPoint(item.transform.position);
        var enemyPos = Camera.main.ScreenToWorldPoint(enemy.transform.position);

        var vector2 = enemyPos - itemPos;
        float a = Mathf.Atan2(vector2.y, vector2.x) * Mathf.Rad2Deg;
        float b = Mathf.Atan2(vector2.y, vector2.x) * (float)(180 / Math.PI);
        Debug.Log(string.Format("a : {0} // b  : {1}", a, b));

        // 방향 벡터
        Vector2 direction = enemy.transform.position - item.transform.position;
        float x = direction.x * Mathf.Cos(angle) - direction.y * Mathf.Sin(angle);
        float y = direction.x * Mathf.Sin(angle) + direction.y * Mathf.Cos(angle);
        Vector2 newDir = new Vector2(x, y);
        newDir = newDir.normalized;
        itemPos = new Vector3(itemPos.x + newDir.x * Time.deltaTime * angleSpeed, itemPos.y + newDir.y * angleSpeed * Time.deltaTime, itemPos.z);
        item.transform.position = Camera.main.WorldToScreenPoint(itemPos);

        var worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        worldPos.z = 0f;
        enemy.transform.position = worldPos;
        /*
        var itemPos = Camera.main.ScreenToWorldPoint(item.transform.position);
        var enemyPos = Camera.main.ScreenToWorldPoint(enemy.transform.position);

        Vector2 dirVector2 = new Vector2(enemyPos.x - itemPos.x, enemyPos.y - itemPos.y);
        double length = Math.Sqrt(itemPos.x * enemyPos.x + itemPos.x * enemyPos.x);
        Vector2 normal = new Vector2(dirVector2.x / (float)length, dirVector2.y / (float)length);

        itemPos = new Vector3(itemPos.x + normal.x * Time.deltaTime * speed, itemPos.y + normal.y * Time.deltaTime * speed, itemPos.z);
        item.transform.position = Camera.main.WorldToScreenPoint(itemPos);

        var worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        worldPos.z = 0f;
        enemy.transform.position = worldPos;
        Debug.Log(Input.mousePosition + " /// " + worldPos);
        */
        //double radian = Util.DegreeToRadian(5f);
        //var itemRot = item.transform.rotation;
        //var itemRotEuler = itemRot.eulerAngles;
        //Debug.Log(itemRotEuler);
    }

    public void OnDrag(bool isStart)
    {

        Debug.Log(string.Format("OnDrage : {0} => {1}", isStart, Input.mousePosition));
    }

    
    
}

public class Util
{
    //벡터를 각도로 변환
    public static double VectorToDegree(Vector2 vector)
    {
        double radian = Math.Atan2(vector.y, vector.x);
        return (radian * 180.0 / Math.PI);
    }

    //벡터를 라디안으로 변환
    public static double VectorToRadian(Vector2 vector)
    {
        return Math.Atan2(vector.y, vector.x);
    }

    //라디안을 각도로 변환
    public static double RadianToDegree(double radian)
    {
        return (radian * 180.0 / Math.PI);
    }

    //각도를 라디안으로 변환
    public static double DegreeToRadian(double degree)
    {
        return (Math.PI / 180.0) * degree;
    }
}
