/*
 * Student number: N8589119
 * Student Name: Thomas Fleming
 *
 * File: vertex.cpp
 * --------------
 * source file includes the main code for the Vertex object
 * 
 *
 */
#include <sstream>
#include <iostream>
#include "vertex.h"
/*  constructor with point identifier arg */
Vertex::Vertex(int identifier){
	this->identifier = identifier;
}
/*  Destructor */
Vertex::~Vertex(){
}

int Vertex::GetId(){
	return identifier;
}
/*
*Adds a pointer to a Vertex to this Vertex’
*adjacency list
*/
void Vertex::AddAdjacency(Vertex* adjacentVertex){
	vertexAdjacencies.push_back(adjacentVertex);
}
/*
*Returns a collection of pointer of Vertex,
*being the vertices adjacent to this Vertex
*check how it returns vector, may need halpz
*/
vector<Vertex*> Vertex::GetAdjacencies(){
	return vertexAdjacencies;
}
/*
*Returns a string representation of this Vertex.
*For testing purposes.
*need to return adjanccy list
*/
string Vertex::ToString(){
string NumString = static_cast<ostringstream*>( &(ostringstream() << identifier) )->str();
NumString+=" ";
return 	NumString;	
}
