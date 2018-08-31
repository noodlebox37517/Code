/*
 * Student number: N8589119
 * Student Name: Thomas Fleming
 *
 * File: disjointset.h
 * Interface defining functionality for disjointset type for use with mst
 */
#ifndef DISJOINTSET_H
#define DISJOINTSET_H

class DisjointSet
{
	public:
		DisjointSet(int);
		~DisjointSet();
		int Find(int);
		void Union(int, int);
		bool SameComponent(int, int);
		
	private:
		int* id;
		int N;
		int* size;
};

#endif
