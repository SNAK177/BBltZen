import "./home.css";

export default function Home() {
    return (
        <div className="bbt-site">
            <section className="bbt-hero">
                <div className="bbt-hero-inner">
                    <div className="logo-img">
                        <img src="/assets/logo_fnaf.png" alt="Logo FNAF Boba"/>
                    </div>
                    <h1 className="bbt-brand"><span>Bubble&nbsp;Freddy</span></h1>
                    <p className="bbt-sub">Boba bar dal vibe arcade • FNAF edition</p>
                    <div className="bbt-hero-ctas">
                        <a className="btn primary" href="#menu">Scopri il Menù</a>
                        <a className="btn ghost" href="#ordina">Pre-ordina e ritira</a>
                    </div>
                </div>
            </section>

            <section className="bbt-philosophy" id="filosofia" aria-label="Filosofia Bubble Freddy">
                <div className="bbt-philosophy-overlay"/>
                <div className="bbt-philosophy-inner">
                    <h2>La filosofia positiva del Bubble Tea</h2>
                    <p>Noi di <strong>Bubble Freddy</strong> vogliamo diffondere uno stile di vita colorato e positivo.
                    </p>
                    <p>Signature bubble tea, topping personalizzati e vibes da sala giochi notturna.</p>
                </div>
            </section>

            <section className="bbt-menu" id="menu" aria-label="Le nostre Bubble Tea">
                <div className="bbt-menu-overlay" aria-hidden="true"/>
                <h3>Le nostre Bubble Tea</h3>

                <div className="bbt-cards-wrapper">
                    <div className="bbt-cards bbt-marquee">
                        <Card
                            imgURL="/assets/freddyStrawberry.png"
                            title="Freddy Strawberry"
                            desc="Latte+Tè nero alla fragola, crema soffice e boba classici."
                        />
                        <Card
                            imgURL="/assets/chicaMango.png"
                            title="Chica Mango"
                            desc="Tè verde al mango con popping boba e lime."
                        />
                        <Card
                            imgURL="/assets/bonnieTaro.png"
                            title="Bonnie Taro"
                            desc="Latte al taro, perle nere e vaniglia."
                        />
                        <Card
                            imgURL="/assets/foxyMatcha.png"
                            title="Foxy Matcha"
                            desc="Matcha latte, miele e perle brune."
                        />
                    </div>
                </div>

                <div className="bbt-menu-cta">
                    <a className="btn primary" href="/menu">Scopri il Menù completo</a>
                </div>
            </section>

            <section className="bbt-where" id="dove" aria-label="Dove siamo">
                <div className="bbt-where-banner">
                    <div className="bbt-container"><h3>Dove siamo</h3></div>
                </div>
                <div className="bbt-container bbt-where-list">
                    <WhereCard
                        city="Amelia"
                        address="Via Gian Francesco Perini, 48/A"
                        tel="+390744"
                        mapSrc="https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d2939.010495854911!2d12.4184247760551!3d42.55507002275911!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x132ede3de0ff0bc7%3A0x900f1e4dfd03ec37!2sVia%20Gian%20Francesco%20Perini%2C%2048%2FA%2C%2005022%20Amelia%20TR!5e0!3m2!1sit!2sit!4v1761226971186!5m2!1sit!2sit"
                    />
                </div>
            </section>

            <section id="ordina" aria-label="Ordina ora" className="bbt-container" style={{padding: "40px 18px"}}>
                <a className="btn primary" href="/carrello">Vai al carrello e ordina</a>
            </section>

            <footer className="bbt-footer">
                <span>© {new Date().getFullYear()} Bubble Freddy — FNAF fan theme non ufficiale.</span>
            </footer>
        </div>
    );
}

/* --- CARD: immagine intera (no crop bicchiere), card più larga --- */
function Card({title, desc, imgURL}) {
    return (
        <article className="bbt-card">
            <figure className="bbt-card-media">
                <img src={imgURL} alt={title} loading="lazy"/>
            </figure>

            <div className="bbt-card-side">
                <h4 className="bbt-card-title">{title}</h4>
                <p className="bbt-card-desc">{desc}</p>
            </div>
        </article>
    );
}

function WhereCard({city, address, tel, mapSrc}) {
    return (
        <div className="bbt-where-card">
            <div className="bbt-where-info">
                <strong>{city}</strong>
                <div>{address}</div>
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
