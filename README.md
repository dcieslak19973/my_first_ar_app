# My First AR App

A browser-based Augmented Reality experience built with [A-Frame](https://aframe.io) and [AR.js](https://ar-js-org.github.io/AR.js-Docs/).

## What it does

Point your device camera at a **Hiro marker** and watch a 3-D scene appear on top of it:

- 🟥 A rotating red box
- 🔵 A blue sphere orbiting the box
- 💬 Floating "Hello, AR!" text

No app installation required — it runs entirely in the browser.

## How to run

1. Serve `index.html` over **HTTPS** (required for camera access on mobile).  
   The quickest options:
   - **GitHub Pages** – push to `main` and enable Pages in repository settings.
   - **VS Code Live Server** extension (auto-generates a local HTTPS tunnel).
   - `npx serve .` and open the printed URL.

2. Open the URL on your phone or laptop (allow camera permissions when prompted).

3. Print or open the [Hiro marker](https://raw.githubusercontent.com/AR-js-org/AR.js/master/data/images/hiro.png) on a second screen and point the camera at it.

## Tech stack

| Library | Version | Purpose |
|---------|---------|---------|
| [A-Frame](https://aframe.io) | 1.4.2 | 3-D / WebXR scene graph |
| [AR.js](https://ar-js-org.github.io/AR.js-Docs/) | latest | Marker tracking via webcam |

Both are loaded from CDN — no build step needed.