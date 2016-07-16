# Pokémon-Unity-Essentials-2D

An open source framework for making fangames

Add stuff to this list if I forgot things (likely, there's a lot of stuff)
Subject to likely change the way things are handled

## Things to do

* Overworld
  - [ ] Maps
    - [x] Basic maps
    - [x] Basic Connectivity
    - [ ] Moving to maps on different scenes (and back) [Don't work on this yet, the main concern is how all the peripheral things are handled upon transfer]

  - [ ] Player Movement (basic movement)
    - [x] Grid based movement (movement distance near perfect.  Do more testing when more maps are added to check)
    - [x] Walking
    - [ ] Running
    - [ ] Surfing

  - [ ] Passability and Terrain
    - [x] Basic check for colliders
    - [ ] Water
    - [ ] Soot Grass
    - [ ] Ice
  
  - [ ] Priority
    - [x] Assign different groups of tiles to different layers
    - [ ] Allow the player (and events) to switch between layers (priority may depend on what 'level' of terrain the player is on, like climbing up stairs would then rend the player above certain tiles)
    - [ ] Events automatically change their priority sub-layer when they move 
  
  - [ ] Day Night Shader (works, but needs better selection of shades)

* Audio
  - [ ] General Audio functions (play/switch BGM, BGS, SE, and Cries) [need to rework the way they're stored, breaks in compiled version]
  - [ ] Fade in/out
  - [ ] Remember where left of and resume (when return from battle or whatever)

* Editor 
  - [x] Snap movement (Ctrl+L to open the AutoSnap panel.  Use this to align the player and any events along the grid, and move them along it)
  - [x] Metadata attatches to each "Map" in the Scene
    - [x] Indoor/Outdoor
    - [ ] BGM (curently fails to change upon map change, but this might be an audio issue)
    - [ ] Allow Bike
    - [ ] Bike Only
    - [ ]  Encounters (a "map" will have it's own encounters, but individual tile layers will also have encounters that can override the maps, if desired)

  - [ ] Automatically add Metadata script when importing map from Tiled (might have done this, can't remember)
  - [ ] Editors for the XML files

* Compiler(XML files, enums mentioned aren't compiled, but compilers will rely on them)
  - [x] Types
  - [x] Abilities (haven't added all abilities, but compiler works)
  - [ ] Items
  - [ ] Moves
  - [ ] Pokemon 
    - [ ] Evolution Types (enum)
    - [ ] Colors (enum)
    - [ ] Breeding Groups (enum)
    - [ ] Exp Gain (enum)
    - [ ] Shiny sprites will use a color pallete swap, defined in the XML
  - [ ] Trainers

* General Utilies During Runtime

  - [x] A watcher that determines when map changes are made
    - [ ] Metadata changes implemented upon map change
  - [ ] Some sort of player settings
  - [ ] Some sort of utility that keeps track of player information (party, location, etc).  This is sort of done 
  - [ ] Game Switches (a hashtable that links a string name with a boolean)
  - [ ] Game Variables (a hashtable that links a string name with some variable)

* UI Stuff
  - [ ] Display Text Boxes (needs to be redone to support any screen size)
    - [x] Display letter by letter
    - [ ] Pause when out of room, then move to next lines
    - [ ] Support easy text formatting
      - [ ] Color
      - [ ] Bold/italics
      - [ ] Reference variables
      - [ ] Display other windows with commands (show money, show a name, show a picture, etc)
  - [ ] Display Choice Windows
  - [ ] Pause Menu (will use choice windows)
  - [ ] Bag
  - [ ] Party
  - [ ] Pokédex
  - [ ] Options
  - [ ] Save
  - [ ] Loading Screen
  - [ ] Title Screen (basic screen is done, needs work)
  - [ ] Intro video thing
  - [ ] Player credits
  - [ ] PokéMart shopping
  - [ ] Pokémon Summary
  - [ ] Storage (both item and pokémon)

* Battle Scene
  - [ ] Figure this out later



