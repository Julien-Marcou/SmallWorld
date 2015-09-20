![SmallWorld Logo](https://raw.githubusercontent.com/Julien-Marcou/SmallWorld/master/Resources/SmallWorldLogo.png)

SmallWorld
===========

SmallWorld is a 2-player tactical game.  
Players can only play one at a time on a local computer (turn based game).


Features
-----------

- Flexible responsive UI
- MVVM design pattern
- Ambient sounds
- Artificial intelligence (using Dijkstra's algorithm)
- Hexagonal map layout
- Random map generation (using seed)
- Custom map generation (handmade)
- Save/reload games


Create a new game
-----------

Select one of the available maps, then each player can choose his own
- Pseudo
- Faction
- Color

A player can be replaced by an artifical intelligence (random or Dijkstra's algorithm).


Factions
-----------

Each faction has its own statistics, advantages and disadvantages

![SmallWorld Units](https://raw.githubusercontent.com/Julien-Marcou/SmallWorld/master/Resources/Units.png)

### Dwarves

Statistics
- 5 health points
- 2 attack points
- 1 defense point

Advantages
- Can move from mountain to mountain

Disadvantages
- None

### Elfs

Statistics
- 5 health points
- 2 attack points
- 1 defense point

Advantages
- 50% chance of resurrecting after being killed
- Moves 2x faster in Forests 

Disadvantages
- Moves 2x slower in Deserts

### Knights

Statistics
- 5 health points
- 2 attack points
- 2 defense points

Advantages
- Moves 2x faster in Plains 

Disadvantages
- Moves 2x slower in Swamps

### Orcs

Statistics
- 5 health points
- 2 attack points
- 2 defense points

Advantages
- Gains 1 score point after each kill
- Moves 2x faster in Plains 

Disadvantages
- None

### Slimes

Statistics
- 3 health points
- 1 attack point
- 2 defense points

Advantages
- Starts with 50% more unit
- 20% chance of resurrecting after being killed
- Moves 4x faster in Swamps 

Disadvantages
- Moves 2x slower in Mountains
- Moves 2x slower in Deserts


Screenshots
-----------

![SmallWorld Home Page](https://raw.githubusercontent.com/Julien-Marcou/SmallWorld/master/Resources/HomePage.jpg)

![SmallWorld Game creation Page](https://raw.githubusercontent.com/Julien-Marcou/SmallWorld/master/Resources/GameCreationPage.jpg)

![SmallWorld Game Page 1](https://raw.githubusercontent.com/Julien-Marcou/SmallWorld/master/Resources/GamePage1.jpg)

![SmallWorld Game Page 2](https://raw.githubusercontent.com/Julien-Marcou/SmallWorld/master/Resources/GamePage2.jpg)


Known issues
-----------

- Zooming one the map is relative to the top-left corner (not the center of the map neither the cursor position)
- When A.I are playing, quit the game doesn't stop the A.I
- Sometime loss of the "Ctrl + Mouse wheel" zoom command (probably due to some bad focus/key events implementation)
- Using arrow keys to move on the map doesn't bring into view the tile if the tile is out of the viewport
