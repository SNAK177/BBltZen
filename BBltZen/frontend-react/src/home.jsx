import "./home.css";

export default function Home() {
  return (
    <div className="bbt-site">
      {/* NAV */}
      <header className="bbt-nav">
        <div className="bbt-logo">
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
          <div className="logo-img">
            <img src="logo_fnaf.png" alt="Logo FNAF Boba"></img>
          </div>
          <h1 className="bbt-brand">
            <span>Bubble&nbsp;Freddy</span>
          </h1>
          <p className="bbt-sub">Boba bar dal vibe arcade • FNAF edition</p>
          <div className="bbt-hero-ctas">
            <a className="btn primary" href="#menu">Scopri il Menù</a>
            <a className="btn ghost" href="#ordina">Pre-ordina e ritira</a>
          </div>
        </div>
      </section>

      {/* FILOSOFIA */}
      <section className="bbt-philosophy" id="filosofia" aria-label="Filosofia Bubble Freddy">
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

      {/* MENU / PRODUCT STRIP */}
      <section className="bbt-menu" id="menu">
        <div className="bbt-menu-overlay" aria-hidden="true" />
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
        {/* Fascia arancione separata */}
        <div className="bbt-where-banner">
          <div className="bbt-container">
            <h3>Dove siamo</h3>
          </div>
        </div>

        {/* Card dei punti vendita su sfondo scuro, staccate dalla fascia */}
        <div className="bbt-container bbt-where-list">
          <WhereCard
            city="Amelia"
            address="Via Gian Francesco Perini, 48/A"
            tel="+390744"
            mapSrc="https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d2939.010495854911!2d12.4184247760551!3d42.55507002275911!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x132ede3de0ff0bc7%3A0x900f1e4dfd03ec37!2sVia%20Gian%20Francesco%20Perini%2C%2048%2FA%2C%2005022%20Amelia%20TR!5e0!3m2!1sit!2sit!4v1761226971186!5m2!1sit!2sit"
          />
          <WhereCard
            city="Terni"
            address="Via Roma, 34"
            tel="+390744"
            mapSrc="https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d2936.304219845037!2d12.541227886969006!3d42.6125036447934!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x132ee5c432f16eb1%3A0x33a5daf271b8b73d!2sV.%20Roma%2C%2034%2C%2005029%20San%20Gemini%20TR!5e0!3m2!1sit!2sit!4v1761230445185!5m2!1sit!2sit"/>
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

function WhereCard({ city, address, tel, mapSrc }) {
  return (
    <div className="bbt-where-card">
      <div className="bbt-where-info">
        <strong>{city}</strong>
        <div>{address}</div>
        {/* href telefono corretto con backticks e replace spazi */}
        <a className="btn tiny" href={`tel:${tel.replace(/\s+/g, "")}`}>Chiama</a>
      </div>
      <div className="bbt-map-skel">
        <iframe
          src={mapSrc}
          width="660"
          height="450"
          loading="lazy"
          allowFullScreen
          referrerPolicy="no-referrer-when-downgrade"
          title={`Mappa ${city}`}
        />
      </div>
    </div>
  );
}

