# LITTLE PENGUIN
#### Video Demo:  https://youtu.be/4nG7E4brBSs
#### Description:
Little Penguin is a short platformer game where you play as a penguin. Final project for CS50X.

## Menu Scene

### Menu
This script has two functions, StartGame() and EndGame() which are attached to the two buttons on the menu scene.
### MenuMoveBackground
This script moves the background image of the menu screen and loops it back to the beginning position to create the illusion of an infinitely-scrolling image.

## Main Scene

### Player
#### Controller
The script called PlayerMovement is in charge of handling the player's movement. It recognizes input from the player using unity's New Input System and sets variables that are used by movement methods to apply force to the player. The movement script uses Unity's built-in Physics system to move the player using Rigidbody.ApplyForce(). Collisions are handled with Unity's layer system. By grouping colliders to separate layers, the script is able to differentiate between different floor types like ground and ice, as well as recognizing when the player is not on any floor.
##### Movement data
All the editable variables that affect how the player's movement feels such as run speed, jump height, fall speed, etc. are gathered in one scriptable object called PlayerForceData. Having all variables in one place like this makes it easy to edit the player movement variables to adjust the game's feel. It also makes it possible to make multiple versions of the player's movement by making multiple scriptable objects, which makes iteration much easier.
##### Player One Way Platform
This is a small separate script which is in charge of one way platforms. This script checks if the player is standing on a one way platform and is holding the down button. If the player holds the down button while on a platform, this script temporarily disables the collision between this collider and the player using a coroutine.
#### Animation
The script PlayerAnimator handles the animation of the player's sprites. It uses information about the player from the movement script and sets the animations according to the current state of the player. It uses bools to dictate which animations to play and sets state animations on and off depending on the player's current state. The player has 6 animated states: death, wallSlide, fall, jump, move, and idle.

### Gameplay Elements
#### KillOnCollision
This kills the player on collision. Used by spikes and the bottom boundary of levels.
#### MovingPlatform
This moving platform parents the player to it and moves. It has three states: Idle, Moving, and Returning. It displays the direction and distance of the platform's movement using gizmos.
#### SpringPad
This applies upward force to the player once the player enters its trigger, and prevents the player from jumping again by setting the player's state.
#### CheckpointSetter
This sets the player's current checkpoint through triggers in every level so that the player does not have to restart the game from the beginning.
#### EndTrigger
This is the trigger for the mini-cutscene at the end. It disables player input, plays a player animation, plas audio, and activates the goodbye screen.
#### GameManager
The game manager handles pausing, unpausing, and disabling input for the game. It is a static class, which makes it easier to access. It also handles the players death.
#### PlayerRespawner
This script handles the respawning of the player by restarting the scene. It checks the current checkpoint of the player and spawns the player in that checkpoint's location once the scene loads.

### Camera
#### Parallax
This script is in charge of handling the game's background. It moves the different background layers at different speeds according to their distance from the camera to create a parallax effect. It prevents the background from being too static.
#### RoomCamera
This handles the activation for the camera of every level in the game. Each level has a separate camera locked to the size of that room to prevent the player from seeing past the level's boundaries.

### Audio
#### AudioManager
This handles making audio sources for each Sound object in a sound array. It also has methods tat handle muting, unmuting, playing, and stopping sounds.It is a static class, which means other scripts can access it easier.
#### Sound
This is an object that includes an audioclip's pitch, name, volume and looping status, making it easier to edit in the audiomanager.

