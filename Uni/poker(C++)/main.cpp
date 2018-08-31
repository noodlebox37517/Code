#include <iostream>
#include <iomanip>
#include <vector>
#include <algorithm>
#include <fstream>

#include "hand.h"
#include "card.h"
#include "deck.h"
#include "cardcomparer.h"
const int PLAYERS = 10;

using namespace std;

int main(int argc, char *argv[]) {
	    // Declare Deck and vector of pointer to Hand
	Deck deck =Deck();
	vector<Hand*>hands;
	    // create Hand for each player
	for (int i = 0; i<PLAYERS; i++){
		//cout<<"new hand "<< i+1<<endl;
		hands.push_back(new Hand(i));
	}
	

        // shuffle
		deck.Shuffle();
        // deal the cards
        for (int i = 0 ; i> CARDS_IN_HAND; i++){
				cout<<i<< endl;
	        	for(int player= 0;PLAYERS; player++){
	        		Card *nextCard = deck.DealNextCard();
	        		cout<< nextCard<<" lols "<< endl;
	        		//hands[player]->AddCard();
	        	}
	        }
     
	
	        
			
	
	cout<<"hello"<<endl;
	Card testcard2 =Card(TWO,CLUBS);
	Card testcard3 =Card(THREE,CLUBS);
	string teststring =testcard2.GetCardName();
	Card* testcard2pointer = &testcard2;
	Card* testcard3pointer = &testcard3;
	cout<<testcard3pointer<<" "<<&testcard3<<endl;
	
	cout<<teststring<< endl;
	if(CardComparer()(testcard2pointer, testcard2pointer)){
	cout<<"true";
	}else{
	cout<<"false";
	}
	
	Deck();
	
	return 0;
}
