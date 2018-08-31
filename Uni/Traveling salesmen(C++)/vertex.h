/*
 * Student number: N8589119
 * Student Name: Thomas Fleming
 *
 * File: vertex.h
 * Interface defining functionality vertex
 * has  id and adjacencies.
 */
#include <vector>
#include <string>

#ifndef VERTEX_H
#define VERTEX_H

using namespace std;

class Vertex
{
	public:
/*  constructor with point identifier arg */
		Vertex(int);
/*  Destructor */
		~Vertex();
/*  Accsesor for id identifier */
	int GetId();
/* Adds a pointer to a Vertex to this Vertex’*/
void AddAdjacency(Vertex*);
/*  Accsesor vector of pointers of adjacent Vertice*/
vector<Vertex*> GetAdjacencies();
/*Returns a string representation of this Vertex*/
string ToString();
		
	private:
		int identifier;
		vector<Vertex*>vertexAdjacencies;
};

#endif
