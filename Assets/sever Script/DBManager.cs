using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using UnityEngine;

public class DBManager : MonoBehaviour  // 사용시 필히 DB테이블 조회하고 보면서 사용할것
{
    string UID = "test";    // 테스트용 id 미리 설정
    string PW = "1234";     // 테스트용 비번 미리 설정
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log(RegisterUID(UID, PW));
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            Debug.Log(LoginCheck(UID));
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            Debug.Log(getScore(UID));
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            Debug.Log(UpdateScore(UID, "5"));
        }
    }

    public static string LoginCheck(string UID)
    {
        string password = "1234";       // 로그인 창에서 password 받아오기
        string url = $"https://projectside.azurewebsites.net/hello-servlet?action=loginCheck&UID={UID}";
        string responseText = string.Empty;
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
        request.Method = "GET";
        request.Timeout = 30 * 1000;
        request.Headers.Add("Authorization", "BASIC SGVsbG8=");

        using (HttpWebResponse resp = (HttpWebResponse)request.GetResponse())
        {
            HttpStatusCode status = resp.StatusCode;
            Console.WriteLine(status);  // 정상이면 "OK"
            Debug.Log(status);
            Stream respStream = resp.GetResponseStream();
            using (StreamReader sr = new StreamReader(respStream))
            {
                responseText = sr.ReadToEnd();
            }
        }
        Debug.Log(responseText);
        if (responseText == password)
            return "Login Success";
        else
            return "Login Failed "+responseText;
    }

    public static string getNickname(string UID)
    {
        string url = $"https://projectside.azurewebsites.net/hello-servlet?action=getNickname&UID={UID}";
        string responseText = string.Empty;
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
        request.Method = "GET";
        request.Timeout = 30 * 1000;
        request.Headers.Add("Authorization", "BASIC SGVsbG8=");

        using (HttpWebResponse resp = (HttpWebResponse)request.GetResponse())
        {
            HttpStatusCode status = resp.StatusCode;
            Console.WriteLine(status);  // 정상이면 "OK"
            Debug.Log(status);
            Stream respStream = resp.GetResponseStream();
            using (StreamReader sr = new StreamReader(respStream))
            {
                responseText = sr.ReadToEnd();
            }
        }
        return responseText;


    }

    public static string getScore(string UID)
    {
        string url = $"https://projectside.azurewebsites.net/hello-servlet?action=getScore&UID={UID}";
        string responseText = string.Empty;
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
        request.Method = "GET";
        request.Timeout = 30 * 1000;
        request.Headers.Add("Authorization", "BASIC SGVsbG8=");

        using (HttpWebResponse resp = (HttpWebResponse)request.GetResponse())
        {
            HttpStatusCode status = resp.StatusCode;
            Console.WriteLine(status);  // 정상이면 "OK"
            Debug.Log(status);
            Stream respStream = resp.GetResponseStream();
            using (StreamReader sr = new StreamReader(respStream))
            {
                responseText = sr.ReadToEnd();
                Debug.Log(responseText);
            }
        }
        return responseText;

    }
    public static string UpdateScore(string UID, string score)
    {
        string url = $"https://projectside.azurewebsites.net/hello-servlet?action=updateScore&UID={UID}&score={score}";
        string responseText = string.Empty;
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
        request.Method = "GET";
        request.Timeout = 30 * 1000;
        request.Headers.Add("Authorization", "BASIC SGVsbG8=");

        using (HttpWebResponse resp = (HttpWebResponse)request.GetResponse())
        {
            HttpStatusCode status = resp.StatusCode;
            Console.WriteLine(status);  // 정상이면 "OK"
            Debug.Log(status);
            Stream respStream = resp.GetResponseStream();
            using (StreamReader sr = new StreamReader(respStream))
            {
                responseText = sr.ReadToEnd();
                Debug.Log(responseText);
            }
        }
        return responseText;
    }
    public static string RegisterUID(string UID, string password)
    {
        string url = $"https://projectside.azurewebsites.net/hello-servlet?action=registerID&UID={UID}&password={password}";
      //  string url = $"http://localhost:8081/demo_war_exploded/hello-servlet?action=registerID&UID={UID}&password={password}";
        string responseText = string.Empty;
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
        request.Method = "GET";
        request.Timeout = 30 * 1000;
        request.Headers.Add("Authorization", "BASIC SGVsbG8=");

        using (HttpWebResponse resp = (HttpWebResponse)request.GetResponse())
        {
            HttpStatusCode status = resp.StatusCode;
            Console.WriteLine(status);  // 정상이면 "OK"
            Debug.Log(status);
            Stream respStream = resp.GetResponseStream();
            using (StreamReader sr = new StreamReader(respStream))
            {
                responseText = sr.ReadToEnd();
                Debug.Log(responseText);
            }
        }
        return responseText;
    }


}
