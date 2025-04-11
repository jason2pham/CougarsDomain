//WHITE BOX TESTING SHAHEEN NAFEIE
/* White box vs Black box: In white box testing we have an understanding of the code 
and we are testing the code based on how it is written. Black box however, we are testing just the 
functionallity. 
*/
using Xunit;

public class HealthTestsWhiteBox{

    /* Below we are testing the healthDamage method. This method decreases
    the health of the player but it cannot go under 0. We test to make sure 
    the damage is working correctly, but also tests for the edge case in which the health
    cannot go below 0. 

    Method under testing:
       public void healthDamage(float dam){
        healthAmount = Math.Max(0, healthAmount - dam);
    }
    */
    //using different values than black box 
    [Fact]
    public void correctDecrease(){
        var health = new CougarHealth(75); 
        health.healthDamage(15);
        Assert.Equal(60,health.healthAmount);
    }

    [Fact]
    public void noHealthUnderZero(){
        var health = new CougarHealth(1);
        health.healthDamage(75);
        Assert.Equal(0, health.healthAmount);

    }

    /* Below we are testing the healthHeal method. This method increases the players health.
    Similar to the damage method, but the opposite, the health of a player cannot go above 100. 
    Testing here to make sure the method is working correclty, and also testing our edge case in which
    the health cannot go above 100.
     
     Method under testing:
      public void healthHeal(float heal){
        healthAmount = Math.Min(100, healthAmount + heal);
    }
    */

    [Fact]
    public void correctIncrease(){
        var health = new CougarHealth(45);
        health.healthHeal(9);
        Assert.Equal(54, health.healthAmount);
    }

    [Fact]
    public void correctIncreaseTwo(){
        var health = new CougarHealth(1);
        health.healthHeal(99);
        Assert.Equal(100, health.healthAmount);
    }

    [Fact]
    public void noHealthAboveHundred(){
        var health = new CougarHealth(98);
        health.healthHeal(30);
        Assert.Equal(100, health.healthAmount);
    }

    //below is a new test to make sure that if the player is already at 100 health, 
    //it caps. 

    [Fact]
    public void StuckAt100(){
        var health = new CougarHealth(100);
        health.healthHeal(30);
        Assert.Equal(100, health.healthAmount);

    }
    // and if for some reason we include negative damage (might fail)
    [Fact]
    public void negativeDamage(){
         var health = new CougarHealth(90);
        health.healthHeal(-25);
        Assert.Equal(90, health.healthAmount);
    }


}