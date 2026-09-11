# Reel Simulator

 **A stupid little game about scrolling reels when you should be paying attention in class.**
 An idea inspired from real life, it came to me when I was sitting in class and I worked on it

Reel Simulator is a short Unity game where you try to survive an entire class while secretly scrolling through reels on your phone.

The catch?

**Your teacher is watching. And your brainrot is running out.**

---

## Gameplay

Pull out your phone and start scrolling through randomly selected reels.

Every reel has a different rarity and gives you points if you watch it long enough.

But while you're scrolling:

* The teacher can catch you
* Your brainrot slowly drains
* Hide your phone when the teacher looks your way
* Different reels have different rarities
* SFX and music react to what's happening
* Random notifications appear on your phone
* Try to get the highest score possible

### Reel Rarities

| Rarity    | Chance | Points |
| --------- | -----: | -----: |
| Common    |    55% |      1 |
| Uncommon  |    25% |      5 |
| Rare      |    12% |     10 |
| Legendary |     6% |     50 |
| Mythical  |     2% |    100 |

---

## Controls

| Action                | Control     |
| --------------------- | ----------- |
| Pull phone out / hide | `F`         |
| Scroll reels          | Mouse Wheel |
| Scroll credits        | Mouse Wheel |

> Controls may vary between builds.

---

## Brainrot System

Your **Brainrot** is constantly draining while you're not watching reels.

Watching reels restores it.

When it reaches zero, the game ends.

The game also gives you a visual warning and sound effect when your brainrot gets dangerously low.

---

## Teacher System

The teacher has a vision cone.

If:

1. You're within her vision range
2. You're inside her vision cone
3. Your phone is out

you have a short amount of time to hide your phone.

Fail to do so and...

**YOU'VE BEEN CAUGHT.**

---

## Phone Notifications

While you're scrolling, random notifications can appear on your phone.

Because obviously these are the most important things happening during class.

---

## Audio

The game features separate audio for:

* Home screen music
* In-game music
* Teacher talking
* Teacher angry/caught SFX
* Phone pull-out/hide SFX
* Reel scrolling SFX
* Score/chime SFX
* Notification sounds
* Brainrot warning
* Brainrot game-over sound

---

## Built With

* Unity
* C#
* Unity Video Player
* Unity Input System
* Unity UI
* TextMeshPro
* Unity Animator
* Desktop Build
* WebGL

---

## Project Structure

```text
Assets/
├── Scenes/
├── Scripts/
├── StreamingAssets/
│   └── Reels/
├── Audio/
├── Materials/
├── Models/
├── Prefabs/
└── UI/
```

Reel videos are stored in:

```text
Assets/StreamingAssets/Reels/
```

This allows the WebGL build to load the reel videos separately rather than embedding them directly into the Unity build.

---

## Web Version

The game supports a **WebGL build**, allowing it to be played directly in a browser without installing the desktop version.

The WebGL build can be hosted using platforms such as itch.io.

---

## Running Locally

After creating a WebGL build, you need to run it through a web server.

For example, with Python:

```bash
cd ReelSimWeb
python -m http.server 8000
```

Then open:

```text
http://localhost:8000
```

**Do not open `index.html` directly** with `file://`, as Unity WebGL builds require a web server.

---

## Credits

### Programming

**Fajsef9**

### Home Screen Art

**@fsossh**

### Models

**Sketchfab / Unity Asset Store**

### Music & SFX

Various assets from creators on Pixabay and other sources.

Full asset attribution and licensing information can be found in the game's **Credits** screen.

---

## License

This project is primarily a personal/educational game project.

Please **do not redistribute the game's assets, videos, music, or other third-party content** without checking their respective licenses.

---

## About

Reel Simulator started as a small Unity experiment and turned into a game about the extremely important skill of:

> **Scrolling reels without getting caught in class.**

Built with Unity, for fun, to have fun!

---

## Play now
https://fajsef.itch.io/reel-simulator

**If you enjoyed the game, consider starring the repository.**
