using UnityEngine;


//Damage Category
public enum MoveDamageCategory {

  [System.ComponentModel.Description("Deal physical damage.")]
  PHYSICAL,
  [System.ComponentModel.Description("Deals special damage.")]
  SPECIAL,
  [System.ComponentModel.Description("Does not deal damage.")]
  STATUS

};

public enum MoveTargetType {

  [System.ComponentModel.Description("Single Pokémon other than the user")]
  SINGLESELECTEDPOKEMON,
  [System.ComponentModel.Description("No target (Counter, Metal Burst, etc)")]
  NOTARGET,
  [System.ComponentModel.Description("Single opposing Pokémon selected at random (Outrage, Thrash, etc)")]
  SINGLERANDOMOPPONENT,
  [System.ComponentModel.Description("All opposing Pokémon")]
  ALLOPPONENTS,
  [System.ComponentModel.Description("All Pokémon other than the user")]
  ALLEXCEPTUSER,
  [System.ComponentModel.Description("User")]
  USER,
  [System.ComponentModel.Description("User's side (Light Screen, Mist, etc)")]
  OWNSIDE,
  [System.ComponentModel.Description("Both sides (Trick Room, Sunny Day, etc)")]
  BOTHSIDES,
  [System.ComponentModel.Description("Opposing side (Spikes, Stealth Rocks, etc)")]
  OPPOSINGSIDE,
  [System.ComponentModel.Description("User's partner (Helping Hand)")]
  PARTNER,
  [System.ComponentModel.Description("Single Pokémon on user's side (Acupressure)")]
  SINGLESELECTEDUSER,
  [System.ComponentModel.Description("Single opposing Pokémon (Me First)")]
  SINGLESELECTEDOPPONENT,
  [System.ComponentModel.Description("Single opposing Pokémon directly opposite of user")]
  OPPONENTDIRECTLYOPPOSITE

};
