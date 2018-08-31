/*
 * Student number: N8589119
 * Student Name: Thomas Fleming
 *
 * File: PokerEval.cpp
 * --------------
 * source file includes the manin code for execution using the class objects
 * 
 *
 */
#include <iostream>
#include <fstream>
#include <iomanip>
#include <vector>
#include <algorithm>

#include "deck.h"
#include "card.h"
#include "hand.h"
#include "cardcomparer.h"
#include "handcomparer.h"
//#include "handcomparer.h"

using namespace std;

const int PLAYERS = 10;
const int CARDS_PER_PLAYER = 5;
vector<Hand*>hands;

int main(int argc, char *argv[]) {

    // Declare Deck and vector of pointer to Hand
	Deck deck;
    // create Hand for each player
	for (int i = 0; i<PLAYERS; i++){
		hands.push_back(new Hand(i+1));
	}
    // allow for testing from file
    if (argc == 2) {

        // open the file and check it exists
        ifstream infile;
        infile.open(argv[1]);
        if (infile.fail()) {
            cerr <<  "Error: Could not find file" << endl;
            return 1;
        }

        // read the cards into the hands
        int rank, suit;
        for (int card = 0; card < CARDS_PER_PLAYER; card++) {
            for (int i = 0; i < PLAYERS; i++) {
                infile >> rank >> suit;
                Card *card = new Card((Rank)rank, (Suit)suit);
                (hands[i])->AddCard(card);
            }
        }

        // close the file
        infile.close();
    } else {
	      // shuffle
	      deck.Shuffle();
        // deal the cards
		    for(int player= 0;player<PLAYERS; player++){
			for (int i = 0 ; i < CARDS_PER_PLAYER; i++){
	        (hands[player])->AddCard(deck.DealNextCard());   
			}
		}
    }  
    // evaluate the hands
for(int player= 0;player<PLAYERS; player++){
	hands[player]->Evaluate();
}
    // sort the hands
sort(hands.begin(), hands.end(), handcomparer());
    // output the hands
    for(int player= 0;player<PLAYERS; player++){
	cout<<"player"<<setw(3)<<hands[player]->GetNumber()<<(hands[player]->ToString())<<hands[player]->GetHandTypeString() <<endl;
}

    return 0;
}
