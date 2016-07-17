using UnityEngine;


//Usage types outside of battle
public enum ItemUsageInField {

  [System.ComponentModel.Description("The item cannot be used outside of battle.")]
  NONE,
  [System.ComponentModel.Description("Can be used on a single Pokémon, then disappears after usage. (Potion, Oran Berry, etc)")]
  SINGLEUSEONPOKEMON,
  [System.ComponentModel.Description("Can be used once by the player, then disappears after usage. (Escape rope, Repel, etc)")]
  SINGLEUSEBYPLAYER,
  [System.ComponentModel.Description("Technical Machine")]
  TM,
  [System.ComponentModel.Description("Hidden Machine")]
  HM,
  [System.ComponentModel.Description("Can be used on a single Pokémon, but does not disappear after usage. (Poké Flute, etc)")]
  UNLIMITEDUSAGEONPOKEMON

};

//Usage types in battle
public enum ItemUsageDuringBattle {

  [System.ComponentModel.Description("The item cannot be used during battle.")]
  NONE,
  [System.ComponentModel.Description("Can be used on any single Pokémon, even if not active battler, then disappears after usage. (Potion, Oran Berry, etc)")]
  SINGLEUSEONANYPOKEMON,
  [System.ComponentModel.Description("This item is a Pokéball, can be used once.")]
  POKEBALL,
  [System.ComponentModel.Description("Can be used on a single Pokémon, but it must be on the active battler, then disappears after usage. (X Attack, Dire Hit, etc)")]
  SINGLEUSEONACTIVEPOKEMON,
  [System.ComponentModel.Description("Can be used on a single Pokémon, but it must be on the active battler, but does not disappear after usage. (Poké Flute, etc)")]
  UNLIMITEDUSAGEONACTIVEPOKEMON,
  [System.ComponentModel.Description("Can be used on any single Pokémon, and does not disappear after usage.")]
  UNLIMITEDUSAGEONANYPOKEMON

};

//Item special types
public enum ItemSpecialTypes {

  [System.ComponentModel.Description("The item is not a special item.")]
  NONE,
  [System.ComponentModel.Description("A Mail item.")]
  MAIL,
  [System.ComponentModel.Description("The item is a Mail item, and the images of the holder and two other party Pokémon appear on the Mail.")]
  SPECIALMAIL,
  [System.ComponentModel.Description("This item is a Pokéball that can capture enemy trainers Shadow Pokemon")]
  SNAGBALL,
  [System.ComponentModel.Description("This is a berry that can be planted.")]
  BERRY,
  [System.ComponentModel.Description("This is a Key Item.")]
  KEYITEM

}
