/*
 * Student number: N8589119
 * Student Name: Thomas Fleming
 *
 * File: random.h
 * --------------
 * This interface provides several functions for generating
 * pseudo-random numbers.
 *
 */

#ifndef _random_h
#define _random_h

class Random {

public:

    /*
     * Constructor
     * Usage: Random rand;
     */
    Random();
    
    /*
     * Destructor
     * Usage: implicit
     */
    ~Random();
    

    /*
     * Function: RandomInteger
     * Usage: n = rand.RandomInteger(low, high);
     * ------------------------------------
     * This function returns a random integer in the range low to high,
     * inclusive.
     */
   
    int GetRandomInteger(int low, int high);
    
    /*
     * Function: RandomReal
     * Usage: d = rand.RandomReal(low, high);
     * ---------------------------------
     * This function returns a random real number in the half-open
     * interval [low .. high), meaning that the result is always
     * greater than or equal to low but strictly less than high.
     */
   
    double RandomReal(double low, double high);
    
    /*
     * Function: RandomChance
     * Usage: if (rand.RandomChance(p)) . . .
     * ---------------------------------
     * The RandomChance function returns true with the probability  indicated by p, 
     * which should be a floating-point number between 0 (meaning never) and 1 
     * (meaning always). Calling RandomChance(.30) returns true 30 percent of the time.
     */
    bool RandomChance(double p);

private:
  
    /*
     * Function: Randomize
     * Usage: Randomize();
     * -------------------
     * This function initializes the random-number generator so that
     * its results are unpredictable. If this function is not called,
     * the other functions will return the same values on each run.
     */
     void Randomise();

};
#endif
