/*
 * Student number: N8589119
 * Student Name: Thomas Fleming
 *
 * File: edge.cpp
 * --------------
 * source file includes the main code for the Edge object
 * 
 *
 */
#include <sstream>
#include <iostream>

#include "edge.h"
/*  constructor with vertex source and destination and edge wieght  args */
Edge::Edge(Vertex* source, Vertex* destination, double edgeWeight){
	this->source = source;
	this->destination = destination;
	if(source==destination){
		this->edgeWeight=0;
	}else{
	this->edgeWeight = edgeWeight;
	}
}
			/*  Destructor */
Edge::~Edge()
{
}
	        /*  Accsesor for edge source */
Vertex* Edge::GetSource(){
	return source;
}
	        /*  Accsesor for edge destination */
Vertex* Edge::GetDestination(){
	return destination;
}
	        /*  Accsesor for edge wieght */
double Edge::GetWeight(){
	return edgeWeight;
}
/*
*Returns a string representation of this Vertex.
*For testing purposes.
*need to return adjanccy list
*/
string Edge::ToString(){
string NumString = static_cast<ostringstream*>( &(ostringstream() << source) )->str();
NumString+=" source ";
NumString += static_cast<ostringstream*>( &(ostringstream() << destination) )->str();
NumString+=" destination ";
NumString += static_cast<ostringstream*>( &(ostringstream() << edgeWeight) )->str();
NumString+=" edgeWeight ";
return 	NumString;
}
