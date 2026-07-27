# Local Community Event Portal — CSS3 Exercises

A single-page site built to walk through all 11 CSS3 exercises for the
"Local Community Event Portal" module. Open `index.html` in a browser or
deploy the folder as-is to GitHub Pages.

## Project structure

```
community-event-portal/
├── index.html
├── css/
│   └── styles.css
├── images/
│   └── community-bg.jpg   (add your own — see note below)
└── README.md
```

> **Note on the background image:** `styles.css` references
> `images/community-bg.jpg` for the body background (Task 4 — background
> image with fallback color). No binary image is included in this
> deliverable, so drop any landscape/community photo into `images/` with
> that filename, or update the `background-image` path in `styles.css`.
> Until an image is added, the page still renders correctly using the
> `background-color: #f4f6f8;` fallback.

## Where each task lives

| # | Task | Where to find it |
|---|------|-------------------|
| 1 | Inline / internal / external CSS | `index.html` — inline style on `<h1>`, embedded `<style>` in `<head>`, `<link>` to `css/styles.css` |
| 2 | Syntax & comments | `css/styles.css` — sectioned with `/* ... */` headers throughout |
| 3 | Selectors playground | `css/styles.css` — `*`, `h2`, `#mainHeader`, `.eventCard`, `h3, p` grouping |
| 4 | Color & background | `css/styles.css` — HEX/RGBA colors, body background image + fallback, gradient headers (`#mainHeader`, `.hero`) |
| 5 | Typography | `index.html` `<head>` — Google Fonts (`Poppins`, `Merriweather`) via `<link>`; font properties throughout `styles.css` |
| 6 | Links & lists | `css/styles.css` — `:link/:hover/:active/:visited` on nav and content links; `list-style-type: none` on nav |
| 7 | Table styling | `index.html` `#admin-table` + `css/styles.css` — borders, padding, `nth-child(even)` zebra striping, `border-collapse: collapse` |
| 8 | Box model & layout | `css/styles.css` — `.eventCard` border/padding/margin, `.signup-form input:focus` outline, `.hidden-visibility` vs `.hidden-display` |
| 9 | Multi-column text | `css/styles.css` `.news-article` — `column-count`, `column-gap`, `column-rule` |
| 10 | Responsive design | `css/styles.css` — `@media (max-width: 768px)` and `@media (max-width: 480px)` blocks; `vw` units on `.hero p` |
| 11 | Debug & test | See below |

## Task 11 — Debugging with DevTools

1. Open the page in Chrome, then open DevTools (`F12` or `Ctrl+Shift+I`).
2. Click the **device toolbar** icon (or `Ctrl+Shift+M`) and switch between
   presets (iPhone, iPad, Responsive) to confirm the nav stacks vertically
   and the bulletin collapses to one column below 768px.
3. In the **Elements** panel, select `.eventCard` or `.signup-form input`
   and use the **Styles** pane to live-edit `padding`, `border`, or
   `outline` values and see the box model update in the layout box at the
   bottom of the pane.
4. Open the **Network** tab, reload the page, and filter by "CSS" to
   confirm `styles.css` returns a `200` status — this verifies the
   external stylesheet linked in Task 1 is loading correctly.

## Deploying to GitHub Pages

```bash
git init
git add .
git commit -m "CSS3 exercises: Local Community Event Portal"
git branch -M main
git remote add origin <your-repo-url>
git push -u origin main
```

Then enable **Settings → Pages → Deploy from branch → main / (root)** in
your GitHub repository.
