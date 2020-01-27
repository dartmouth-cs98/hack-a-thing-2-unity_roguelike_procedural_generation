# Hack-a-Thing 2: Roguelike Procedural Generation
#### Lauren Gray & Zeke Baker | 2020-01-27

## What we attempted to build

Our initial goal was to create some sort of procedurally generated structure. Lauren found a [Unity maze generation tutorial](https://www.raywenderlich.com/82-procedural-generation-of-mazes-with-unity) which we were initially considering, but since a maze is more comparable to a series of connected hallways and not rooms, we decided to keep our minds open a little bit more before we started. Zeke then found the following YouTube video, which shows off procedural generation of a dungeon-like structure in Unity:

[![Unity3d Dungeon Generator](https://i.ytimg.com/vi/0YXoq12Devw/maxresdefault.jpg)](https://youtu.be/0YXoq12Devw)

The video linked to [a Reddit post by u/DMeville](https://www.reddit.com/r/Unity3D/comments/3dt2in/procedural_dungeon_generator_im_working_on/?st=j3tkmk23&sh=61e60504), in which they outlined (in pseudocode) the general algorithm they were using. For our Hack-a-Thing, we decided to take a stab at implementing that pseudocode. Zeke had a faint understanding of the ["roguelike" genre](https://en.wikipedia.org/wiki/Roguelike) frequently associated with [dungeon-crawling](https://en.wikipedia.org/wiki/Dungeon_crawl), which is what gave this project its name.

## Who did what

Zeke worked on the algorithms to couple rooms together at pre-defined doors and to determine whether two rooms were intersecting or blocking doors. This door-blocking functionality is still a little buggy, but for the most part it works currently.

Lauren implemented the pseudocode from the Reddit post, which ended up being, at it’s deepest, a `for` loop inside a `do while` loop inside a `do while` loop inside a `while` loop. Not fun, but also pretty cool to see working once it was all put together.

We worked together at the end to try to find and fix some bugs with rooms spawning inside of/overlapping other rooms, which generally turned out to be an issue with `Vector3`s not being precise after having rotations applied to them.

After some careful debugging and rounding, we managed to get everything working as planned.

![Screenshot of current state](/Assets/Images/roguelikePG_20200127.JPG)

## What we learned

* To heck with rotations (our new mantra)
* Unity doesn’t support `Tuple`s 🙁
* It can take many iterations and a lot of thought to come up with the optimal data structure/organization/methods for a given problem
* The data structure/organization/methods may be subject to a change after working things out with designers, so this will be evolving as we go

## How does this hack-a-thing inspire you or relate to your possible project ideas?

This hack-a-thing covers the main functionality of this project that Zeke is most interested in working on for this project. For both of us, it is inspiring to have a feature of our game that will likely be integral to its functionality working (at least primitively) so soon.

Having this functionality working early on is useful because it will allow for diversity in play experience when conducting user testing. It is good to figure out the basics of procedural map generation early. A solid foundation will give designers a lot of freedom and developers a lot of time to build out the map generation into something very polished.

## What didn’t work

As mentioned earlier, we had some issues with rotations of `Vector3` values that resulted in some `Room`s generating on top of one another as well as `Door`s being blocked by walls.

We also initially attempted to incorporate `Tuple`s into our `RoomManager`, which eventually had to be changed because Unity does not support `Tuple`s.

## External links (tutorials)

[dmeville’s “https://youtu.be/0YXoq12Devw” on YouTube](https://youtu.be/0YXoq12Devw)
[DMeville’s “Procedural Dungeon Generator I'm working on!” on Reddit](https://www.reddit.com/r/Unity3D/comments/3dt2in/procedural_dungeon_generator_im_working_on/?st=j3tkmk23&sh=61e60504)
[Joseph Hocking’s  “Procedural Generation of Mazes with Unity” on raywenderlich.com](https://www.raywenderlich.com/82-procedural-generation-of-mazes-with-unity)