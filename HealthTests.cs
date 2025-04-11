using Xunit; // xUnit for C# testing BLACK BOX TESTING Shaheen Nafeie

namespace healthnamespace{
public class HealthTests{

    [Fact]
    public void ReduceHealthIfDamaged(){
        var health = new CougarHealth(100); // start at 100 health
        health.healthDamage(15);
        Assert.Equal(85,health.healthAmount);
    } //This test ensures the health bar goes down if player is damaged

    [Fact]
    public void IncreaseHealthIfHealed(){
        var health = new CougarHealth(91);
        health.healthHeal(9);
        Assert.Equal(100, health.healthAmount);
    } //This test ensures the health bar increases if player is healed

    [Fact]
    public void NoNegativeDamage(){
        var health = new CougarHealth(14);
        health.healthDamage(98);
        Assert.Equal(0, health.healthAmount);
    

    }//If the attack is higher than the current health, then the health must always be 0.

    [Fact]
    public void NoHealthOverHundred(){
        var health = new CougarHealth(88);
        health.healthHeal(20);
        Assert.Equal(100, health.healthAmount);

    }   //Health bar cannot go over 100


    
}
}




