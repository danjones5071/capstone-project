# Capstone Project
SWENG Capstone Project made by Group 4 in the Unity Game Engine.

### Current Project State
This first commit is just a demo of some primitive features we will refine throughout the semester. This includes basic player movement, a simple laser, and asteroid generation. The asteroids don't hurt the player yet since it would probably be appropriate to have some UI for a "Play Again?" screen. But the player can fire lasers to destroy asteroids with the space key. For right now, the asteroids are generated with basic Instantiate and Destroy calls, but we will probably want to implement an object pool as the project gets more complex for the sake of efficiency.

### Known issues
* If the laser hits an asteroid on the very edge it may not be destroyed. That is just because I was lazy and used a circle collider that doesn't cover the entire asteroid for this first commit. For now, you'll just need to aim for the center of the asteroids.

### Temporary Image Sources
Asteroid: 
https://media.nationalgeographic.org/assets/interactives/433/90b/43390b12-c7c2-450e-94de-ac2e04b52359/public/splash/images/asteroid.png

Spaceship: 
https://uploads.scratch.mit.edu/users/avatars/14843621.png

Laser Blast:
https://donaldcarling.files.wordpress.com/2016/03/blast-harrier-laser-1.png?w=500