// src/components/menu.jsx
import {useEffect, useMemo, useState} from "react";
import "./menu.css";

/**
 * === CONFIGURAZIONE API ===
 * In produzione usa import.meta.env.VITE_API_BASE (imposta .env.local).
 */
const API_BASE =
    import.meta?.env?.VITE_API_BASE?.replace(/\/$/, "") || "http://localhost:5174";

/**
 * Endpoints: adatta questi path ai tuoi controller reali.
 * Il componente prova prima menu dinamico e, se non c'è, fa fallback su dolci/standard/ingredienti.
 */
const ENDPOINTS = {
    menuDinamico: "/api/Menu/dinamico", // se hai una view VwMenuDinamico esposta
    dolci: "/api/Dolce/disponibili",
    bevandeStandard: "/api/BevandaStandard/disponibili",
    ingredientiDisponibili: "/api/Ingrediente/disponibili",
    categorieIngredienti: "/api/CategoriaIngrediente", // opzionale
};

/** Helper fetch con token opzionale (se usi JWT) */
async function apiGet(path, token) {
    const res = await fetch(`${API_BASE}${path}`, {
        headers: token ? {Authorization: `Bearer ${token}`} : {},
    });
    if (!res.ok) throw new Error(`${path} -> ${res.status}`);
    return res.json();
}

/** Mapper DTO → Product UI-friendly */
function mapFromVwMenuDinamico(v) {
    return {
        id: v.id,
        name: v.nomeBevanda,
        description: v.descrizione ?? "",
        price: Number(v.prezzoLordo ?? v.prezzoNetto ?? 0),
        rawPriceNet: Number(v.prezzoNetto ?? 0),
        iva: Number(v.ivaPercentuale ?? 0),
        imageUrl: v.immagineUrl || "",
        type: v.tipo, // "Dolce" | "BevandaStandard" | ...
        priority: v.priorita ?? 0,
    };
}

function mapFromDolce(d) {
    return {
        id: d.articoloId,
        name: d.nome,
        description: d.descrizione ?? "",
        price: Number(d.prezzo ?? 0),
        imageUrl: d.immagineUrl || "",
        type: "Dolce",
        priority: d.priorita ?? 0,
    };
}

function mapFromBevandaStandard(b) {
    const dim = b.dimensioneBicchiere?.nome || b.dimensioneBicchiere?.dimensione || "";
    return {
        id: b.articoloId,
        name: `Bevanda • ${dim || "Standard"}`,
        description: "",
        price: Number(b.prezzo ?? 0),
        imageUrl: b.immagineUrl || "",
        type: "BevandaStandard",
        priority: b.priorita ?? 0,
    };
}

/** Split per sezioni */
function splitByType(items) {
    const dolci = items.filter((x) => (x.type || "").toLowerCase().includes("dolce"));
    const standard = items.filter((x) => (x.type || "").toLowerCase().includes("standard"));
    return {
        dolci: dolci.sort((a, b) => b.priority - a.priority),
        standard: standard.sort((a, b) => b.priority - a.priority),
    };
}

