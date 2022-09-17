# AltaGamesTest

Gameplay
On the screen, the ball is the player in the lower left corner, and the goal in the upper right corner, where the ball
should hit. The path is blocked by obstacles. Ball player creates shots at the expense
its size, you need to clear the way so that the player's ball jumps along the cleared
path to the end goal.

Prototype
When tapa, the ball-shot begins to separate from the ball-player. Ball player decreases in
size is proportional to how the size of the shot ball grows and depends on
hold time (the longer, the longer). When released, the shot flies to
direction to the target, and upon collision with the first obstacle "infects"
obstacles in range and they explode.
The power of the shot - the radius of the "infection" depends on the size of the ball of the shot than it
the larger, the larger the radius. The closer the obstacles are to each other, the easier it is to
infect. You need to take small shots on lonely ones so as not to waste all
player's ball size.
After clearing the area, the ball player moves forward accordingly
goals. At the end, its size is reduced, but it should clearly have enough passage to
freely pass between obstacles by jumping in the center of the track. Track
decreases with the size of the ball.
There should be a door at the end. The door opens when the ball approaches 5
meters to her
.
If the player held the tap for too long, and the player’s ball was completely pumped into a shot
(pick up the minimum critical size) - loss. If not enough
shots to clear the way - losing.
Initially, the size of the ball should be enough with a margin of 20% to pass.
Send the completed task in the form of a build for Android, video of the gameplay
on YouTube, and project source in Unity.
