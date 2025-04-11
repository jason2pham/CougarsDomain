//SHAHEEN NAFEIE Integration testing

//Testing to ensure cougarHealth and HealthPowerUp are working together as expected.

using Xunit;

public class IntegrationTests{

    [Fact]
    public void DamageAndAttack(){
        var player = new CougarHealth(90);
        var minHealthUp = new HealthPowerUp(25);

        player.healthDamage(30);
        Assert.Equal(60, player.healthAmount); // player now has 60 health

        minHealthUp.UseHeal(player);
        Assert.Equal(85, player.healthAmount); //health should be 85 after health power up

        player.healthDamage(90);
        Assert.Equal(0, player.healthAmount); //should stay at 0
    }
}