export default function Menu({token, onAddToCart}) {
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState("");
    const [dolci, setDolci] = useState([]);
    const [standard, setStandard] = useState([]);
    const [showCustom, setShowCustom] = useState(false);

    // personalizzazione
    const [ingredienti, setIngredienti] = useState([]);
    const [categorie, setCategorie] = useState([]);
    const [selezionati, setSelezionati] = useState(new Set());
    const [customName, setCustomName] = useState("La mia bevanda");

    const cartHandler = (item) => {
        if (onAddToCart) onAddToCart(item);
        else console.log("Aggiunto al carrello:", item);
    };

    useEffect(() => {
        (async () => {
            try {
                setLoading(true);
                setError("");

                // 1) Prova menu dinamico
                try {
                    const data = await apiGet(ENDPOINTS.menuDinamico, token);
                    const mapped = (data || []).map(mapFromVwMenuDinamico);
                    const {dolci, standard} = splitByType(mapped);
                    setDolci(dolci);
                    setStandard(standard);
                } catch {
                    // 2) Fallback: dolci + standard
                    const [d, s] = await Promise.all([
                        apiGet(ENDPOINTS.dolci, token).catch(() => []),
                        apiGet(ENDPOINTS.bevandeStandard, token).catch(() => []),
                    ]);
                    setDolci((d || []).map(mapFromDolce));
                    setStandard((s || []).map(mapFromBevandaStandard));
                }

                // 3) Ingredienti (best-effort)
                const [ing, cat] = await Promise.all([
                    apiGet(ENDPOINTS.ingredientiDisponibili, token).catch(() => []),
                    apiGet(ENDPOINTS.categorieIngredienti, token).catch(() => []),
                ]);
                setIngredienti(Array.isArray(ing) ? ing : []);
                setCategorie(Array.isArray(cat) ? cat : []);
            } catch (e) {
                setError("Non riesco a leggere il menu dal back-end. Controlla gli endpoint o CORS.");
                console.error(e);
            } finally {
                setLoading(false);
            }
        })();
    }, [token]);

    // ingredienti per categoria
    const ingredientiPerCategoria = useMemo(() => {
        const byCat = new Map();
        for (const ing of ingredienti) {
            const catId = ing.categoriaId ?? 0;
            if (!byCat.has(catId)) byCat.set(catId, []);
            byCat.get(catId).push(ing);
        }
        for (const [, arr] of byCat) {
            arr.sort((a, b) => Number(b.prezzoAggiunto) - Number(a.prezzoAggiunto));
        }
        return byCat;
    }, [ingredienti]);

    const nomeCategoria = (categoriaId) => {
        const found = categorie.find((c) => c.categoriaId === categoriaId);
        return found?.categoria || `Categoria ${categoriaId}`;
    };

    const totaleCustom = useMemo(() => {
        let tot = 0;
        for (const ing of ingredienti) {
            if (selezionati.has(ing.ingredienteId)) tot += Number(ing.prezzoAggiunto || 0);
        }
        return Number(tot.toFixed(2));
    }, [selezionati, ingredienti]);

    const toggleIngrediente = (id) =>
        setSelezionati((prev) => {
            const next = new Set(prev);
            if (next.has(id)) next.delete(id);
            else next.add(id);
            return next;
        });

    const addCustomToCart = () => {
        const chosen = ingredienti.filter((i) => selezionati.has(i.ingredienteId));
        if (!chosen.length) return alert("Seleziona almeno un ingrediente 🙂");
        cartHandler({
            id: `custom-${Date.now()}`,
            name: customName || "Bevanda personalizzata",
            description: `${chosen.length} ingredienti`,
            price: totaleCustom,
            imageUrl: "",
            type: "BevandaCustom",
            meta: {ingredienti: chosen},
        });
        // reset
        setSelezionati(new Set());
        setCustomName("La mia bevanda");
        setShowCustom(false);
    };

    return (
        <div className="menu">
            <header className="menu-header">
                <h2 className="menu-title">Il nostro Menu</h2>
                <div className="menu-actions">
                    <button className="btn ghost" onClick={() => setShowCustom(true)}>
                        + Crea la tua bevanda
                    </button>
                </div>
            </header>

            {loading && <div className="menu-skel">Caricamento in corso…</div>}
            {error && <div className="menu-error">{error}</div>}

            {!loading && !error && (
                <>
                    {/* Bevande Standard */}
                    <section className="menu-section">
                        <h3 className="menu-section-title">Bevande Standard</h3>
                        <div className="menu-grid">
                            {standard.map((p) => (
                                <article key={`std-${p.id}`} className="product-card">
                                    {p.imageUrl ? (
                                        <img className="product-img" src={p.imageUrl} alt={p.name}/>
                                    ) : (
                                        <div className="product-img skel"/>
                                    )}
                                    <div className="product-body">
                                        <div className="product-head">
                                            <h4 className="product-title">{p.name}</h4>
                                            <span className="product-price">€ {p.price.toFixed(2)}</span>
                                        </div>
                                        {p.description ? <p className="product-desc">{p.description}</p> : null}
                                        <button className="btn add" onClick={() => cartHandler(p)}>
                                            Aggiungi al carrello
                                        </button>
                                    </div>
                                </article>
                            ))}
                            {!standard.length && <div className="menu-empty">Nessuna bevanda standard disponibile</div>}
                        </div>
                    </section>

                    {/* Dolci */}
                    <section className="menu-section">
                        <h3 className="menu-section-title">Dolci</h3>
                        <div className="menu-grid">
                            {dolci.map((p) => (
                                <article key={`dolce-${p.id}`} className="product-card">
                                    {p.imageUrl ? (
                                        <img className="product-img" src={p.imageUrl} alt={p.name}/>
                                    ) : (
                                        <div className="product-img skel"/>
                                    )}
                                    <div className="product-body">
                                        <div className="product-head">
                                            <h4 className="product-title">{p.name}</h4>
                                            <span className="product-price">€ {p.price.toFixed(2)}</span>
                                        </div>
                                        {p.description ? <p className="product-desc">{p.description}</p> : null}
                                        <button className="btn add" onClick={() => cartHandler(p)}>
                                            Aggiungi al carrello
                                        </button>
                                    </div>
                                </article>
                            ))}
                            {!dolci.length && <div className="menu-empty">Nessun dolce disponibile</div>}
                        </div>
                    </section>
                </>
            )}

            {/* MODALE PERSONALIZZAZIONE */}
            {showCustom && (
                <div className="modal" role="dialog" aria-modal="true">
                    <div className="modal-card">
                        <div className="modal-head">
                            <h4>Crea la tua bevanda</h4>
                            <button className="icon-btn" aria-label="Chiudi" onClick={() => setShowCustom(false)}>
                                ✕
                            </button>
                        </div>

                        <label className="field">
                            <span>Nome</span>
                            <input
                                value={customName}
                                onChange={(e) => setCustomName(e.target.value)}
                                placeholder="Es. Foxy Matcha Personal"
                            />
                        </label>

                        <div className="ingr-wrap">
                            {Array.from(ingredientiPerCategoria.entries()).map(([catId, list]) => (
                                <div className="ingr-group" key={`cat-${catId}`}>
                                    <h5 className="ingr-title">{nomeCategoria(catId)}</h5>
                                    <div className="ingr-grid">
                                        {list.map((ing) => (
                                            <label
                                                key={ing.ingredienteId}
                                                className={`ingr-item ${selezionati.has(ing.ingredienteId) ? "on" : ""}`}
                                            >
                                                <input
                                                    type="checkbox"
                                                    checked={selezionati.has(ing.ingredienteId)}
                                                    onChange={() => toggleIngrediente(ing.ingredienteId)}
                                                />
                                                <span className="ingr-name">{ing.ingrediente1}</span>
                                                <span
                                                    className="ingr-price">+€ {Number(ing.prezzoAggiunto || 0).toFixed(2)}</span>
                                            </label>
                                        ))}
                                    </div>
                                </div>
                            ))}
                            {!ingredienti.length &&
                                <div className="menu-empty">Nessun ingrediente disponibile al momento</div>}
                        </div>

                        <div className="modal-foot">
                            <div className="total">Totale: € {totaleCustom.toFixed(2)}</div>
                            <button className="btn add" onClick={addCustomToCart}>
                                Aggiungi al carrello
                            </button>
                        </div>
                    </div>
                </div>
            )}
        </div>
    );
}
