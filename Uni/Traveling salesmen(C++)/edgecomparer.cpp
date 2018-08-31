/*
 * Student number: N8589119
 * Student Name: Thomas Fleming
 *
 * File: edgecomparer.cpp
 * --------------
 * source file includes the main code for the edgecomparer class
 * 
 *
 */
#include "Edge.h"
#include "edgecomparer.h"
	        /*  comparer for edge class */
	bool EdgeComparer::operator()(Edge* edgeOne, Edge* edgeTwo){
		if((edgeOne->GetWeight())<edgeTwo->GetWeight()){
			return true;
		}else{
			return false;
		}
	}
