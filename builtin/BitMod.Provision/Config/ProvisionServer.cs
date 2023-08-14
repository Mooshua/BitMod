namespace BitMod.Provision.Config;

public class ProvisionServer
{
	public bool BleedingEnabled { get; set; } = true;

	public float DamageMultiplier { get; set; } = 1f;

	public bool SpectatorsEnabled { get; set; } = true;

	public bool HitMarkersEnabled { get; set; } = true;

	public bool StaminaEnabled { get; set; } = true;

	public bool FriendlyFireEnabled { get; set; } = true;

	public bool PointLogEnabled { get; set; } = true;

	public bool OnlyWinnersCanVote { get; set; } = false;


	public List<string> Gamemodes { get; set; } = new();

	public List<string> Maps { get; set; } = new();
}
