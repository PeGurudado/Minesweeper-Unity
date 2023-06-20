# Minesweeper-Unity

![Captura de Tela (26)](https://github.com/PeGurudado/Minesweeper-Unity/assets/43541901/46425e30-1e0a-4986-84c0-344d7114eb21)

![Captura de Tela (27)](https://github.com/PeGurudado/Minesweeper-Unity/assets/43541901/9737f485-ad09-437e-8beb-cc7dbd70a49a)

## [Release link here](https://github.com/PeGurudado/Minesweeper-Unity/releases)

**Minesweeper Test**

**1. The assignment is to create a Minesweeper clone in Unity with the following
functionalities:**
  * Random generation of a board with x mines
  * Left clicking a tile opens it
  * Right clicking a tile marks it as a mine (cannot be left clicked)
  * Opened tiles show the number of mines next to it (in all 8 directions)
  * Counter that shows the number of remaining mines (max mines - marked mines)
  * Restart button

**2. Expand the game with the following functionalities:**
  * The board should be read from a configuration JSON or XML file (if the file is
  missing, random generation is used)
  * If a tile is opened that doesn't have any mines next to it, that tile is opened
  automatically as well

**3. Expand the game even more with the following functionality:**
  * Auto-play that plays the game by itself, without the knowledge of the
  whereabouts of the mines. The only time it should stop playing is if all of the
  remaining steps are luck based (i.e., if there is a mine left in the corner with 2 tiles
  remaining and there are no logical conclusions where it is)
**Solution Considerations**
  * All properties should be modifiable without code recompilation.
  * Game assets can be used from free websites such as Kenney.nl
  * A local database can be used (such as LiteDB, SQLite, CastleDB or any kind of
  basic JSON/XML data serialization.)
  * Follow proper coding conventions
  * Consider following the MVC pattern
  * Proper organization of files and GameObjects in the Scene should be present

Submit the whole project as a link to a public repository or an archive (please do not
include generated folders Library, obj. etc).
