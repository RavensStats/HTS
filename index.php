<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Stark 44</title>
    <style>
        @import url('https://fonts.googleapis.com/css2?family=Bebas+Neue&family=Rajdhani:wght@400;600&display=swap');

        *, *::before, *::after { box-sizing: border-box; margin: 0; padding: 0; }

        body {
            min-height: 100vh;
            display: flex;
            flex-direction: column;
            font-family: 'Rajdhani', sans-serif;
            color: #e0e0e0;
            background-color: #0a0a0a;
            /* Chevron texture via CSS: two diagonal stripes layered */
            background-image:
                repeating-linear-gradient(
                    135deg,
                    transparent,
                    transparent 10px,
                    rgba(255,255,255,0.03) 10px,
                    rgba(255,255,255,0.03) 20px
                ),
                repeating-linear-gradient(
                    45deg,
                    transparent,
                    transparent 10px,
                    rgba(255,255,255,0.03) 10px,
                    rgba(255,255,255,0.03) 20px
                );
        }

        /* ── Header ── */
        header {
            background: linear-gradient(90deg, #0d0d0d 0%, #1a1a1a 50%, #0d0d0d 100%);
            border-bottom: 2px solid #c8a84b;
            padding: 18px 32px;
            display: flex;
            align-items: center;
            gap: 16px;
            box-shadow: 0 4px 24px rgba(0,0,0,0.7);
        }

        header h1 {
            font-family: 'Bebas Neue', sans-serif;
            font-size: 3rem;
            letter-spacing: 6px;
            color: #c8a84b;
            text-shadow:
                0 0 12px rgba(200,168,75,0.5),
                2px 2px 0 #000;
        }

        header .tagline {
            font-size: 0.85rem;
            letter-spacing: 3px;
            text-transform: uppercase;
            color: #888;
            margin-top: 4px;
        }

        /* ── Layout ── */
        .layout {
            display: flex;
            flex: 1;
            min-height: calc(100vh - 80px);
        }

        /* ── Left Nav ── */
        nav {
            width: 220px;
            min-width: 220px;
            background: rgba(10,10,10,0.85);
            border-right: 1px solid #222;
            padding: 24px 0;
            display: flex;
            flex-direction: column;
            gap: 4px;
        }

        nav .nav-section-title {
            font-size: 0.65rem;
            letter-spacing: 3px;
            text-transform: uppercase;
            color: #555;
            padding: 12px 24px 6px;
        }

        nav a {
            display: block;
            padding: 10px 24px;
            color: #aaa;
            text-decoration: none;
            font-size: 0.95rem;
            font-weight: 600;
            letter-spacing: 1px;
            transition: background 0.15s, color 0.15s, border-left 0.15s;
            border-left: 3px solid transparent;
        }

        nav a:hover {
            background: rgba(200,168,75,0.08);
            color: #c8a84b;
            border-left: 3px solid #c8a84b;
        }

        /* ── Main content ── */
        main {
            flex: 1;
            padding: 40px 48px;
        }

    </style>
</head>
<body>

    <header>
        <div>
            <h1>STARK 44</h1>
            <div class="tagline">Welcome To Stark Tower</div>
        </div>
    </header>

    <div class="layout">
        <nav>
            <div class="nav-section-title">Menu</div>
            <!-- Navigation links go here -->
        </nav>

        <main>
        </main>
    </div>

</body>
</html>
