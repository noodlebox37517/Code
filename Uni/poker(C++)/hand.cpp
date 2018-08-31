/*
 * Student number: N8589119
 * Student Name: Thomas Fleming
 *
 * File: hand.cpp
 * --------------
 * source file includes the main code for the hand class
 * 
 *
 */
#include <iostream>
#include <iomanip>
#include <vector>
#include <algorithm>
#include <sstream>

#include "hand.h"
#include "card.h"
#include "deck.h"
#include "cardcomparer.h"

using namespace std;



/* Constructor to create a new Hand with arg of player number */
Hand::Hand(int player ){
	playerNum = player;
}
/* Destructor - does nothing */
Hand::~Hand()
{
}

/*Adds a Card* to the Hand vector*/ 
void Hand::AddCard(Card* card){
	cards.push_back (card);

}
/*sorts hand into ascending order then evaluates hand type and value*/
void Hand::Evaluate(){
	int ascending= 0;
	int flush= 0;
	int inarow= 0;
	int twoPairs= 0;
	int firstPair ;
	int secondPair;
	//sorts hand into ascending order using cardcomparer class
	sort(cards.begin(), cards.end(), CardComparer());
	
	for (int i = 0; i <4; i++) { 
	// checks if cards ar in ascending order
	if((cards[i]->GetRank())==(cards[i+1])->GetRank()-1){
		ascending++;
	}
	//checks if cars are same suit
	if((cards[i]->GetSuit())==(cards[i+1])->GetSuit()){
		flush++;
	}
	//  checks if card is equal to next card
	if((cards[i]->GetRank())==(cards[i+1]->GetRank())){
		
		inarow = inarow+ 1 ;
		if(inarow==1){
			//counts how many times and where one pair has been found, for full house and two pair
			twoPairs+=1;
			firstPair = i;
			if(twoPairs==2){
			secondPair = i;
			}
		}
	}else{
		//resets count to zero
		inarow = 0;	
		}


 }
 //checks to see if ace can be used as one to make a straight
 if(ascending==3&&cards[4]->GetRank()==ACE){
 	if((cards[0]->GetRank()==TWO)){
 		ascending=4;
 	}
 }
 //handtype assigned based on  results above each stage trys to eleminate the most possible results and as high card is the most often presented it is first
 if(inarow>0){
 	if(inarow==1){
 		if(twoPairs==2){
 			handType = ONEPAIR;
 		}else{
 			handType = TWOPAIR;
 		}
 	}else if(inarow==2){
 		if(twoPairs==2){
 			handType = FULLHOUSE;
 		}else{
 			handType = THREEOFKIND;
 		}
 	}else{
 		handType = FOUROFKIND;
 	}
 }else if(ascending==4||flush==4){
 	if(ascending&&flush==4){
 		handType = STRAIGHTFLUSH;
 	}else if(ascending=4){
 		handType = STRAIGHT;
 	}else{
 		handType = FLUSH;
 	}	
 }else{
 	handType = HIGHCARD;
 }
 handValue = 0;
 // assigns hand value based on hand type and hand value only to be used against other same handtypes
 	switch (handType) {
		case HIGHCARD   : for(int i =0;i<5;i++){
			handValue +=(cards[i]->GetRank()*13*i);
		} ; break;
		
		case ONEPAIR : handValue =(cards[firstPair]->GetRank()*13*4*2);
		for(int i =0;i<CARDS_IN_HAND;i++){handValue +=(cards[i]->GetRank());
		}handValue -=(cards[firstPair]->GetRank()*2); break;
		
		case TWOPAIR  : handValue =(cards[firstPair]->GetRank()*13*3*2)+(cards[secondPair]->GetRank()*13*4*2); break;
		
		case THREEOFKIND  : handValue =(cards[2]->GetRank()*(13*4*3)); break;
		
		case FULLHOUSE   : handValue =(cards[2]->GetRank()*(13*4*3)); break;
		
		case FOUROFKIND : handValue =(cards[2]->GetRank()*(13*4*4)); break;
		
		case STRAIGHT : for(int i =0;i<CARDS_IN_HAND;i++){
			handValue +=(cards[i]->GetRank()*13*i);
			}; break;
			
		case FLUSH  : for(int i =0;i<CARDS_IN_HAND;i++){
			handValue +=(cards[i]->GetRank());
			}; break;
			
		case STRAIGHTFLUSH   : for(int i =0;i<CARDS_IN_HAND;i++){
			handValue +=(cards[i]->GetRank());
			}; break;
		}


	
}
/*Accsesor for the value of  hand */
int Hand::GetValue(){
	return handValue;
}
/*Accsesor for string of player number*/
string Hand::GetNumber(){
playerNumString = static_cast<ostringstream*>( &(ostringstream() << playerNum) )->str();
	return playerNumString;
}
/*Accsesor for the string value of  handType */
string Hand::GetHandTypeString(){
	string HandTypeString = "";
	switch (handType) {
		case HIGHCARD   : HandTypeString += "High Card"; break;
		case ONEPAIR : HandTypeString += "One Pair"; break;
		case TWOPAIR  : HandTypeString += "Two Pair"; break;
		case THREEOFKIND  : HandTypeString += "Three of a Kind"; break;
		case FULLHOUSE   : HandTypeString += "Fullhouse"; break;
		case FOUROFKIND : HandTypeString += "Four o a Kind"; break;
		case STRAIGHT : HandTypeString += "Straight"; break;
		case FLUSH  : HandTypeString += "Flush"; break;
		case STRAIGHTFLUSH   : HandTypeString += "Straight Flush"; break;
	}
	return 	HandTypeString;
}
/*Accsesor for the type of hand */
HandType Hand::GetHandType(){
	return handType;
}
/* creates a string representation of hand including handtype and a summary of its card objects*/
string Hand::ToString(){
	string handstring =" - ";
	for (int i = 0; i <5; i++){
		handstring = handstring + (cards.at(i)->GetCardName())+" ";
	}
	handstring+= " -  ";
		return handstring;
		
	
	
}
