# The Fountain Of Objects

The Fountain of Objects game is a 2D grid-based world full of rooms. Most rooms are empty, but a few
are unique rooms. One room is the cavern entrance. Another is the fountain room, containing the
Fountain of Objects.

The player moves through the cavern system one room at a time to find the Fountain of Objects. They
activate it and then return to the entrance room. If they do this without falling into a pit, they win the
game.

Unnatural darkness pervades the caverns, preventing both natural and human-made light. The player
must navigate the caverns in the dark, relying on their sense of smell and hearing to determine what
room they are in and what dangers lurk in nearby rooms.

This challenge serves as the basis for the other challenges in this level. It must be completed before the
others can be started. The requirements of this game are listed below.

## Main Objectives

- The world consists of a grid of rooms, where each room can be referenced by its row and column.
- North is up, east is right, south is down, and west is left
- The game’s flow works like this: The player is told what they can sense in the dark (see, hear, smell).
  Then the player gets a chance to perform some action by typing it in. Their chosen action is resolved
  (the player moves, state of things in the game changes, checking for a win or a loss, etc.). Then the
  loop repeats.
- Most rooms are empty rooms, and there is nothing to sense.
- The player is in one of the rooms and can move between them by typing commands like the
  following: “move north”, “move south”, “move east”, and “move west”. The player should not be able
  to move past the end of the map.
- The room at (Row=0, Column=0) is the cavern entrance (and exit). The player should start here. The
  player can sense light coming from outside the cavern when in this room. (“You see light in this room
  coming from outside the cavern. This is the entrance.”)
- The room at (Row=0, Column=2) is the fountain room, containing the Fountain of Objects itself. The
  Fountain can be either enabled or disabled. The player can hear the fountain but hears different
  things depending on if it is on or not. (“You hear water dripping in this room. The Fountain of Objects
  is here!” or “You hear the rushing waters from the Fountain of Objects. It has been reactivated!”) The
  fountain is off initially. In the fountain room, the player can type “enable fountain” to enable it. If the
  player is not in the fountain room and runs this, there should be no effect, and the player should be
  told so.
- The player wins by moving to the fountain room, enabling the Fountain of Objects, and moving back
  to the cavern entrance. If the player is in the entrance and the fountain is on, the player wins.
- Use different colors to display the different types of text in the console window. For example,
  narrative items (intro, ending, etc.) may be magenta, descriptive text in white, input from the user
  in cyan, text describing entrance light in yellow, messages about the fountain in blue.

## Expansions

### Small, Medium, or Large

The larger the Cavern of Objects is, the more difficult the game becomes. The basic game only requires
a small 4×4 world, but we will add a medium 6×6 world and a large 8×8 world for this challenge.

#### Objectives:

- Before the game begins, ask the player whether they want to play a small, medium, or large game.
  Create a 4×4 world if they choose a small world, a 6×6 world if they choose a medium world, and an
  8×8 world if they choose a large world.
- Pick an appropriate location for both the Fountain Room and the Entrance room.

### Maelstroms

The Uncoded One knows the significance of the Fountain of Objects and has placed minions in the
caverns to defend it. One of these is the maelstrom—a sentient, malevolent wind. Encountering a
maelstrom does not result in instant death, but entering a room containing a maelstrom causes the
player to be swept away to another room. The maelstrom also moves to a new location. If the player is
moved to another dangerous location, such as a pit, that room’s effects will happen upon landing in that
room.
A player can hear the growling and groaning of a maelstrom from a neighboring room (including
diagonals), which gives them a clue to be careful.
Modify the basic Fountain of Objects game in the ways below to add maelstroms to the game.

#### Objectives:

- Add a maelstrom to the small 4×4 game in a location of your choice.
- The player can sense maelstroms by hearing them in adjacent rooms. (“You hear the growling and
  groaning of a maelstrom nearby.”)
- If a player enters a room with a maelstrom, the player moves one space north and two spaces east,
  while the maelstrom moves one space south and two spaces west. When the player is moved like
  this, tell them so. If this would move the player or maelstrom beyond the map’s edge, ensure they
  stay on the map. (Clamp them to the map, wrap around to the other side, or any other strategy.)
