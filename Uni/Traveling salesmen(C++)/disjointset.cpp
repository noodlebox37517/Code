#include "disjointset.h"
/*
 * Student number: N8589119
 * Student Name: Thomas Fleming
 *
 * File: disjointset.cpp
 * --------------
 * source file includes the main code for disjointset object
 * 
 *
 */
#include <iostream>

using namespace std;

DisjointSet::DisjointSet(int N){
this->N = N;
size = new int[N];
	for (int i = 0; i <= N; i++){
        size[i] = 1;
    }	
id = new int[N];
for (int i = 0; i < N; i++) {
id[i] = i;
}
}


DisjointSet::~DisjointSet(){
}
int DisjointSet::Find(int i){
	while (i != id[i]) {
id[i] = id[id[i]]; // make elements point to their grandparent
i = id[i];
}
return i;
}

void DisjointSet::Union(int p, int q){
	int i = Find(p);
int j = Find(q);
if (size[i] < size[j]) {
id[i] = j;
size[j] += size[i];
} else {
id[j] = i;
size[i] += size[j];
}
}
bool DisjointSet::SameComponent(int p, int q){
	return Find(p) == Find(q);
}

