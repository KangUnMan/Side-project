using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Queue : MonoBehaviour
{
    int MAX;
    int front;    //머리 쪽에 위치할 index값, pop할때 참조하는 index
    int rear;    //꼬리 쪽에 위치할 index값, push할때 참조하는 index
    String[] queue;
    public Queue(int size)
    {
        front = rear = 0;    //초기값 0
        queue = new string[size]; //배열 생성
        MAX = size;
    }

    public bool isEmpty()
    { //queue에 아무것도 들어있지 않은지 판단하는 함수
        return front == rear;
    }
    public bool isFull()
    {    //queue가 가득 차 공간이 없는지 판단하는 함수
        if (rear == MAX - 1)
        {
            return true;
        }
        else
            return false;
    }
    public int size()
    { //queue에 현재 들어가 있는 데이터의 개수를 return
        return front - rear;
    }
    public void push(string value)
    {
        if (isFull())
        {
            Debug.Log("Queue is Full");
            return;
        }
        queue[rear++] = value; //rear가 위치한 곳에 값을 넣어주고 rear를 증가시킨다.
    }
    public string pop()
    {
        if (isEmpty())
        {
            Debug.Log("Queue is Empty");
            return "Queue is Empty";
        }
        string popValue = queue[front++];
        return popValue;
    }
    public string peek()
    {
        if (isEmpty())
        {
            Debug.Log("Queue is Empty");
            return "Queue is Empty";
        }
        string popValue = queue[front];
        return popValue;
    }
}
