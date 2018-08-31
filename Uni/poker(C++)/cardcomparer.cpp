/*
 * Student number: N8589119
 * Student Name: Thomas Fleming
 *
 * File: cardcomparer.cpp
 * --------------
 * source file includes the main code for the cardcomparer class
 * 
 *
 */
#include <iostream>

#include "cardcomparer.h"
#include "card.h"

//takes two cards and returns bool of whether the first is graeter then the second
bool CardComparer::operator()(Card* cardone, Card* cardtwo){
return cardone->GetRank() < cardtwo->GetRank();

}
