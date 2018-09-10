# Capstone Project
SWENG Capstone Project made by Group 4 in the Unity Game Engine.

### Current Project State
The game currently includes the following basic features:
* Basic player movement (up and down using arrow or 'W' and 'S' Keys).
* Firing a laser using the space key.
* Randomly generated asteroids that are destroyed when hit by a laser.
* Simple sound effects.

The asteroids don't hurt the player yet since it would probably be appropriate to have some UI for a "Play Again?" screen. But the player can fire lasers to destroy asteroids with the space key. For right now, the asteroids are generated with basic Instantiate and Destroy calls, but we will probably want to implement an object pool as the project gets more complex for the sake of efficiency.

[Playable Link](https://htmlpreview.github.io/?https://github.com/danjones5071/capstone-project/blob/master/Group4Capstone/Export/index.html)

### Known issues
* If the laser hits an asteroid on the very edge it may not be destroyed. That is just because I was lazy and used a circle collider that doesn't cover the entire asteroid for the first commit. For now, you'll just need to aim for the center of the asteroids.
* With the addition of vertical movement, the asteroids will sometimes move off-screen before they have a chance to reach the player. This should just take some tweaking.
* If the game goes into fullscreen mode, the rear of the player will be partially cut off. This can be corrected with a script to change the player's position at runtime if fullscreen is detected.
* There are a couple of warnings about deprecated methods. For right now, this is just due to some scripts in the standard particle assets. It's strange that this warning shows up for Unity-provided assets, but it shouldn't cause any real problems. These warnings do appear in other projects too, so it has nothing to do with our work.

## Current Outside Resources:
### Images
Asteroid: 
https://media.nationalgeographic.org/assets/interactives/433/90b/43390b12-c7c2-450e-94de-ac2e04b52359/public/splash/images/asteroid.png

Spaceship: 
https://uploads.scratch.mit.edu/users/avatars/14843621.png

Laser Blast:
https://donaldcarling.files.wordpress.com/2016/03/blast-harrier-laser-1.png?w=500
