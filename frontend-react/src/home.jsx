import React from "react";
import "./home.css";

export default function Home() {
  return (
    <div className="bbt-site">
      {/* NAV */}
      <header className="bbt-nav">
        <div className="bbt-logo">
          <FreddyPixel />
          <span className="bbt-wordmark">Bubble&nbsp;Freddy</span>
        </div>
        <nav className="bbt-links">
          <a href="#menu">Menù</a>
          <a href="#dove">Dove siamo</a>
          <a href="#lavora">Lavora con noi</a>
          <a className="bbt-cta" href="#ordina">Ordina ora</a>
        </nav>
      </header>

      {/* HERO */}
      <section className="bbt-hero">
        <div className="bbt-hero-inner">
          <div className="bbt-hero-icons">
            <span className="cup" aria-hidden="true">🧋</span>
            <span className="cup" aria-hidden="true">🧋</span>
            <span className="cup" aria-hidden="true">🧋</span>
          </div>
          <h1 className="bbt-brand">
            <FreddyPixel />
            <span>Bubble&nbsp;Freddy</span>
          </h1>
          <p className="bbt-sub">Boba bar dal vibe arcade • FNAF edition</p>
          <div className="bbt-hero-ctas">
            <a className="btn primary" href="#menu">Scopri il Menù</a>
            <a className="btn ghost" href="#ordina">Pre-ordina e ritira</a>
          </div>
        </div>
      </section>

      {/* FILOSOFIA – stessa struttura/stile del sito di riferimento */}
      <section className="bbt-philosophy" id="filosofia" aria-label="Filosofia Bubble Bear">
        <div className="bbt-philosophy-overlay" />
        <div className="bbt-philosophy-inner">
          <h2>La filosofia positiva del Bubble Tea</h2>
          <p>
            Noi di <strong>Bubble Freddy</strong> vogliamo diffondere uno stile di vita
            colorato, divertente e positivo. Una pausa dolce che unisce creatività,
            ingredienti selezionati e l’energia di un’iconica sala giochi notturna.
          </p>
          <p>
            Questa ventata di aria fresca arriva dall’Asia e incontra le vibes
            della cultura pop: perle di tapioca danzano nel bicchiere come pixel
            sullo schermo, mentre luci neon e musica lo-fi ti accompagnano.
          </p>
          <p>
            Nel nostro locale potrai gustare <em>signature</em> bubble tea e topping
            personalizzati, immergendoti in un ambiente cool e rilassato.
          </p>
          <p>
            La nostra specialità è il <strong>Bubble Tea</strong>: una base di tè o latte
            con sciroppi fruttati, panna o schiume leggere e i classici boba.
            Tutte le nostre bevande vengono preparate al momento con ingredienti
            di prima scelta.
          </p>
        </div>
      </section>

      {/* PRODUCT STRIP (facoltativo ma aderente allo stile) */}
   <section className="bbt-menu" id="menu">
  <div className="bbt-menu-overlay" aria-hidden="true"></div>
  <h3>Le nostre Bubble Tea</h3>

  {/* CONTENITORE ANIMATO */}
  <div className="bbt-cards-wrapper">
    <div className="bbt-cards bbt-marquee">
      {/* CARD x4 + duplicate per animazione fluida */}
      <Card title="Freddy Strawberry" desc="Latte+Tè nero alla fragola, crema soffice e boba classici." />
      <Card title="Chica Mango" desc="Tè verde al mango con popping boba e lime." />
      <Card title="Bonnie Taro" desc="Latte al taro, perle nere e una spolverata di vaniglia." />
      <Card title="Foxy Matcha" desc="Matcha latte, miele e perle brune slow-cooked." />

      {/* DUPLICATI per l’effetto loop */}
      <Card title="Freddy Strawberry" desc="Latte+Tè nero alla fragola, crema soffice e boba classici." />
      <Card title="Chica Mango" desc="Tè verde al mango con popping boba e lime." />
      <Card title="Bonnie Taro" desc="Latte al taro, perle nere e una spolverata di vaniglia." />
      <Card title="Foxy Matcha" desc="Matcha latte, miele e perle brune slow-cooked." />
    </div>
  </div>

  <div className="bbt-menu-cta">
    <a className="btn primary" href="#ordina">Scopri il Menù completo</a>
  </div>
</section>



      {/* DOVE SIAMO */}
      <section className="bbt-where" id="dove">
        <h3>Dove siamo</h3>
        <div className="bbt-where-card">
          <div>
            <strong>Terni</strong>
            <div>Vico dell’Ortino, 11</div>
            <a className="btn tiny" href="tel:+390744">Chiama</a>
          </div>
          <div className="bbt-map-skel" aria-hidden="true">[mappa]</div>
        </div>
      </section>

      <footer className="bbt-footer">
        <span>© {new Date().getFullYear()} Bubble Freddy — FNAF fan theme non ufficiale.</span>
      </footer>
    </div>
  );
}

/* --- COMPONENTI --- */

function Card({ title, desc }) {
  return (
    <article className="bbt-card">
      <div className="bbt-card-img" aria-hidden="true">🧋</div>
      <div className="bbt-card-body">
        <h4>{title}</h4>
        <p>{desc}</p>
      </div>
    </article>
  );
}

/** Pixel logo di Freddy in SVG (stilizzato, safe-use, non ufficiale) */
function FreddyPixel() {
  return (
    <svg
      className="freddy"
      viewBox="0 0 64 64"
      role="img"
      aria-label="Freddy pixel logo"
    >
      <rect width="64" height="64" rx="8" className="freddy-bg" />
      {/* cappello */}
      <rect x="22" y="8" width="20" height="6" className="hat" />
      <rect x="27" y="4" width="10" height="6" className="hat" />
      {/* faccia */}
      <rect x="12" y="16" width="40" height="34" className="face" rx="6" />
      {/* orecchie */}
      <rect x="8" y="18" width="10" height="10" className="ear" />
      <rect x="46" y="18" width="10" height="10" className="ear" />
      {/* occhi */}
      <rect x="22" y="28" width="8" height="6" className="eye" />
      <rect x="34" y="28" width="8" height="6" className="eye" />
      {/* muso */}
      <rect x="20" y="36" width="24" height="10" className="muzzle" rx="3" />
      <rect x="26" y="40" width="4" height="2" className="tooth" />
      <rect x="34" y="40" width="4" height="2" className="tooth" />
    </svg>
  );
}