# Blind Pathfinding Sample

Example enemy pathfinding that tracks player using sound and "smell". Created using Unity 2022.3.22f1.

Licensed under the MIT License (more info in LICENSE file).

## General
The enemy's pathfinding is influenced by the enemy's sense of hearing and smell. If a sound is loud enough to be heard by the enemy, it will navigate towards the source. If the enemy passes through a smell trail, it will follow that trail, but only if the trail is newer than the sound. Likewise, if the enemy passes through another smell trail while following one, it will shift focus to the other smell trail if it is newer. In other words, it will prioritize the freshest sources.

## Sound-based Pathfinding
Long-range navigation towards target by listening for sounds.

Various player actions emit sound of varying volume. The volume of the sound affects the distance at which the enemy AI is able to detect the sound. Sound-emitting actions include:
- Walking (Low volume)
- Running on grass / gravel (Medium volume)
- Running on metal / through water (High volume)
- Throwing objects (High volume)
- Sneezing (High volume)

Because the AI targets sounds, distractions can be created by throwing objects, luring the AI towards the sound of impact.

## Smell-based Pathfinding
Short-range navigation towards target by following smell trails.

The player emits a short "smell trail" - breadcrumbs that the enemy is able to pick up on when within a short distance. Every second, the player drops a breadcrumb that dissipates after a few seconds. Certain actions can affect the strength of the breadcrumbs as well as the rate of dissipation; walking through a dust cloud will cause the player to stink, making their breadcrumbs last longer and have a larger detection range, and waking through water will clean the player and reset the strength and duration of their breadcrumbs.

## Third Party Assets
Royalty-free assets are used in this project for demonstration purposes, namely sound effects and textures. Sound effects are sourced from [Pixabay](https://pixabay.com/). Textures are sourced from [FreeStylized](https://freestylized.com/).