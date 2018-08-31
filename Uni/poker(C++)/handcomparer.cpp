/*
 * Student number: N8589119
 * Student Name: Thomas Fleming
 *
 * File: handcomparer.cpp
 * --------------
 * source file includes the main code for the handcomparer class
 * 
 *
 */
#include <iostream>

#include "handcomparer.h"
#include "hand.h"

bool handcomparer::operator()(Hand* handone, Hand* handtwo){
	//takes two hands and returns bool of whether the first hand beats the second, first using the handtype and if they're the same the hand value. 
	//if they are the same it returns false simulating a draw
	 if (handone->GetHandType() > handtwo->GetHandType()){
	 	return true;
	 }else if((handone->GetHandType() == handtwo->GetHandType())){
	 if(handone->GetValue() > handtwo->GetValue()){
	 	
	 	return true;
	 }
	 }else{
	 	return false;
	 }
}
