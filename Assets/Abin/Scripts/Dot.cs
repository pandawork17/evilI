using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Dot : MonoBehaviour
{

    private Vector2 firstTouchPosition;
    private Vector2 finalTouchPosition;
    private Vector2 tempPosition;
    public float swipeAngle = 0;

    public int column;
    public int row;
    public int targetX;
    public int targetY;
    private GameObject otherDot;
    private Boards board;


    // Start is called before the first frame update
    void Start()
    {
        board = FindObjectOfType<Boards>();
        targetX = (int)transform.position.x;
        targetY = (int)transform.position.y;
        row = targetY;
        column = targetX;
    }

    // Update is called once per frame
    void Update()
    {
        targetX = column;
        targetY = row;
        if(Mathf.Abs(targetX - transform.position.x)>.1){
            //move towards the target
            tempPosition = new Vector2(targetX, transform.position.y);
            transform.position = Vector2.Lerp(transform.position, tempPosition, .4f);
        }
        else{
            //Directly set the position
            tempPosition = new Vector2(targetX,transform.position.y);
            transform.position = tempPosition;
            board.allDots[column,row] = this.gameObject;

        }
        if(Mathf.Abs(targetY - transform.position.y)>.1){
            //move towards the target
            tempPosition = new Vector2(transform.position.x,targetY);
            transform.position = Vector2.Lerp(transform.position, tempPosition, .4f);
        }
        else{
            //Directly set the position
            tempPosition = new Vector2(transform.position.x,targetY);
            transform.position = tempPosition;
            board.allDots[column,row] = this.gameObject;

        }
    }
    private void OnMouseDown(){
        firstTouchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //Debug.Log(firstTouchPosition);
    }
    private void OnMouseUp(){

        finalTouchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //Debug.Log(finalTouchPosition);
        CalculateAngle();
    }

    void CalculateAngle(){
        swipeAngle = Mathf.Atan2(finalTouchPosition.y - firstTouchPosition.y , finalTouchPosition.x - firstTouchPosition.x)*180/MathF.PI;
        //Debug.Log(swipeAngle);
        MovePieces();
    }

    void MovePieces(){
        if(swipeAngle > -45 && swipeAngle <= 45 && column <board.width){
            //Right Swipe
            otherDot = board.allDots[column+1,row];
            otherDot.GetComponent<Dot>().column -=1;
            column += 1;
        }
        else if(swipeAngle > 45 && swipeAngle <= 135 && row< board.height){
            //Up Swipe
            otherDot = board.allDots[column,row+1];
            otherDot.GetComponent<Dot>().row -=1;
            row += 1;
        }
        else if((swipeAngle > 135 || swipeAngle <= -135)&& column>0){
            //Left Swipe
            otherDot = board.allDots[column-1,row];
            otherDot.GetComponent<Dot>().column +=1;
            column -= 1;
        }
        else if(swipeAngle < -45 && swipeAngle >= -135 && row>0){
            //Down Swipe
            otherDot = board.allDots[column,row-1];
            otherDot.GetComponent<Dot>().row +=1;
            row -= 1;
        }
    }
   
}
