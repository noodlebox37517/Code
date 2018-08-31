/*
 * Student number: N8589119
 * Student Name: Thomas Fleming
 *
 * File: point.cpp
 * --------------
 * source file includes the main code for the point object
 * 
 *
 */
#include <math.h>
#include <sstream>
#include "point.h"
#include <iostream>
/*  constructor with point identifier arg */
Point::Point(int x,int y)
{
this->xCoords = x;
this->yCoords = y;	
}
/*  Destructor */
Point::~Point()
{
}
/*
*Returns the Euclidean distance between this Point and
the Point* parameter to the function
*/		
double Point::DistanceTo(Point* otherPoint ){
	double distance;
	double xdist;
	double ydist;
	xdist=(this->xCoords-otherPoint->xCoords);
	ydist=(this->yCoords-otherPoint->yCoords);
	if(xdist<0.0)xdist=-xdist;
	if(ydist<0.0)ydist=-ydist;
	distance=sqrt((pow(xdist,2.0))+(pow(ydist,  2.0)));
	
}
/*
Produces a string representation of this Point
*/
string Point::ToString(){

string CoordsNumString = static_cast<ostringstream*>( &(ostringstream() << xCoords) )->str();
CoordsNumString+=" ";
CoordsNumString += static_cast<ostringstream*>( &(ostringstream() << yCoords) )->str();
return 	CoordsNumString;
}
