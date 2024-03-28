# Blind Pathfinding Sample

Example enemy pathfinding that tracks player using sound and "smell". Created using Unity 2022.3.22f1.

Licensed under the MIT License (more info in LICENSE file).

## Sound-based Pathfinding
Long-range navigation towards the player by listening for sounds. Imprecise (~5m).

Various player actions emit sound of varying volume. The volume of the sound effects the distance at which the enemy AI is able to detect the sound. Sound-emitting actions include:
- Walking (Low volume)
- Running (Medium volume)
- Crunching Leaves (High volume)
- Sneezing (High volume)

## Smell-based Pathfinding
Short-range navigation towards the player by sniffing out the player's trail. Precise (0m).

The player emits a short "smell trail" -  breadcrumbs that the enemy is able to pick up on when within a short distance. Certain actions can raise or lower the rate of decay of these breadcrumbs; Odorizers (dust cloud, toxic waste, flower bed) lower the rate of decay, and Deodorizers (water) raise the rate of decay.