using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HercDancing : MonoBehaviour
{
    Animator anim;

    //Array to store whether or not each move has been done by our character
    bool[] moveBusted;

    //Number of dance moves available to our character
    public int numMoves = 4;

    //Keep track of how many moves our character did since resetting the moves busted
    //When character has done all available moves, we will reset movesBusted array
    int countMovesBusted = 0;
    // Start is called before the first frame update
    void Start()
    {
       // Set our animator and movesBusted array to false
       anim = GetComponent<Animator>(); 
       moveBusted = new bool[numMoves];
    }

    // Update is called once per frame
    void Update()
    {
        // Add conditions to trigger animation transitions 
        if(Input.GetKeyDown(KeyCode.B))
        {
            BustRandomMove();
        }
        
    }

    void BustRandomMove()
    {
        // We will continue to pick a random integer between 0 ans number of available moves until we find
        // a move in movesBusted array that has not been busted
        bool keepPicking = true;
        while(keepPicking == true){

            //Pick a random integer move between 0 (inclusive) and number of available moves (exclusive)
            int moveIndex = Random.Range(0,numMoves);

            //Determine if randomly chosen move has already been busted
            if(moveBusted[moveIndex] == false){
                //If move has not been busted, set index and trigger
                // Change move to true in movesBusted array to show it's done
                //Increment countMovesBusted to show character has done a move
                //Exit out of while loop by setting keepPicking to false
                anim.SetInteger("MoveIndex",moveIndex);
                anim.SetTrigger("BustMove");
                moveBusted[moveIndex] = true;
                countMovesBusted += 1;
                keepPicking = false;
            }
        }
        //Check if we have busted all available moves. If so, reset the movesBusted array
        if(countMovesBusted == numMoves){
            moveBusted = new bool[numMoves];
        }
    }
}
